using System.Collections.Generic;

namespace LocoMotionServer
{
    public interface IRenderable
    {
        string TextureId { get; }
        public ISceneObject sceneObject { get; set; }
        public void Render(IRenderer renderer);
        // Texture or Animatable
    }
    public abstract class RenderableSceneObject : IRenderable
    {
        [Newtonsoft.Json.JsonIgnore]
        public ISceneObject sceneObject { get; set; }
        public string TextureId { get => sceneObject.TextureId; }

        public RenderableSceneObject(ISceneObject sceneObject)
        {
            this.sceneObject = sceneObject;
        }

        public abstract void Render(IRenderer renderer);
    }
    public interface ITexturedRectangle : IRenderable
    {
        IVector2D BottomLeft { get; set; }
        IVector2D TopRight { get; set; }
    }

    public class TexturedRectangle : RenderableSceneObject, ITexturedRectangle
    {
        public TexturedRectangle(ISceneObject sceneObject) : base(sceneObject)
        {
            BottomLeft = new Vector2D();
            TopRight = new Vector2D();
        }
        public TexturedRectangle(ISceneObject sceneObject, IVector2D bottomLeft, IVector2D topRight) : base(sceneObject)
        {
            BottomLeft = bottomLeft;
            TopRight = topRight;
        }

        public TexturedRectangle(ISceneObject sceneObject, float width, float height) : base(sceneObject)
        {
            throw new System.NotImplementedException();
        }

        public IVector2D BottomLeft { get; set; }
        public IVector2D TopRight { get; set; }

        public override void Render(IRenderer renderer)
        {
            renderer.Render(this);
        }
    }

    public interface ISpritePoint : IRenderable
    {
        float Scale { get; }
        IVector2D Position { get; }
        float Rotation { get; }
        bool FlipHorizontally { get; set; }
        bool FlipVertically { get; set; }
    }

    public class SpritePoint : RenderableSceneObject, ISpritePoint
    {
        public bool FlipHorizontally { get; set; }
        public bool FlipVertically { get; set; }
        public float Scale { get => sceneObject.Scale; }
        public IVector2D Position { get => sceneObject.Position; }
        public float Rotation { get => sceneObject.Rotation; }

        public SpritePoint(ISceneObject sceneObject) : base(sceneObject)
        {
            FlipHorizontally = false;
            FlipVertically = false;
        }

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