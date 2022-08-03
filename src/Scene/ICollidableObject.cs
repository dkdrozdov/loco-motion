using ProtoBuf;

namespace LocoMotionServer
{
    public interface ICollidableObject : ISceneObject
    {
        public float CollisionBoxWidth { get; set; }
        public float CollisionBoxHeight { get; set; }
        void OnCollision(ICollisionEvent e);
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(Platform))]
    [ProtoInclude(2, typeof(PhysicalObject))]
    public class CollidableObject : SceneObject, ICollidableObject
    {
        [ProtoMember(3)]
        public float CollisionBoxWidth { get; set; }
        [ProtoMember(4)]
        public float CollisionBoxHeight { get; set; }

        public CollidableObject() : base() { }

        public void OnCollision(ICollisionEvent e)
        {
            // Noop.
        }
    }
}