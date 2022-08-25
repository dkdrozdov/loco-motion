using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;

public class LocoMotionGameWindow : GameWindow
{
    public LocoMotionGameWindow(IRenderer renderer, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
        _sceneRenderer = renderer;
        horizontalResolution = Monitors.GetMonitorFromWindow(this).WorkArea.Size.X;
        verticalResolution = Monitors.GetMonitorFromWindow(this).WorkArea.Size.Y;
        viewportMax = horizontalResolution > verticalResolution ? verticalResolution : horizontalResolution;
        Console.WriteLine("horizontalResolution: {0}, verticalResolution: {1}", horizontalResolution, verticalResolution);
    }
    int horizontalResolution;
    int verticalResolution;
    int viewportMax;
    private IRenderer _sceneRenderer;
    protected override void OnLoad()
    {
        base.OnLoad();
        _sceneRenderer.OnLoad();
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit);
        _sceneRenderer.OnRender();

        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        var input = KeyboardState;

        if (input.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Size.X, Size.Y);

        _sceneRenderer.UpdateRatio((float)Size.X / (float)viewportMax, (float)Size.Y / (float)viewportMax);

        Console.WriteLine("x: {0}, y: {1}", Size.X, Size.Y);
        Console.WriteLine("rx: {0}, ry: {1}", (float)Size.X / (float)viewportMax, (float)Size.Y / (float)viewportMax);
    }
}
