using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    [ProtoInclude(3, typeof(SceneObject))]
    public interface ISceneObject : IManagableObject
    {
        [ProtoMember(1)]
        string id { get; set; }
        [ProtoMember(2)]
        IVector2D Position { get; set; }
        ISceneObject Snapshot();
    }

    [ProtoContract]
    [ProtoInclude(3, typeof(Collidable))]
    public class SceneObject : ISceneObject
    {
        [ProtoMember(1)]
        public string id { get; set; } = "NO_ID";
        [ProtoMember(2)]
        public IVector2D Position { get; set; } = new Vector2D();
        public SceneObject() : base()
        {

        }
        public SceneObject(string id, IVector2D position)
        {
            this.id = id;
            Position = position;
        }
        public SceneObject(ISceneObject data)
        {
            id = data.id;
            Position = data.Position;
        }

        // TODO: Move to `ManagebleObject` class.
        public void OnCreate()
        {
            throw new NotImplementedException();
        }

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }

        public ISceneObject Snapshot()
        {
            return new SceneObject(id, Position);
        }
    }

    [ProtoContract]
    [ProtoInclude(2, typeof(SceneGeometry))]
    public interface ISceneGeometry
    {
        public void AddPlatform(Collidable platform);
        IEnumerable<Collidable> Platforms { get; }
    }

    public interface IScene
    {
        IVector2D Size { get; }
        ISceneGeometry Geometry { get; }
        IEnumerable<PhysicalObject> SceneObjects { get; }
        public void AddPlatform(Collidable platform);
        public void AddObject(PhysicalObject physicalObject);
        void Add(ISceneObject o);
        void Remove(ISceneObject o);
        ISceneObject Query(string id);
        IEnumerable<T> All<T>();
        IEnumerable<ISceneObject> All();
    }

    [ProtoContract]
    public class Scene : IScene
    {
        [ProtoMember(1)]
        public IVector2D Size { get; set; } = new Vector2D();
        [ProtoMember(2)]
        public ISceneGeometry Geometry { get; set; } = new SceneGeometry();
        //  TODO: Fix SceneObjects' polymorphism
        private List<PhysicalObject> _objects = new List<PhysicalObject>();
        [ProtoMember(3)]
        public IEnumerable<PhysicalObject> SceneObjects => _objects;

        public Scene(IVector2D size)
        {
            Size = size;
        }
        public Scene()
        {

        }
        public void LoadData(IScene sceneData)
        {
            Size = sceneData.Size;
            Geometry = sceneData.Geometry;
            foreach (var sceneObjectData in sceneData.SceneObjects)
            {
                if (sceneObjectData is PhysicalObject)
                {
                    _objects.Add(new PhysicalObject(sceneObjectData));
                }
                else
                {
                    // TODO: Fix SceneObjects
                    // _objects.Add(new SceneObject(sceneObjectData));
                }
            }
        }
        public void Add(ISceneObject sceneObject)
        {
            _objects.Add((PhysicalObject)sceneObject);
        }

        public void Remove(ISceneObject sceneObject)
        {
            _objects.Remove((PhysicalObject)sceneObject);
        }

        public ISceneObject Query(string id)
        {
            return _objects.Find(o => o.id == id)!;
        }

        public IEnumerable<T> All<T>()
        {
            return _objects.OfType<T>();
        }

        public IEnumerable<ISceneObject> All()
        {
            return _objects;
        }

        public void AddPlatform(Collidable platform)
        {
            Geometry.AddPlatform(platform);
        }

        public void AddObject(PhysicalObject physicalObject)
        {
            _objects.Add(physicalObject);
        }
    }
}