using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    class Platform : CollidableData
    {
        public Platform() : base()
        {

        }
        public Platform(float collisionBoxWidth, float collisionBoxHeight)
        {
            CollisionBoxWidth = collisionBoxWidth;
            CollisionBoxHeight = collisionBoxHeight;
        }
        public Platform(SceneObjectData sceneObjectData, float width, float height) : base(sceneObjectData, width, height)
        {
        }
    }
}