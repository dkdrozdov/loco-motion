namespace LocoMotionServer
{
    public interface IManagableObject
    {
        void OnCreate();
        void OnDestroy();
    }
}