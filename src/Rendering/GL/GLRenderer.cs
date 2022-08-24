using System.Collections.Generic;
using LocoMotionServer;
using OpenTK.Mathematics;

public class GLRenderer : IRenderer
{
    private Matrix4 _projection = Matrix4.CreateOrthographicOffCenter(-1.0f, 1.0f, -1.0f, 1.0f, -1.0f, 1.0f);
    private Matrix4 _view = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
    private Dictionary<string, ISceneObjectRenderer> _objectRenderers;
    private IResourcePack? _resourcePack;
    private IScene? _scene;

    public GLRenderer()
    {
        _objectRenderers = new Dictionary<string, ISceneObjectRenderer>();
    }

    public void InitResources(IResourcePack resourcePack, IScene scene)
    {
        _resourcePack = resourcePack;
        _scene = scene;

        var items = resourcePack.ResourceItems;
        foreach (var item in items)
        {
            _objectRenderers[item.TexturePath] = new SceneObjectRenderer(item.ResourceData!);
            // _objectRenderers[item.TexturePath] = new Texture(item.ResourceData!);
        }
    }

    public void OnLoad()
    {
        foreach (var item in _objectRenderers.Values)
        {

            item.OnLoad();
            item.SetProjection(_projection);
            item.SetView(_view);
        }
    }

    public void OnRender()
    {
        foreach (SceneObject sceneObject in _scene?.SceneObjects!)
        {
            sceneObject.Renderable.Render(this);
        }
    }

    public void Render(ITexturedRectangle texturedRectangle)
    {
        _objectRenderers[texturedRectangle.TextureId].OnRender(texturedRectangle);
    }

    public void Render(ISpritePoint spritePoint)
    {
        _objectRenderers[spritePoint.TextureId].OnRender(spritePoint);
    }
}