using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    [ProtoInclude(1, typeof(SceneObject))]
    public interface ISceneObject : IManagableObject
    {
        string Id { get; set; }
        IVector2D Position { get; set; }
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(CollidableObject))]
    public class SceneObject : ManagableObject, ISceneObject
    {
        [ProtoMember(2)]
        public string Id { get; set; } = "NO_ID";
        [ProtoMember(3)]
        public IVector2D Position { get; set; } = new Vector2D();

        public SceneObject() : base() { }

        public SceneObject(string id, IVector2D position)
        {
            this.Id = id;
            Position = position;
        }
    }
}