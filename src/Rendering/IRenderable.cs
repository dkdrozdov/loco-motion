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
        public IVector2D? BottomLeft { get; set; }
        public IVector2D? TopRight { get; set; }
        public override void Render(IRenderer renderer)
        {
            renderer.Render(this);
        }
    }

    public interface ISpritePoint : ISceneObject
    {
        IVector2D SpritePosition { get; set; }
        float Rotation { get; set; }
        bool FlipHorizontally { get; set; }
        bool FlipVertically { get; set; }
    }

    public class SpritePoint : SceneObject, ISpritePoint
    {
        public IVector2D SpritePosition { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public float Rotation { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool FlipHorizontally { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool FlipVertically { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int ResourceItemKind => throw new System.NotImplementedException();
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