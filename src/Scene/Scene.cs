using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    [ProtoInclude(3, typeof(SceneObjectData))]
    [ProtoInclude(4, typeof(ICollidableData))]
    [ProtoInclude(5, typeof(ISceneObject))]
    public interface ISceneObjectData
    {
        [ProtoMember(1)]
        string id { get; set; }
        [ProtoMember(2)]
        IVector2D Position { get; set; }
    }
    [ProtoContract]
    [ProtoInclude(3, typeof(SceneObject))]
    public class SceneObjectData : ISceneObjectData
    {
        public SceneObjectData()
        {

        }
        public SceneObjectData(string id, IVector2D position)
        {
            this.id = id;
            Position = position;
        }
        [ProtoMember(1)]
        public string id { get; set; } = "NO_ID";
        [ProtoMember(2)]
        public IVector2D Position { get; set; } = new Vector2D();
    }

    public interface ISceneObject : ISceneObjectData, IManagableObject
    {
        ISceneObjectData Snapshot();
    }

    [ProtoContract]
    [ProtoInclude(3, typeof(Collidable))]
    public class SceneObject : SceneObjectData, ISceneObject
    {
        public SceneObject() : base()
        {

        }
        public SceneObject(string id, IVector2D position) : base(id, position)
        {
        }
        public SceneObject(ISceneObjectData data) : base(data.id, data.Position)
        {
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

        public ISceneObjectData Snapshot()
        {
            return new SceneObjectData(id, Position);
        }
    }

    public interface ISceneGeometry
    {
        public void AddPlatform(ICollidableData platform);
        IEnumerable<ICollidableData> Platforms { get; }
    }

    public interface ISceneData
    {
        IVector2D Size { get; }
        ISceneGeometry Geometry { get; }
        IEnumerable<ISceneObjectData> SceneObjects { get; }
        public void AddPlatform(ICollidableData platform);
        public void AddObject(IPhysicalObject physicalObject);
    }

    public interface IScene : ISceneData
    {
        void Add(ISceneObject o);
        void Remove(ISceneObject o);
        ISceneObject Query(string id);
        IEnumerable<T> All<T>();
        IEnumerable<ISceneObject> All();
        ISceneData Snapshot();
    }

    //  TODO: make Scene inherited from SceneData
    public class Scene : IScene
    {
        public IVector2D Size { get; set; }
        public ISceneGeometry Geometry { get; set; }
        public IEnumerable<ISceneObjectData> SceneObjects => _objects;

        private List<ISceneObject> _objects = new List<ISceneObject>();

        public Scene(IVector2D size, ISceneGeometry geometry)
        {
            Size = size;
            Geometry = geometry;
        }
        public Scene()
        {
            Size = new Vector2D();
            Geometry = new SceneGeometry();
        }
        public void LoadData(ISceneData sceneData)
        {
            Size = sceneData.Size;
            Geometry = sceneData.Geometry;
            foreach (var sceneObjectData in sceneData.SceneObjects)
            {
                if (sceneObjectData is IPhysicalObject)
                {
                    _objects.Add(new PhysicalObject((IPhysicalObject)sceneObjectData));
                }
                else
                {
                    _objects.Add(new SceneObject(sceneObjectData));
                }
            }
        }
        public void Add(ISceneObject sceneObject)
        {
            _objects.Add(sceneObject);
        }

        public void Remove(ISceneObject sceneObject)
        {
            _objects.Remove(sceneObject);
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

        public void AddPlatform(ICollidableData platform)
        {
            throw new NotImplementedException();
        }

        public void AddObject(IPhysicalObject physicalObject)
        {
            throw new NotImplementedException();
        }

        public ISceneData Snapshot()
        {
            throw new NotImplementedException();
        }
    }
}