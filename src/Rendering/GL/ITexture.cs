using OpenTK.Graphics.OpenGL4;

public interface ITexture
{
    public void LoadFromFile(string path);
    public void Use(TextureUnit unit);
}