using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace LocoMotionServer
{
    public interface ISceneObject : IManagableObject
    {
        string id { get; set; }
        IVector2D Position { get; set; }
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(CollidableObject))]
    public class SceneObject : ManagableObject, ISceneObject
    {
        [ProtoMember(2)]
        public string id { get; set; } = "NO_ID";
        [ProtoMember(3)]
        public IVector2D Position { get; set; } = new Vector2D();

        public SceneObject() : base() { }

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
    }

    public interface ICollidableObject : ISceneObject
    {
        public float CollisionBoxWidth { get; set; }
        public float CollisionBoxHeight { get; set; }
        void OnCollision(ICollisionEvent e);
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(PhysicalObject))]
    [ProtoInclude(2, typeof(Platform))]
    public class CollidableObject : SceneObject, ICollidableObject
    {
        [ProtoMember(3)]
        public float CollisionBoxWidth { get; set; }
        [ProtoMember(4)]
        public float CollisionBoxHeight { get; set; }

        public CollidableObject() : base() { }

        public CollidableObject(ISceneObject data) : base(data) { }

        public CollidableObject(ISceneObject data, float width, float height) : base(data)
        {
            CollisionBoxWidth = width;
            CollisionBoxHeight = height;
        }

        public void OnCollision(ICollisionEvent e)
        {
            // Noop.
        }
    }

    public interface IPhysicalObject : ICollidableObject
    {
        IVector2D Velocity { get; set; }
        IVector2D Rotation { get; set; }
        IVector2D Force { get; set; }
        IVector2D Momentum { get; }
        float Mass { get; set; }
        bool isGrounded { get; set; }
        void OnMove(IMoveEvent e);
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(AgentObject))]
    public class PhysicalObject : CollidableObject, IPhysicalObject
    {
        [ProtoMember(2)]
        public IVector2D Velocity { get; set; } = new Vector2D();
        [ProtoMember(3)]
        public IVector2D Rotation { get; set; } = new Vector2D();
        [ProtoMember(4)]
        public IVector2D Force { get; set; } = new Vector2D();
        [ProtoMember(5)]
        public float Mass { get; set; } = 1.0f;
        [ProtoMember(6)]
        public bool isGrounded { get; set; } = false;

        public IVector2D Momentum => Mass * (Vector2D)Velocity;

        public PhysicalObject() : base() { }

        public PhysicalObject(ISceneObject data) : base(data) { }

        public void OnMove(IMoveEvent e)
        {
            Console.WriteLine($"New position ({e.To.X}, {e.To.Y})");
        }
    }

    public interface IScene
    {
        IVector2D Size { get; }
        ISceneGeometry Geometry { get; }
        IEnumerable<PhysicalObject> SceneObjects { get; }
        public void AddPlatform(CollidableObject platform);
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
        [ProtoMember(3)]
        public IEnumerable<PhysicalObject> SceneObjects => _objects;

        //  TODO: Fix SceneObjects' polymorphism
        private List<PhysicalObject> _objects = new List<PhysicalObject>();

        public Scene() { }

        public Scene(IVector2D size)
        {
            Size = size;
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

        public void AddPlatform(CollidableObject platform)
        {
            Geometry.AddPlatform(platform);
        }

        public void AddObject(PhysicalObject physicalObject)
        {
            _objects.Add(physicalObject);
        }
    }
}