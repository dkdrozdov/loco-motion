class SceneGeometry : ISceneGeometry
{

    public SceneGeometry(IEnumerable<ICollidable> platforms)
    {
        Platforms = platforms;
    }
    public SceneGeometry()
    {
        Platforms = new List<ICollidable>();
    }
    public IEnumerable<ICollidable> Platforms { get; }
}