using System.IO;

public enum ResourceItemKind
{
    Sprite
}

public interface IResourceItem
{
    public void InitItem();
    public string TexturePath { get; set; }
    public ResourceItemKind Kind { get; set; }
    public byte[]? ResourceData { get; set; }
}

public class ResourceItem : IResourceItem
{
    public ResourceItem(ResourceItemKind kind, string texturePath)
    {
        Kind = kind;
        TexturePath = texturePath;
    }
    public byte[]? ResourceData { get; set; }
    public string TexturePath { get; set; }
    public ResourceItemKind Kind { get; set; }

    public void InitItem()
    {
        ResourceData = File.ReadAllBytes(TexturePath);
    }
}