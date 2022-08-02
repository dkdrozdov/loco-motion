using System.Collections.Generic;

namespace LocoMotionServer
{
    public class SceneData : ISceneData
    {
        public SceneData()
        {

        }
        public SceneData(IVector2D size, ISceneGeometry geometry)
        {
            Size = size;
            Geometry = geometry;
        }
        public SceneData(IVector2D size)
        {
            Size = size;
        }
        public IVector2D Size { get; } = new Vector2D();

        public ISceneGeometry Geometry { get; } = new SceneGeometry();
        private List<IPhysicalObject> _physicalSceneObjects = new List<IPhysicalObject>();
        public IEnumerable<ISceneObjectData> SceneObjects => _physicalSceneObjects;
        public void AddPlatform(ICollidableData platform)
        {
            Geometry.AddPlatform(platform);
        }

        public void AddObject(IPhysicalObject physicalObject)
        {
            _physicalSceneObjects.Add(physicalObject);
        }
    }
}