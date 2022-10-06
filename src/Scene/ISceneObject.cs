using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    [ProtoInclude(1, typeof(SceneObject))]
    public interface ISceneObject : IManagableObject
    {
        string Id { get; set; }
        IVector2D Position { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }
        IRenderable Renderable { get; set; }
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(CollidableObject))]
    public abstract class SceneObject : ManagableObject, ISceneObject
    {
        [ProtoMember(2)]
        [Newtonsoft.Json.JsonIgnore]
        public string Id { get; set; } = "NO_ID";
        [ProtoMember(3)]
        public IVector2D Position { get; set; } = new Vector2D();
        public float Scale { get; set; } = 1.0f;
        public float Rotation { get; set; } = 0f;
        [Newtonsoft.Json.JsonIgnore]
        public abstract IRenderable Renderable { get; set; }
        public SceneObject() : base() { }

        public SceneObject(string id, IVector2D position)
        {
            this.Id = id;
            Position = position;
        }

    }

    public interface IAnimatableObject : ISceneObject
    {
        enum Animations { };
    }

    public class Ground : BoxObject
    {
        public Ground(float width, float height) : base(width, height)
        {
            Renderable = new TexturedRectangle(this);
        }
        [Newtonsoft.Json.JsonIgnore]

        public override IRenderable Renderable { get; set; }
        public override float BoxWidth { get; set; }
        public override float BoxHeight { get; set; }
    }

    public class Cat : SceneObject
    {
        public Cat()
        {
            Renderable = new SpritePoint(this);
        }

        [Newtonsoft.Json.JsonIgnore]
        public override IRenderable Renderable { get; set; }
    }

    public class FlippedCat : SceneObject
    {
        public FlippedCat()
        {
            Renderable = new SpritePoint(this);
        }
        [Newtonsoft.Json.JsonIgnore]

        public override IRenderable Renderable { get; set; }
    }
}