using System.Collections.Generic;

namespace LocoMotionServer
{
    public interface ITexturedRectangle
    {
        IVector2D BottomLeft { get; set; }
        IVector2D TopRight { get; set; }
        int TextureId { get; set; }
    }

    public interface ISpritePoint
    {
        IVector2D Position { get; set; }
        float Rotation { get; set; }
        bool FlipHorizontally { get; set; }
        bool FlipVertically { get; set; }
        int SpriteId { get; set; }
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