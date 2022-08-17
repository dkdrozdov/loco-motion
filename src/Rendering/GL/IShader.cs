using OpenTK.Mathematics;

public interface IShader
{
    public void Use();
    public int GetAttribLocation(string attribName);
    public void SetInt(string name, int data);
    public void SetFloat(string name, float data);
    public void SetMatrix4(string name, Matrix4 data);
    public void SetVector3(string name, Vector3 data);
}