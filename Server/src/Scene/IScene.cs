using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    [ProtoInclude(1, typeof(Scene))]
    public interface IScene
    {
        IVector2D Size { get; }
        IEnumerable<ISceneObject> SceneObjects { get; }
        void AddObject(ISceneObject sceneObject);
        void RemoveObject(ISceneObject sceneObject);
        ISceneObject Query(string id);
        IEnumerable<T> All<T>();
    }

    [ProtoContract]
    public class Scene : IScene
    {
        [ProtoMember(1)]
        public IVector2D Size { get; set; } = new Vector2D(1f, 1f);
        [ProtoMember(2)]
        public IEnumerable<ISceneObject> SceneObjects => _objects;

        //  TODO: Fix SceneObjects' polymorphism
        private List<ISceneObject> _objects = new List<ISceneObject>();

        public Scene() { }

        public Scene(IVector2D size)
        {
            Size = size;
        }

        public void AddObject(ISceneObject sceneObject)
        {
            _objects.Add(sceneObject);
        }

        public void RemoveObject(ISceneObject sceneObject)
        {
            throw new System.NotImplementedException();
        }

        public ISceneObject Query(string id)
        {
            return _objects.Find(o => o.Id == id)!;
        }

        public IEnumerable<T> All<T>()
        {
            return _objects.OfType<T>();
        }

        public IEnumerable<ISceneObject> All()
        {
            return _objects;
        }
    }
}