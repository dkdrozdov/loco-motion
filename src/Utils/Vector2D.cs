namespace LocoMotionServer
{
    public class Vector2D : IVector2D
    {
        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector2D()
        {
            X = default(float);
            Y = default(float);
        }
        public float X { get; set; }
        public float Y { get; set; }
        // TODO@ekdrozdov: type conversions from `IVector2D` to `Vector2D` could be avoided by replacing `IVector2D` with `Vector2D` in interfaces. Should I do this?
        public static Vector2D operator +(Vector2D lhs, Vector2D rhs) => new Vector2D(lhs.X + rhs.X, lhs.Y + rhs.Y);
        public static Vector2D operator *(float scalar, Vector2D vector) => new Vector2D(scalar * vector.X, scalar * vector.Y);
    }
}