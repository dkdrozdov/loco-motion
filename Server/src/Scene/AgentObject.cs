namespace Server
{
  class AgentObject : PhysicalObject, IPhysicalObject
  {
    public AgentObject() : base()
    {
      Renderable = new SpritePoint(this);
      Scale = 0.125f;
    }
    [Newtonsoft.Json.JsonIgnore]
    public override IRenderable Renderable { get; set; }
  }
}