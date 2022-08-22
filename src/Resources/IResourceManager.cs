using System.Collections.Generic;

namespace LocoMotionServer
{
    // TODO: finish draft.
    // It should load and validate all resources required for scene.
    // Each renderable scene object must
    //  - define assiciation with resource kind (texture or animatable).
    // Each scene object of animatable kind must
    //  - define animations it requires (it could use only animations from this definition)
    // Each scene object of animatable kind must
    //  - define texture it requires?
    public interface IResourceManager
    {
        // TODO: should load resource pack scene refers to.
        void LoadScene(SceneManifest manifest);
        void InitRenderer(IRenderer renderer);
    }

    // TODO: finish draft.
    public interface IResourceLoader
    {
        void LoadResourceItem();
    }

    public interface IResourcePack
    {

        List<IResourceItem> ResourceItems { get; }
    }

    public class ResourcePack : IResourcePack
    {
        public List<IResourceItem> ResourceItems { get; }

        public ResourcePack(params IResourceItem[] resourceItems)
        {
            ResourceItems = new List<IResourceItem>();
            AddItem(resourceItems);
        }

        public void AddItem(params IResourceItem[] items)
        {
            foreach (var item in items)
            {
                ResourceItems.Add(item);
            }
        }
    }

    public class SceneManifest
    {

    }

    public class ResourceManager : IResourceManager
    {
        private IResourcePack? _resourcePack;
        private IScene? _scene;

        // TODO: load from serialized resourcePack, not from constructor
        public ResourceManager(IResourcePack resourcePack)
        {
            _resourcePack = resourcePack;
        }
        public void InitRenderer(IRenderer renderer)
        {
            renderer.InitResources(_resourcePack!, _scene!);
        }

        public void LoadScene(SceneManifest manifest, ResourcePack resourcePack, Scene scene)
        {
            _scene = scene;
            foreach (var resourceItem in resourcePack.ResourceItems)
            {
                resourceItem.InitItem();
            }
        }

        //  TODO: implement...
        public void LoadScene(SceneManifest manifest)
        {
            throw new System.NotImplementedException();
        }
    }
}