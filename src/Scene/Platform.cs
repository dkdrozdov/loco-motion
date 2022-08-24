namespace LocoMotionServer
{
    public interface IPlatform : ICollidableObject
    {

    }

    public class Platform : CollidableObject
    {
        public Platform() : base()
        {
            Renderable = new TexturedRectangle(this);
        }
        public Platform(float collisionBoxWidth, float collisionBoxHeight)
        {
            Renderable = new TexturedRectangle(this, collisionBoxWidth, collisionBoxHeight);
            CollisionBoxWidth = collisionBoxWidth;
            CollisionBoxHeight = collisionBoxHeight;
        }
        [Newtonsoft.Json.JsonIgnore]

        public override IRenderable Renderable { get; set; }
    }
}