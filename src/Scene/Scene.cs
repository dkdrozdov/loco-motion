public interface ISceneObjectData
{
    string id { get; set; }
    IVector2D Position { get; set; }
}

public interface ISceneObject : ISceneObjectData, IManagableObject
{
    ISceneObjectData Snapshot();
}

public class SceneObject : ISceneObject
{
    public string id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IVector2D Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
        throw new NotImplementedException();
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

    public ISceneData Snapshot() { throw new NotImplementedException(); }

    public IEnumerable<T> All<T>()
    {
        return _objects.OfType<T>();
    }

    public IEnumerable<ISceneObject> All()
    {
        throw new NotImplementedException();
    }
}