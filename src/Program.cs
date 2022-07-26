public interface IVector2D
{
    float X { get; set; }
    float Y { get; set; }
}

public interface IEditableSceneData : ISceneData
{
    public void AddPlatform(ICollidableData platform);
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

