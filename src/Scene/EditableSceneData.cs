using System;
using System.Collections.Generic;

class EditableSceneGeometry : ISceneGeometry
{
    private List<ICollidableData> _platforms = new List<ICollidableData>();
    public IEnumerable<ICollidableData> Platforms => _platforms;

    public void AddPlatform(ICollidableData platform)
    {
        _platforms.Add(platform);
    }
}

class EditableSceneData : IEditableSceneData
{
    private EditableSceneGeometry _geometry = new EditableSceneGeometry();
    public ISceneGeometry Geometry => _geometry;
    public IVector2D Size { get; private set; }

    public IEnumerable<ISceneObjectData> SceneObjects { get; }

    public EditableSceneData(EditableSceneGeometry geometry, IVector2D size, IEnumerable<ISceneObjectData> sceneObjects)
    {
        _geometry = geometry;
        Size = size;
        SceneObjects = sceneObjects;
    }
    public EditableSceneData(IVector2D size)
    {
        Size = size;
        SceneObjects = new List<ISceneObjectData>();
    }

    public void AddPlatform(ICollidableData platform)
    {
        _geometry.AddPlatform(platform);
    }

    public void AddPlatform(ICollidable platform)
    {
        throw new NotImplementedException();
    }

}