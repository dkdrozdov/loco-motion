using System;
using ProtoBuf;

namespace LocoMotionServer
{
    public interface IPhysicalObject : ICollidableObject
    {
        IVector2D Velocity { get; set; }
        IVector2D Force { get; set; }
        IVector2D Momentum { get; }
        float Mass { get; set; }
        bool isGrounded { get; set; }
        void OnMove(IMoveEvent e);
    }

    public abstract class PhysicalObject : CollidableObject, IPhysicalObject
    {
        [ProtoMember(2)]
        public IVector2D Velocity { get; set; } = new Vector2D();
        [ProtoMember(3)]
        public IVector2D Force { get; set; } = new Vector2D();
        [ProtoMember(4)]
        public float Mass { get; set; } = 1.0f;
        [ProtoMember(5)]
        public bool isGrounded { get; set; } = false;

        public IVector2D Momentum => Mass * (Vector2D)Velocity;

        public PhysicalObject() : base() { }

        public void OnMove(IMoveEvent e)
        {
            Console.WriteLine($"New position ({e.To.X}, {e.To.Y})");
        }
    }
}