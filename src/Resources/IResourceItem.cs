using System.IO;
using System.Text.Json.Serialization;

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
    [JsonIgnore]
    public byte[]? ResourceData { get; set; }
    public string TexturePath { get; set; }
    public string Kind { get; set; }

    public void InitItem()
    {
        ResourceData = File.ReadAllBytes(TexturePath);
    }
}