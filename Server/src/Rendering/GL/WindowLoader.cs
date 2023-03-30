using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

public static class WindowManager
{
    public static LocoMotionGameWindow CreateWindow(IRenderer renderer, int updateTimeMs, double renderFrequency)
    {
        var nativeWindowSettings = new NativeWindowSettings()
        {
            Size = new Vector2i(800, 600),
            Title = "loco-motion",
            Flags = ContextFlags.ForwardCompatible,
        };
        var gameWindowSettings = new GameWindowSettings()
        {
            UpdateFrequency = 1f / ((double)updateTimeMs / 1000f),
            RenderFrequency = renderFrequency
        };
        var window = new LocoMotionGameWindow(renderer, gameWindowSettings, nativeWindowSettings, updateTimeMs);
        return window;
    }
    public static void StartWindow(LocoMotionGameWindow window)
    {
        window.Run();
    }
}