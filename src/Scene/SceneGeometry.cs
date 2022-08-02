using System.Collections.Generic;

namespace LocoMotionServer
{
    class SceneGeometry : ISceneGeometry
    {
        private List<ICollidableData> _platforms = new List<ICollidableData>();
        public IEnumerable<ICollidableData> Platforms => _platforms;

        public void AddPlatform(ICollidableData platform)
        {
            _platforms.Add(platform);
        }
        public SceneGeometry()
        {
            _platforms = new List<ICollidableData>();
        }
    }
}