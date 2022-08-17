using System.Collections.Generic;
using OpenTK.Mathematics;

public class SceneRenderer
{
    private Matrix4 _projection = Matrix4.CreateOrthographicOffCenter(-1.0f, 1.0f, -1.0f, 1.0f, -1.0f, 1.0f);
    private Matrix4 _view = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
    List<IResourceItemRenderer> renderables = new List<IResourceItemRenderer>();
    public void OnLoad()
    {
        foreach (var renderable in renderables)
        {
            renderable.OnLoad();
            renderable.SetProjection(_projection);
            renderable.SetView(_view);
        }
    }

    public void Render()
    {
        foreach (var renderable in renderables)
        {
            renderable.Render();
        }
    }

    public void AddObject(IResourceItemRenderer renderable)
    {
        renderables.Add(renderable);
    }
};