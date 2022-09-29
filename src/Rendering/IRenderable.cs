using System.Collections.Generic;

namespace LocoMotionServer
{
    public interface IRenderable
    {
        public string TextureId { get; }
        public IVector2D Position { get; }
        public void Bind(ISceneObject data);
        public void Render(IRenderer renderer);
    }

    public interface ITexturedRectangle : IRenderable
    {
        float Width { get; }
        float Height { get; }
    }

    public class TexturedRectangle : ITexturedRectangle
    {
        public TexturedRectangle(ISceneObject sceneObject)
        {
            Bind(sceneObject);
        }
        private IBoxObject? sceneObject;
        public float Width { get => sceneObject!.BoxWidth; }
        public float Height { get => sceneObject!.BoxHeight; }
        public string TextureId => sceneObject!.TextureId;
        public IVector2D Position => sceneObject!.Position;
        public void Bind(ISceneObject data)
        {
            sceneObject = (IBoxObject)data;
        }
        public void Render(IRenderer renderer)
        {
            renderer.Render(this);
        }
    }

    public interface ISpritePoint : IRenderable
    {
        float Scale { get; }
        float Rotation { get; }
        bool FlipHorizontally { get; set; }
        bool FlipVertically { get; set; }
    }

    public class SpritePoint : ISpritePoint
    {
        private ISceneObject? sceneObject;
        public string TextureId => sceneObject!.TextureId;
        public IVector2D Position => sceneObject!.Position;
        public bool FlipHorizontally { get; set; }
        public bool FlipVertically { get; set; }
        public float Scale { get => sceneObject!.Scale; }
        public float Rotation { get => sceneObject!.Rotation; }

        public void Bind(ISceneObject data)
        {
            sceneObject = data;
        }

        public SpritePoint(ISceneObject sceneObject)
        {
            FlipHorizontally = false;
            FlipVertically = false;
        }

        public void Render(IRenderer renderer)
        {
            renderer.Render(this);
        }
    }

    public interface IAnimation
    {
        float IterationIntervalMs { get; set; }
        void StartAnimation();
        void StopAnimation();
    }

    public interface ISpriteAnimation : IAnimation
    {
        IList<int> SpriteSequence { get; }
    }

    public interface IVectorAnimation : IAnimation
    {
        void NextTick();
    }

    public interface IAnimator
    {
        void StartSpriteAnimation(ISpriteAnimation animation);
        void StartVectorAnimation(IVectorAnimation animation);
        void StopSpriteAnimation();
    }
}