using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using LocoMotionServer;

public class LocoMotionGameWindow : GameWindow, IClock
{
    public event TickEventHandler? TickEvent;
    public float virtualTimeMultiplier { get; set; } = 1.0f;
    private int horizontalResolution;
    private int verticalResolution;
    private int viewportMax;
    private IRenderer _sceneRenderer;
    private int _updateTimeMs;
    public LocoMotionGameWindow(IRenderer renderer, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, int updateTimeMs)
    : base(gameWindowSettings, nativeWindowSettings)
    {
        _updateTimeMs = updateTimeMs;
        _sceneRenderer = renderer;
        horizontalResolution = Monitors.GetMonitorFromWindow(this).WorkArea.Size.X;
        verticalResolution = Monitors.GetMonitorFromWindow(this).WorkArea.Size.Y;
        viewportMax = horizontalResolution > verticalResolution ? verticalResolution : horizontalResolution;
    }
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
        var args = new TickEventArgs((int)(UpdateTime * 1000f * virtualTimeMultiplier), (int)(UpdateTime * 1000f));
        TickEvent?.Invoke(this, args);
        var input = KeyboardState;

        if (input.IsKeyDown(Keys.Escape))
        {
            Close();
        }
    }
    public void Stop()
    {
        Stop();
    }
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Size.X, Size.Y);

        _sceneRenderer.UpdateRatio((float)Size.X / (float)viewportMax, (float)Size.Y / (float)viewportMax);
    }
}
