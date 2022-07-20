class Platform : ICollidable
{
    public IVector2D BottomLeft { get; set; }
    public IVector2D TopRight { get; set; }
    public Platform(IVector2D bottomLeft, IVector2D topRight)
    {
        BottomLeft = bottomLeft;
        TopRight = topRight;
    }
}