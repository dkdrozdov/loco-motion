using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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
        IScene LoadScene(string path);
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

    public static class Serializer
    {
        public static void Serialize(string path, object o)
        {
            string jsonString = JsonConvert.SerializeObject(o, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            System.IO.FileInfo file = new System.IO.FileInfo(path);
            file.Directory?.Create();
            File.WriteAllText(file.FullName, jsonString);
        }
    }

    public class SceneManifest
    {
        public SceneManifest()
        {
        }

        public string? Id { get; set; }
        public IVector2D Size { get; set; } = new Vector2D();
        public List<string>? ResourcePacks { get; set; }

        // SceneObject type to SceneObject
        public List<SceneObject>? SceneObjects { get; set; }
        public void Serialize()
        {
            Serializer.Serialize("resources/scenes/" + Id + "/manifest.json", this);
        }
    }

    public class ResourcePackManifest
    {
        public ResourcePackManifest()
        {
        }

        public string? Id { get; set; }
        public List<ResourceItem>? ResourceItems { get; set; }

        public void Serialize()
        {
            Serializer.Serialize("resources/resourcePacks/" + Id + "/manifest.json", this);
        }
    }

    public class ResourceManager : IResourceManager
    {
        private IResourcePack? _resourcePack;
        private IScene? _scene;

        // TODO: load from serialized resourcePack, not from constructor
        public ResourceManager()
        {
            _resourcePack = new ResourcePack();
        }
        public void InitRenderer(IRenderer renderer)
        {
            renderer.InitResources(_resourcePack!, _scene!);
        }

        public IScene LoadScene(string path)
        {
            string jsonSceneManifest = File.ReadAllText(path + "/" + "manifest.json");
            SceneManifest? sceneManifest = JsonConvert.DeserializeObject<SceneManifest>(
                jsonSceneManifest,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

            _scene = new Scene(sceneManifest!.Size);
            foreach (var sceneObject in sceneManifest?.SceneObjects!)
            {
                _scene.AddObject(sceneObject);
            }

            List<ResourcePackManifest>? resourcePackManifests = new List<ResourcePackManifest>();

            foreach (var manifestName in sceneManifest?.ResourcePacks!)
            {
                string jsonResourcePackManifest = File.ReadAllText("resources/resourcePacks/" + manifestName + "/manifest.json");
                ResourcePackManifest resourcePackManifest = JsonConvert.DeserializeObject<ResourcePackManifest>(jsonResourcePackManifest)!;
                foreach (var item in resourcePackManifest.ResourceItems!)
                {
                    string fullTexturePath = "resources/resourcePacks/" + manifestName + "/" + item.TexturePath;
                    item.TexturePath = fullTexturePath;
                    if (_resourcePack?.ResourceItems.Find(i => string.Equals(i.TexturePath, item.TexturePath)) == null)
                        _resourcePack?.ResourceItems.Add(item);
                }
            }

            foreach (var resourceItem in _resourcePack!.ResourceItems)
            {
                resourceItem.InitItem();
            }
            return _scene;
        }
    }
}