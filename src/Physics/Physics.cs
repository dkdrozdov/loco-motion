using System;
using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    [ProtoInclude(3, typeof(IPhysicalObjectData))]
    public interface ICollidableData : ISceneObjectData
    {
        [ProtoMember(1)]
        float CollisionBoxWidth { get; set; }
        [ProtoMember(2)]
        float CollisionBoxHeight { get; set; }
    }

    public interface ICollidable : ISceneObject
    {
        void OnCollision(ICollisionEvent e);
    }

    [ProtoContract]
    [ProtoInclude(3, typeof(PhysicalObject))]
    public class Collidable : SceneObject, ICollidable
    {
        public Collidable(ISceneObjectData data) : base(data)
        {
        }
        public Collidable() : base()
        {

        }

        public void OnCollision(ICollisionEvent e)
        {
            // Noop.
        }
    }
    [ProtoContract]
    [ProtoInclude(1, typeof(IPhysicalObject))]
    public interface IPhysicalObjectData : ICollidableData
    {
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(PhysicalObject))]
    public interface IPhysicalObject : IPhysicalObjectData, ICollidable
    {
        [ProtoMember(2)]
        IVector2D Velocity { get; set; }
        [ProtoMember(3)]
        IVector2D Rotation { get; set; }
        [ProtoMember(4)]
        IVector2D Force { get; set; }
        IVector2D Momentum { get; }
        [ProtoMember(5)]
        float Mass { get; set; }
        [ProtoMember(6)]
        bool isGrounded { get; set; }
        void OnMove(IMoveEvent e);
    }

    [ProtoContract]
    public class PhysicalObject : Collidable, IPhysicalObject
    {
        public PhysicalObject() : base()
        {
        }

        public PhysicalObject(ISceneObjectData data) : base(data)
        {
        }
        [ProtoMember(3)]
        public IVector2D Velocity { get; set; } = new Vector2D();
        [ProtoMember(4)]
        public IVector2D Rotation { get; set; } = new Vector2D();
        [ProtoMember(5)]
        public IVector2D Force { get; set; } = new Vector2D();
        [ProtoMember(6)]
        public float Mass { get; set; } = 1.0f;
        [ProtoMember(7)]
        public float CollisionBoxWidth { get; set; } = 0f;
        [ProtoMember(8)]
        public float CollisionBoxHeight { get; set; } = 0f;
        [ProtoMember(9)]
        public bool isGrounded { get; set; } = false;
        public IVector2D Momentum => Mass * (Vector2D)Velocity;

        public void OnMove(IMoveEvent e)
        {
            Console.WriteLine($"New position ({e.To.X}, {e.To.Y})");
        }
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