public interface IVector2D
{
    int X { get; set; }
    int Y { get; set; }
}

public interface ISceneObjectData
{
    string id { get; set; }
    IVector2D Position { get; set; }
}

public interface IMoveEvent
{
    IVector2D From { get; }
    IVector2D To { get; }
}

public interface ICollisionEvent
{
    IPhysicalObject CollidingObject { get; }
}

public interface ISceneObject : ISceneObjectData, IManagableObject
{
    ISceneObjectData Snapshot();
}

public interface ICollidable
{
    IVector2D BottomLeft { get; set; }
    IVector2D TopRight { get; set; }
}

public interface IPhysicalObjectData : ICollidable
{
    IVector2D Velocity { get; set; }
    IVector2D Face { get; set; }
    float Mass { get; set; }
}

public interface IPhysicalObject : ISceneObject, IPhysicalObjectData
{
    void OnMove(IMoveEvent e);
    void OnCollision(ICollisionEvent e);
}

public interface ISceneGeometry
{
    IEnumerable<ICollidable> Platforms { get; }
}

public interface ISceneData
{
    IVector2D Size { get; }
    ISceneGeometry Geometry { get; }
}

public interface IScene : ISceneData
{
    void Add(ISceneObject o);
    void Remove(ISceneObject o);
    ISceneObject Query(string id);
    ISceneData Snapshot();
}

public interface IPhysics
{
    ISceneData step(IScene scene, float dt);
}

public interface IManagableObject
{
    void OnCreate();
    void OnDestroy();
}

public interface IObjectManager
{
    T create<T>() where T : IManagableObject;
    void destroy(IManagableObject o);
}

