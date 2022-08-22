using LocoMotionServer;

public interface IRenderer
{
    public void InitResources(IResourcePack resourcePack, IScene scene);
    public void OnLoad();
    public void OnRender();
    public void Render(ITexturedRectangle texturedRectangle);
    public void Render(ISpritePoint spritePoint);
}
