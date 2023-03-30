namespace Server
{
  public interface IManagableObject
  {
    void OnCreate();
    void OnDestroy();
  }

  public class ManagableObject : IManagableObject
  {
    public ManagableObject() { }

    public void OnCreate()
    {
      // NOOP.
    }

    public void OnDestroy()
    {
      // NOOP.
    }
  }
}