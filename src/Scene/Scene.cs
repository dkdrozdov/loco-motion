using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LocoMotionServer
{
    public interface ISceneObjectData
    {
        string id { get; set; }
        IVector2D Position { get; set; }
    }

    public class SceneObjectData : ISceneObjectData
    {
        public SceneObjectData(string id, IVector2D position)
        {
            this.id = id;
            Position = position;
        }

        public string id { get; set; }
        public IVector2D Position { get; set; }
    }

    public interface ISceneObject : ISceneObjectData, IManagableObject
    {
        ISceneObjectData Snapshot();
    }

    public class SceneObject : ISceneObject
    {
        public string id { get; set; } = "NO_ID";
        public IVector2D Position { get; set; } = new Vector2D();

        public SceneObject(ISceneObjectData data)
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

        public ISceneObjectData Snapshot()
        {
            return new SceneObjectData(id, Position);
        }
    }

    public interface ISerializableData
    {
        string Serialize();
    }

    public class SerializableData : ISerializableData
    {
        public string Serialize()
        {
            return JsonSerializer.Serialize<object>(this);
        }
    }

    public interface ISceneGeometry
    {
        IEnumerable<ICollidableData> Platforms { get; }
    }

    public interface ISceneData
    {
        IVector2D Size { get; }
        ISceneGeometry Geometry { get; }
        IEnumerable<ISceneObjectData> SceneObjects { get; }
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
                _objects.Add(new SceneObject(sceneObjectData));
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

        public ISceneData Snapshot()
        {
            return new SceneData(Size, Geometry, SceneObjects);
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