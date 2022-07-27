namespace LocoMotionServer
{
    public interface IEditableSceneData : ISceneData
    {
        public void AddPlatform(ICollidableData platform);
    }
}