using System;

namespace LocoMotionServer
{
    class Platform : ICollidableData
    {
        public string id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IVector2D Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float CollisionBoxWidth { get; set; }
        public float CollisionBoxHeight { get; set; }
        IVector2D ISceneObjectData.Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Platform(float collisionBoxWidth, float collisionBoxHeight)
        {
            CollisionBoxWidth = collisionBoxWidth;
            CollisionBoxHeight = collisionBoxHeight;
        }
    }
}