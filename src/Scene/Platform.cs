using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    class Platform : Collidable
    {
        public Platform() : base()
        {

        }
        public Platform(float collisionBoxWidth, float collisionBoxHeight)
        {
            CollisionBoxWidth = collisionBoxWidth;
            CollisionBoxHeight = collisionBoxHeight;
        }
        public Platform(SceneObject sceneObject, float width, float height) : base(sceneObject, width, height)
        {
        }
    }
}