class SceneData : ISceneData
{
    public SceneData(IVector2D size, ISceneGeometry geometry)
    {
        Size = size;
        Geometry = geometry;
    }

    public IVector2D Size { get; }

    public ISceneGeometry Geometry { get; }
}