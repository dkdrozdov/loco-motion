using OpenTK.Mathematics;

public interface IResourceItemRenderer
{
    public void OnLoad();
    public void SetProjection(Matrix4 projection);
    public void SetView(Matrix4 view);
    public void Render();
}