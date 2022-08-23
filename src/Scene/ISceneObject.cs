using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    [ProtoInclude(1, typeof(SceneObject))]
    public interface ISceneObject : IManagableObject
    {
        string Id { get; set; }
        IVector2D Position { get; set; }

        string TextureId { get; set; }
        public void Render(IRenderer renderer);
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(CollidableObject))]
    public class SceneObject : ManagableObject, ISceneObject
    {
        [ProtoMember(2)]
        public string Id { get; set; } = "NO_ID";
        [ProtoMember(3)]
        public IVector2D Position { get; set; } = new Vector2D();
        public string TextureId { get; set; } = "";

        public SceneObject() : base() { }

        public SceneObject(string id, IVector2D position)
        {
            this.Id = id;
            Position = position;
        }

        public virtual void Render(IRenderer renderer)
        {

        }
    }

    public interface IAnimatableObject : ISceneObject
    {
        enum Animations { };
    }

    public class Ground : TexturedRectangle
    {
        public Ground(IVector2D bottomLeft, IVector2D topRight) : base(bottomLeft, topRight)
        {
            TextureId = "ground.jpeg";
        }

        public int ResourceItemKind => throw new System.NotImplementedException();
    }

    public class Cat : SpritePoint
    {
        public Cat()
        {
            TextureId = "resources/resourcePacks/Cat/sprite.png";
        }

    }

    public class FlippedCat : SpritePoint
    {
        public FlippedCat()
        {
            TextureId = "resources/resourcePacks/FlippedCat/sprite2.png";
        }

    }
}