using System.IO;
using Newtonsoft.Json;

public enum ResourceItemKind
{
    Sprite,
    Texture
}

public interface IResourceItem
{
    public void InitItem();
    public string TexturePath { get; set; }
    public string Kind { get; set; }
    public byte[]? ResourceData { get; set; }
}

public class ResourceItem : IResourceItem
{
    public ResourceItem(string kind, string texturePath)
    {
        Kind = kind;
        TexturePath = texturePath;
    }
    [JsonIgnoreAttribute]
    public byte[]? ResourceData { get; set; }
    public string TexturePath { get; set; }
    public string Kind { get; set; }

    public void InitItem()
    {
        ResourceData = File.ReadAllBytes(TexturePath);
    }
}