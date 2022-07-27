public interface IObjectManager
{
    T create<T>() where T : IManagableObject;
    void destroy(IManagableObject o);
}

