class Platform : ICollidableData
{
    public string id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IVector2D Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float CollisionBoxWidth { get; set; }
    public float CollisionBoxHeight { get; set; }

    public Platform(float collisionBoxWidth, float collisionBoxHeight)
    {
        CollisionBoxWidth = collisionBoxWidth;
        CollisionBoxHeight = collisionBoxHeight;
    }
}