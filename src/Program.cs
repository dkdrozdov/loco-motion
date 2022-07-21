public interface IVector2D
{
    int X { get; set; }
    int Y { get; set; }
}

public interface IEditableSceneData : ISceneData
{
    public void AddPlatform(ICollidable platform);
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

