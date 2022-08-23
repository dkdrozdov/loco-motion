using LocoMotionServer;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

interface ISceneObjectRenderer
{
    public void OnLoad();
    public void OnRender(ITexturedRectangle texturedRectangle);
    public void OnRender(ISpritePoint spritePoint);
    public void SetProjection(Matrix4 projection);
    public void SetView(Matrix4 view);
}

class SceneObjectRenderer : ISceneObjectRenderer
{
    private readonly float[] _vertices =
    {
            // Position         Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
    private readonly uint[] _indices =
    {
            0, 1, 3,
            1, 2, 3
        };
    ITexture? _texture;
    private IShader? _shader;
    private int _elementBufferObject;
    private int _vertexBufferObject;
    private int _vertexArrayObject;

    public SceneObjectRenderer(byte[] textureData)
    {
        _texture = new Texture(textureData);
    }

    public SceneObjectRenderer(ITexture texture)
    {
        _texture = texture;
    }

    public void OnLoad()
    {
        _texture?.OnLoad();

        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);

        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        _elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

        _shader = new Shader("src/Rendering/GL/Shaders/Shader.vert", "src/Rendering/GL/Shaders/Shader.frag");
        _shader.Use();

        var vertexLocation = _shader.GetAttribLocation("aPosition");
        GL.EnableVertexAttribArray(vertexLocation);
        GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

        var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
        GL.EnableVertexAttribArray(texCoordLocation);
        GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

        _texture?.Use(TextureUnit.Texture0);
    }

    public void OnRender(ISpritePoint spritePoint)
    {
        GL.BindVertexArray(_vertexArrayObject);

        Matrix4 model = Matrix4.Identity;
        model *= Matrix4.CreateScale(spritePoint.Scale);
        model *= Matrix4.CreateRotationZ(spritePoint.Rotation);
        model *= Matrix4.CreateTranslation(spritePoint.Position.X, spritePoint.Position.Y, 0.0f);

        // center object
        //model *= Matrix4.CreateTranslation(0.5f * Size.X, 0.5f * Size.Y, 0.0f);
        //model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation));
        //model = Matrix4.CreateTranslation(-0.5f * Size.X, -0.5f * Size.Y, 0.0f);
        //model *= Matrix4.CreateScale(Size.X, Size.Y, 1.0f);
        //model *= Matrix4.CreateTranslation(Position.X, Position.Y, 0.0f);

        _shader!.SetMatrix4("model", model);
        _texture!.Use(TextureUnit.Texture0);
        _shader!.Use();

        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void OnRender(ITexturedRectangle texturedRectangle)
    {
        throw new System.NotImplementedException();
    }

    public void SetProjection(Matrix4 projection)
    {
        _shader!.SetMatrix4("projection", projection);
    }

    public void SetView(Matrix4 view)
    {
        _shader!.SetMatrix4("view", view);
    }
}