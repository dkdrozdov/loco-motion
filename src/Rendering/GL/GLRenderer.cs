using System.Collections.Generic;
using LocoMotionServer;
using OpenTK.Mathematics;

public class GLRenderer : IRenderer
{
    private Matrix4 _projection;
    private Matrix4 _view;
    private Dictionary<string, ISceneObjectRenderer> _objectRenderers;
    private IResourcePack? _resourcePack;
    private IScene? _scene;
    private IVector2D _sceneDimensions => _scene!.Size;
    public GLRenderer()
    {
        _objectRenderers = new Dictionary<string, ISceneObjectRenderer>();
    }

    public void SetProjection(IVector2D dimensions, float normalizedWidth, float normalizedHeight)
    {
        if (dimensions.X > dimensions.Y)
            _projection = Matrix4.CreateOrthographicOffCenter(
                -dimensions.X / 2f * normalizedWidth,
                dimensions.X / 2f * normalizedWidth,
                -dimensions.X / 2f * normalizedHeight,
                dimensions.X / 2f * normalizedHeight,
                -1f, 1f);
        else
            _projection = Matrix4.CreateOrthographicOffCenter(
                -dimensions.Y / 2f * normalizedWidth,
                dimensions.Y / 2f * normalizedWidth,
                -dimensions.Y / 2f * normalizedHeight,
                dimensions.Y / 2f * normalizedHeight,
                -1f, 1f);
    }

    public void InitResources(IResourcePack resourcePack, IScene scene)
    {
        _resourcePack = resourcePack;
        _scene = scene;
        SetProjection(_sceneDimensions, 1f, 1f);
        _view = Matrix4.CreateScale(0.5f);
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
        _objectRenderers[texturedRectangle.TextureId].SetProjection(_projection);
        _objectRenderers[texturedRectangle.TextureId].SetView(_view);
        _objectRenderers[texturedRectangle.TextureId].OnRender(texturedRectangle);
    }

    public void Render(ISpritePoint spritePoint)
    {
        _objectRenderers[spritePoint.TextureId].SetProjection(_projection);
        _objectRenderers[spritePoint.TextureId].SetView(_view);
        _objectRenderers[spritePoint.TextureId].OnRender(spritePoint);
    }

    public void UpdateRatio(float width, float height)
    {
        SetProjection(new Vector2D(_sceneDimensions.X, _sceneDimensions.Y), width, height);
    }
}