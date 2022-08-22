using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

public static class WindowManager
{
    public static void StartWindow(IRenderer renderer)
    {
        var nativeWindowSettings = new NativeWindowSettings()
        {
            Size = new Vector2i(800, 600),
            Title = "loco-motion",
            Flags = ContextFlags.ForwardCompatible,
        };

        // SceneRenderer renderer = new SceneRenderer();
        using (var window = new LocoMotionGameWindow(renderer, GameWindowSettings.Default, nativeWindowSettings))
        {
            // ResourceItemRenderer o = new ResourceItemRenderer();
            // o.LoadTexture("resources/sprite.png");
            // renderer.AddObject(o);

            window.Run();
        }
    }
}