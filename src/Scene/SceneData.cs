class SceneData : ISceneData
{
    public SceneData(IVector2D size, ISceneGeometry geometry, IEnumerable<ISceneObjectData> sceneObjects)
    {
        Size = size;
        Geometry = geometry;
        SceneObjects = sceneObjects;
    }

    public IVector2D Size { get; }

    public ISceneGeometry Geometry { get; }

    public IEnumerable<ISceneObjectData> SceneObjects { get; }
}