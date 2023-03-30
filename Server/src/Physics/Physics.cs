namespace Server
{
  public interface ICollisionEvent
  {
    IPhysicalObject CollidingObject { get; }
  }

  public interface IMoveEvent
  {
    IVector2D From { get; }
    IVector2D To { get; }
  }

  public class MoveEvent : IMoveEvent
  {
    public IVector2D From { get; }

    public IVector2D To { get; }
    public MoveEvent(IVector2D from, IVector2D to)
    {
      From = from;
      To = to;
    }
  }

  public interface IPhysics
  {
    void step(float dt);
  }

  public class Physics : IPhysics
  {
    private static float g = 0.1f;
    private static float k = 0.2f;
    private static float minVelocity = 0.005f;
    private IScene _scene;

    public Physics(IScene scene)
    {
      _scene = scene;
    }

    public void step(float dt)
    {
      var fObjs = _scene.All<IPhysicalObject>();
      foreach (var fo in fObjs)
      {
        Vector2D acceleration;
        // Consider friction.
        if (fo.isGrounded)
        {
          var frictionForce = new Vector2D(-Math.Sign(fo.Velocity.X) * Physics.k * fo.Mass * Physics.g, 0f);
          Vector2D forces = (Vector2D)fo.Force + frictionForce;
          acceleration = new Vector2D(forces.X / fo.Mass, forces.Y);
        }
        // Consider gravity.
        else
        {
          var gravityForce = new Vector2D(0f, -fo.Mass * Physics.g);
          Vector2D forces = (Vector2D)fo.Force + gravityForce;
          acceleration = forces;
        }

        // Consider acceleration.
        fo.Velocity = (Vector2D)fo.Velocity + acceleration;

        // "Normalize" velocity.
        if (Math.Abs(fo.Velocity.X) < Physics.minVelocity)
        {
          fo.Velocity.X = 0f;
        }
        if (Math.Abs(fo.Velocity.Y) < Physics.minVelocity)
        {
          fo.Velocity.Y = 0f;
        }

        var sourcePosition = fo.Position;
        var targetPosition = (Vector2D)fo.Position + dt * (Vector2D)fo.Velocity;

        // Consider collision.

        // Building a line by 2 points.

        // Left-bottom point line.
        var x1 = sourcePosition.X;
        var y1 = sourcePosition.Y;
        var x2 = targetPosition.X;
        var y2 = targetPosition.Y;

        var ps = _scene.All<Platform>();
        foreach (var p in ps)
        {
          // Upper-border line.
          var x3 = p.Position.X;
          var y3 = p.Position.Y + p.BoxHeight;
          var x4 = x3 + p.BoxWidth;
          var y4 = p.Position.Y;

          var x0 = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
          var y0 = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

          // Check if an insersection point belongs to the both lines.
          var left1 = x1 < x2 ? x1 : x2;
          var right1 = x1 > x2 ? x1 : x2;
          var bottom1 = y1 < y2 ? y1 : y2;
          var top1 = y1 > y2 ? y1 : y2;

          var left2 = x3 < x4 ? x3 : x4;
          var right2 = x3 > x4 ? x3 : x4;
          var bottom2 = y3 < y4 ? y3 : y4;
          var top2 = y3 > y4 ? y3 : y4;

          // Find a strict borders.
          var top = top1 < top2 ? top1 : top2;
          var bottom = bottom1 > bottom2 ? bottom1 : bottom2;
          var left = left1 > left2 ? left1 : left2;
          var right = right1 < right2 ? right1 : right2;

          var distance1To1 = Math.Sqrt((x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1));
          var distance1To2 = Math.Sqrt((x0 - x2) * (x0 - x2) + (y0 - y2) * (y0 - y2));
          var trueDistance1 = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

          var distance2To1 = Math.Sqrt((x0 - x3) * (x0 - x3) + (y0 - y3) * (y0 - y3));
          var distance2To2 = Math.Sqrt((x0 - x4) * (x0 - x4) + (y0 - y4) * (y0 - y4));
          var trueDistance2 = Math.Sqrt((x3 - x4) * (x3 - x4) + (y3 - y4) * (y3 - y4));

          var eps = 0.0001f;

          bool isBelong = ((distance1To1 + distance1To2 - trueDistance1) < eps && (distance2To1 + distance2To2 - trueDistance2) < eps);

          // Collision detected -> apply it to the movement.
          if (isBelong)
          {
            targetPosition.X = x0;
            targetPosition.Y = y0;
          }
        }

        fo.Position = targetPosition;
        if (fo.Position.X != sourcePosition.X || fo.Position.Y != sourcePosition.Y)
        {
          var moveEvent = new MoveEvent(sourcePosition, fo.Position);
          fo.OnMove(moveEvent);
        }
      }
    }
  }
}