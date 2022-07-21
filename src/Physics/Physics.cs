public interface ICollidable
{
    IVector2D BottomLeft { get; set; }
    IVector2D TopRight { get; set; }
}

public interface IPhysicalObjectData : ICollidable
{
    IVector2D Velocity { get; set; }
    IVector2D Face { get; set; }
    float Mass { get; set; }
}

public interface IPhysicalObject : ISceneObject, IPhysicalObjectData
{
    void OnMove(IMoveEvent e);
    void OnCollision(ICollisionEvent e);
}

public interface ICollisionEvent
{
    IPhysicalObject CollidingObject { get; }
}

public interface IMoveEvent
{
    IVector2D From { get; }
    IVector2D To { get; }
}

public class Physics
{
    void step(IScene scene)
    {
        foreach (var so in scene.SceneObjects)
        {
            so.Position.X += 1;
            so.Position.Y += 1;
        }
    }
}