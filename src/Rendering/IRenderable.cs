using System.Collections.Generic;

namespace LocoMotionServer
{
    public interface IRenderableSceneObject
    {
        // Texture or Animatable
    }

    public interface ITexturedRectangle : ISceneObject
    {
        IVector2D BottomLeft { get; set; }
        IVector2D TopRight { get; set; }
    }

    public class TexturedRectangle : SceneObject, ITexturedRectangle
    {
        public TexturedRectangle(IVector2D bottomLeft, IVector2D topRight)
        {
            BottomLeft = bottomLeft;
            TopRight = topRight;
        }

        public IVector2D BottomLeft { get; set; }
        public IVector2D TopRight { get; set; }
        public override void Render(IRenderer renderer)
        {
            renderer.Render(this);
        }
    }

    public interface ISpritePoint : ISceneObject
    {
        IVector2D SpritePosition { get; }
        bool FlipHorizontally { get; set; }
        bool FlipVertically { get; set; }
    }

    public class SpritePoint : SceneObject, ISpritePoint
    {
        public IVector2D SpritePosition => Position;
        public bool FlipHorizontally { get; set; }
        public bool FlipVertically { get; set; }
        public int ResourceItemKind;
        public override void Render(IRenderer renderer)
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