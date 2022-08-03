using System.Collections.Generic;
using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    public class SceneGeometry : ISceneGeometry
    {
        private List<CollidableData> _platforms = new List<CollidableData>();
        [ProtoMember(1)]
        public IEnumerable<CollidableData> Platforms => _platforms;

        public void AddPlatform(CollidableData platform)
        {
            _platforms.Add(platform);
        }
        public SceneGeometry()
        {
        }
    }
}