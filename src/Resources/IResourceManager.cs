using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace LocoMotionServer
{
    public interface IResourceManager
    {
        IScene LoadScene(string path);
        void InitRenderer(IRenderer renderer);
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
        public List<SceneObject>? SceneObjects { get; set; }
        public void Serialize()
        {
            Serializer.Serialize("resources/scenes/" + Id + "/manifest.json", this);
        }
    }

    public class SceneObjectDefaults
    {
        public SceneObjectDefaults()
        {
            ObjectIdToTextureId = new Dictionary<string, string>();
        }
        public Dictionary<string, string> ObjectIdToTextureId { get; set; }
        public void Serialize()
        {
            Serializer.Serialize("resources/sceneObjects/sceneObjectDefaults.json", this);
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
        private SceneObjectDefaults? _defaults;
        public ResourceManager()
        {
            _resourcePack = new ResourcePack();
        }
        public void InitRenderer(IRenderer renderer)
        {
            renderer.InitResources(_resourcePack!, _scene!);
        }

        public void AssignTexture(ISceneObject sceneObject)
        {
            sceneObject.Renderable.TextureId = _defaults!.ObjectIdToTextureId[sceneObject.GetType().Name];
        }

        private SceneManifest ReadSceneManifest(string path)
        {
            string jsonSceneManifest = File.ReadAllText(path + "/" + "manifest.json");
            SceneManifest? sceneManifest = JsonConvert.DeserializeObject<SceneManifest>(
                jsonSceneManifest,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            return sceneManifest!;
        }

        private ResourcePackManifest ReadResourcePackManifest(string manifestName)
        {
            string jsonResourcePackManifest = File.ReadAllText("resources/resourcePacks/" + manifestName + "/manifest.json");
            ResourcePackManifest resourcePackManifest = JsonConvert.DeserializeObject<ResourcePackManifest>(jsonResourcePackManifest)!;

            return resourcePackManifest;
        }

        private SceneObjectDefaults ReadSceneObjectDefaults()
        {
            string jsonSceneObjectDefaults = File.ReadAllText("resources/sceneObjects/sceneObjectDefaults.json");
            SceneObjectDefaults sceneObjectDefaults = JsonConvert.DeserializeObject<SceneObjectDefaults>(jsonSceneObjectDefaults)!;

            return sceneObjectDefaults;
        }

        public IScene LoadScene(string path)
        {
            SceneManifest sceneManifest = ReadSceneManifest(path);

            // loading scene dependencies
            List<ResourcePackManifest>? resourcePackManifests = new List<ResourcePackManifest>();
            foreach (var manifestName in sceneManifest?.ResourcePacks!)
            {
                ResourcePackManifest resourcePackManifest = ReadResourcePackManifest(manifestName);
                // aggregating unique resource items
                foreach (var item in resourcePackManifest.ResourceItems!)
                {
                    string fullTexturePath = manifestName + "/" + item.TexturePath;
                    item.TexturePath = fullTexturePath;
                    if (_resourcePack?.ResourceItems.Find(i => string.Equals(i.TexturePath, item.TexturePath)) == null)
                        _resourcePack?.ResourceItems.Add(item);
                }
            }

            _defaults = ReadSceneObjectDefaults();

            _scene = new Scene(sceneManifest!.Size);
            foreach (var sceneObject in sceneManifest?.SceneObjects!)
            {
                _scene.AddObject(sceneObject);
                AssignTexture(sceneObject);
            }

            foreach (var resourceItem in _resourcePack!.ResourceItems)
            {
                resourceItem.InitItem();
            }
            return _scene;
        }
    }
}