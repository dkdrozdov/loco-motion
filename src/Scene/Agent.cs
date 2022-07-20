class Agent : IPhysicalObject
{
    public Agent(string id, IVector2D position, IVector2D velocity, IVector2D face, float mass, IVector2D bottomLeft, IVector2D topRigth)
    {
        this.id = id;
        Position = position;
        Velocity = velocity;
        Face = face;
        Mass = mass;
        BottomLeft = bottomLeft;
        TopRight = topRigth;
    }

    public string id { get; set; }
    public IVector2D Position { get; set; }
    public IVector2D Velocity { get; set; }
    public IVector2D Face { get; set; }
    public float Mass { get; set; }
    public IVector2D BottomLeft { get; set; }
    public IVector2D TopRight { get; set; }

    public void OnCollision(ICollisionEvent e)
    {
        throw new NotImplementedException();
    }

    public void OnCreate()
    {
        throw new NotImplementedException();
    }

    public void OnDestroy()
    {
        throw new NotImplementedException();
    }

    public void OnMove(IMoveEvent e)
    {
        throw new NotImplementedException();
    }

    public ISceneObjectData Snapshot()
    {
        throw new NotImplementedException();
    }
}