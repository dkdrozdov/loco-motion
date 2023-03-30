using ProtoBuf;

namespace Server
{
  [ProtoContract]
  [ProtoInclude(3, typeof(Vector2D))]
  public interface IVector2D
  {
    [ProtoMember(1)]
    float X { get; set; }
    [ProtoMember(2)]
    float Y { get; set; }
  }
}