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
        public Platform(float boxWidth, float boxHeight)
        {
            Renderable = new TexturedRectangle(this);
            BoxWidth = boxWidth;
            BoxHeight = boxHeight;
        }
        [Newtonsoft.Json.JsonIgnore]

        public override IRenderable Renderable { get; set; }
    }
}