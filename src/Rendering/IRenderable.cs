using System.Collections.Generic;

namespace LocoMotionServer
{
    public interface IRenderable
    {
        string TextureId { get; }
        public void Render(IRenderer renderer);
        // TODO: add this:
        // public void Bind(IRenderer renderer, ISceneObject data);
    }
    public abstract class RenderableSceneObject : IRenderable
    {
        [Newtonsoft.Json.JsonIgnore]
        public virtual ISceneObject SceneObject { get; set; }
        public string TextureId { get => SceneObject.TextureId; }

        public RenderableSceneObject(ISceneObject sceneObject)
        {
            this.SceneObject = sceneObject;
        }

        public abstract void Render(IRenderer renderer);
    }
    public interface ITexturedRectangle : IRenderable
    {
        // TODO: remove it.
        public IBoxObject BoxObject { get; set; }
        float Width { get; }
        float Height { get; }
        // IVector2D BottomLeft { get; }
    }

    public class TexturedRectangle : RenderableSceneObject, ITexturedRectangle
    {
        public TexturedRectangle(IBoxObject boxObject) : base(boxObject)
        {
            BoxObject = boxObject;
        }

        public float Width { get => BoxObject.BoxWidth; }
        public float Height { get => BoxObject.BoxHeight; }
        public override ISceneObject SceneObject { get => BoxObject; set => BoxObject = (IBoxObject)value; }
        public IBoxObject BoxObject { get; set; }
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
        public float Scale { get => SceneObject.Scale; }
        public IVector2D Position { get => SceneObject.Position; }
        public float Rotation { get => SceneObject.Rotation; }

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