namespace LocoMotionServer
{
    public interface IPlatform : ICollidableObject
    {

    }

    public class Platform : CollidableObject
    {
        public Platform() : base() { }
        public Platform(float collisionBoxWidth, float collisionBoxHeight)
        {
            CollisionBoxWidth = collisionBoxWidth;
            CollisionBoxHeight = collisionBoxHeight;
        }
    }
}