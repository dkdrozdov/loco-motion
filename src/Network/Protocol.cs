namespace LocoMotionServer
{
    public interface IMessage
    {
        string Id { get; }
    }

    class UpdateMessage : IMessage
    {
        public string Id => "update";
        public ISceneData payload { get; }

        public UpdateMessage(ISceneData payload)
        {
            this.payload = payload;
        }
    }

    public interface INetworkManager
    {
        void broadcast(IMessage message);
    }
}