class EditableSceneGeometry : ISceneGeometry
{
  private List<ICollidable> _platforms = new List<ICollidable>();
  public IEnumerable<ICollidable> Platforms => _platforms;

  public void AddPlatform(ICollidable platform)
  {
    _platforms.Add(platform);
  }
}

class EditableSceneData : IEditableSceneData
{
  private EditableSceneGeometry _geometry = new EditableSceneGeometry();
  public ISceneGeometry Geometry => _geometry;
  public IVector2D Size { get; private set; }

  public EditableSceneData(IVector2D size)
  {
    Size = size;
  }

  public void AddPlatform(ICollidable platform)
  {
    _geometry.AddPlatform(platform);
  }
}