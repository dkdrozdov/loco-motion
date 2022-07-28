using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LocoMotionServer
{
    class SceneData : SerializableData, ISceneData
    {
        public SceneData(Vector2D size, SceneGeometry geometry, List<ISceneObjectData> sceneObjects)
        {
            Size = size;
            Geometry = geometry;
            SceneObjects = sceneObjects;
        }
        [JsonConstructorAttribute]
        public SceneData(IVector2D size, ISceneGeometry geometry, IEnumerable<ISceneObjectData> sceneObjects)
        {
            Size = size;
            Geometry = geometry;
            SceneObjects = sceneObjects;
        }

        public IVector2D Size { get; }

        public ISceneGeometry Geometry { get; }

        public IEnumerable<ISceneObjectData> SceneObjects { get; }
    }
}