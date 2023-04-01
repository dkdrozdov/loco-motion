namespace Server
{
  public interface IMessage
  {
    string Id { get; }
  }

  class UpdateMessage : IMessage
  {
    public string Id => "update";
    public IScene payload { get; }

    public UpdateMessage(IScene payload)
    {
      this.payload = payload;
    }
  }

  public interface INetworkManager
  {
    void broadcast(IMessage message);
  }
}