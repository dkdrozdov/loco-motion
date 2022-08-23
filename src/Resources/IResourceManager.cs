using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
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
        void LoadScene(string path);
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
        public SceneManifest()
        {
        }

        public string? Id { get; set; }
        public List<string>? ResourcePacks { get; set; }

        // SceneObject type to SceneObject
        public List<KeyValuePair<string, SerializableSceneObject>>? SceneObjects { get; set; }
        public void Serialize(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(this, options);

            System.IO.FileInfo file = new System.IO.FileInfo(path);
            file.Directory?.Create();
            File.WriteAllText(file.FullName, jsonString);
        }
        public void Serialize()
        {
            Serialize("resources/scenes/" + Id + "/manifest.json");
        }
    }

    public class SerializableSceneObject
    {
        public Vector2D Position { get; set; } = new Vector2D();
        public string TextureId { get; set; } = "";

        public SerializableSceneObject()
        {

        }
        public SerializableSceneObject(SceneObject sceneObject)
        {
            Position = (Vector2D)sceneObject.Position;
            TextureId = sceneObject.TextureId;
        }
    }

    public class ResourcePackManifest
    {
        public ResourcePackManifest()
        {
        }

        public string? Id { get; set; }
        public List<ResourceItem>? ResourceItems { get; set; }

        public void Serialize(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(this, options);

            System.IO.FileInfo file = new System.IO.FileInfo(path);
            file.Directory?.Create();
            File.WriteAllText(file.FullName, jsonString);
        }

        public void Serialize()
        {
            Serialize("resources/resourcePacks/" + Id + "/manifest.json");
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

        public void LoadScene(string path)
        {
            string jsonSceneManifest = File.ReadAllText(path + "/" + "manifest.json");
            SceneManifest? sceneManifest = JsonSerializer.Deserialize<SceneManifest>(jsonSceneManifest);
            _scene = new Scene();
            foreach (var pair in sceneManifest?.SceneObjects!)
            {
                Type objectType = Type.GetType(pair.Key)!;
                ISceneObject sceneObject = (ISceneObject)Activator.CreateInstance(objectType)!;
                sceneObject.Position = pair.Value.Position;
                sceneObject.TextureId = pair.Value.TextureId;
                _scene.AddObject(sceneObject);
            }

            List<ResourcePackManifest>? resourcePackManifests = new List<ResourcePackManifest>();

            foreach (var manifestName in sceneManifest?.ResourcePacks!)
            {
                string jsonResourcePackManifest = File.ReadAllText("resources/resourcePacks/" + manifestName + "/manifest.json");
                ResourcePackManifest resourcePackManifest = JsonSerializer.Deserialize<ResourcePackManifest>(jsonResourcePackManifest)!;
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
        }
    }
}