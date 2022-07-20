class Scene : IScene
{
    public IVector2D Size { get; set; }
    public ISceneGeometry Geometry { get; set; }
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
}