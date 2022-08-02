using System.Collections.Generic;

namespace LocoMotionServer
{
    class EditableSceneData : IEditableSceneData
    {
        private SceneGeometry _geometry = new SceneGeometry();
        private List<IPhysicalObject> _physicalSceneObjects = new List<IPhysicalObject>();
        public ISceneGeometry Geometry => _geometry;
        public IVector2D Size { get; private set; }
        public IEnumerable<ISceneObjectData> SceneObjects => _physicalSceneObjects;

        public EditableSceneData(IVector2D size)
        {
            Size = size;
        }

        public void AddPlatform(ICollidableData platform)
        {
            _geometry.AddPlatform(platform);
        }

        public void AddObject(IPhysicalObject physicalObject)
        {
            _physicalSceneObjects.Add(physicalObject);
        }
    }
}