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
            Renderable = new TexturedRectangle(this);
            CollisionBoxWidth = collisionBoxWidth;
            CollisionBoxHeight = collisionBoxHeight;
        }
        [Newtonsoft.Json.JsonIgnore]

        public override IRenderable Renderable { get; set; }
        public override string TextureId { get => "resources/resourcePacks/Common/platform.png"; }
    }
}