namespace LocoMotionServer
{
    class AgentObject : PhysicalObject, IPhysicalObject
    {
        public AgentObject() : base()
        {
            Renderable = new SpritePoint(this);
        }
        [Newtonsoft.Json.JsonIgnore]
        public override IRenderable Renderable { get; set; }
        public override string TextureId { get => "resources/resourcePacks/Common/agent.png"; }
    }
}