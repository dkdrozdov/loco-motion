using ProtoBuf;

namespace LocoMotionServer
{
    public interface IBoxObject : ISceneObject
    {
        public float BoxWidth { get; set; }
        public float BoxHeight { get; set; }
    }
    public abstract class BoxObject : SceneObject, IBoxObject
    {
        public BoxObject() : base() { }

        public BoxObject(float boxWidth, float boxHeight)
        {
            BoxWidth = boxWidth;
            BoxHeight = boxHeight;
        }

        public abstract float BoxWidth { get; set; }
        public abstract float BoxHeight { get; set; }
    }

    public interface ICollidableObject : IBoxObject
    {
        public float CollisionBoxWidth { get; set; }
        public float CollisionBoxHeight { get; set; }
        void OnCollision(ICollisionEvent e);
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(Platform))]
    [ProtoInclude(2, typeof(PhysicalObject))]
    public abstract class CollidableObject : BoxObject, ICollidableObject
    {
        [ProtoMember(3)]
        public float CollisionBoxWidth { get; set; }
        [ProtoMember(4)]
        public float CollisionBoxHeight { get; set; }
        public override float BoxWidth { get => CollisionBoxWidth; set => CollisionBoxWidth = value; }
        public override float BoxHeight { get => CollisionBoxHeight; set => CollisionBoxHeight = value; }
        public CollidableObject() : base() { }

        public void OnCollision(ICollisionEvent e)
        {
            // Noop.
        }
    }
}