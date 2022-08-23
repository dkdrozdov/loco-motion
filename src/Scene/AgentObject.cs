namespace LocoMotionServer
{
    class AgentObject : PhysicalObject, IPhysicalObject
    {
        public AgentObject(string id, IVector2D position, IVector2D velocity, float rotation, float mass, IVector2D bottomLeft, IVector2D topRigth)
        {
            this.Id = id;
            Position = position;
            Velocity = velocity;
            Rotation = rotation;
            Mass = mass;
        }
    }
}