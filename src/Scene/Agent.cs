using System;

namespace LocoMotionServer
{
    class Agent : PhysicalObject, IPhysicalObject
    {
        public Agent(string id, IVector2D position, IVector2D velocity, IVector2D face, float mass, IVector2D bottomLeft, IVector2D topRigth)
        {
            this.id = id;
            Position = position;
            Velocity = velocity;
            Rotation = face;
            Mass = mass;
        }

        public void OnCollision(ICollisionEvent e)
        {
            throw new NotImplementedException();
        }
    }
}