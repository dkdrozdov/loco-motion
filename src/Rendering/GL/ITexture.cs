using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;

public interface ITexture
{
    public void OnLoad();
    public void LoadFromFile(string path);
    public void Use(TextureUnit unit);
    public float Height { get; set; }
    public float Width { get; set; }
}

public class Texture : ITexture
{
    public float Height { get; set; }
    public float Width { get; set; }
    private int _handle;
    private byte[]? _textureData;

    private void LoadData()
    {
        _handle = GL.GenTexture();

        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, _handle);

        StbImage.stbi_set_flip_vertically_on_load(1);

        ImageResult image = ImageResult.FromMemory(_textureData, ColorComponents.RedGreenBlueAlpha);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
        Height = image.Height;
        Width = image.Width;

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
    }

    public void OnLoad()
    {
        LoadData();
    }

    public Texture(float _height, float _width)
    {
        Height = _height;
        Width = _width;
    }
    public Texture(byte[] textureData)
    {
        _textureData = textureData;
        Height = 1f;
        Width = 1f;
    }
    public void Use(TextureUnit unit)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, _handle);
    }

    public void LoadFromFile(string path)
    {
        throw new System.NotImplementedException();
    }
}
