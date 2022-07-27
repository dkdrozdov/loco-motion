using System.Collections.Generic;

class SceneGeometry : ISceneGeometry
{

    public SceneGeometry(IEnumerable<ICollidableData> platforms)
    {
        Platforms = platforms;
    }
    public SceneGeometry()
    {
        Platforms = new List<ICollidableData>();
    }
    public IEnumerable<ICollidableData> Platforms { get; }
}