class Vector2D : IVector2D
{
    public Vector2D(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Vector2D()
    {
        X = default(int);
        Y = default(int);
    }
    public int X { get; set; }
    public int Y { get; set; }
}