using System.Collections.Generic;

namespace LocoMotionServer
{
    class EditableSceneGeometry : ISceneGeometry
    {
        private List<ICollidableData> _platforms = new List<ICollidableData>();
        public IEnumerable<ICollidableData> Platforms => _platforms;

        public void AddPlatform(ICollidableData platform)
        {
            _platforms.Add(platform);
        }
    }

    class EditableSceneData : IEditableSceneData
    {
        private EditableSceneGeometry _geometry = new EditableSceneGeometry();
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