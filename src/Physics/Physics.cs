using System;

namespace LocoMotionServer
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
            var sObjs = _scene.All();
            var fObjs = _scene.All<IPhysicalObject>();
            foreach (var fo in fObjs)
            {
                Vector2D acceleration;
                if (fo.isGrounded)
                {
                    var frictionForce = new Vector2D(-Math.Sign(fo.Velocity.X) * Physics.k * fo.Mass * Physics.g, 0f);
                    Vector2D forces = (Vector2D)fo.Force + frictionForce;
                    acceleration = new Vector2D(forces.X / fo.Mass, forces.Y);
                }
                else
                {
                    var gravityForce = new Vector2D(0f, -fo.Mass * Physics.g);
                    Vector2D forces = (Vector2D)fo.Force + gravityForce;
                    acceleration = forces;
                }
                fo.Velocity = (Vector2D)fo.Velocity + acceleration;
                if (Math.Abs(fo.Velocity.X) < Physics.minVelocity)
                {
                    fo.Velocity.X = 0f;
                }
                if (Math.Abs(fo.Velocity.Y) < Physics.minVelocity)
                {
                    fo.Velocity.Y = 0f;
                }
                // TODO@ekdrozdov: respect collisions.
                var oldPosition = fo.Position;
                fo.Position = (Vector2D)fo.Position + dt * (Vector2D)fo.Velocity;
                if (fo.Position.X != oldPosition.X || fo.Position.Y != oldPosition.Y)
                {
                    var moveEvent = new MoveEvent(oldPosition, fo.Position);
                    fo.OnMove(moveEvent);
                }
            }
        }
    }
}