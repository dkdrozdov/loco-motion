using System.Collections.Generic;
using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    public class SceneGeometry : ISceneGeometry
    {
        private List<Collidable> _platforms = new List<Collidable>();
        [ProtoMember(1)]
        public IEnumerable<Collidable> Platforms => _platforms;

        public void AddPlatform(Collidable platform)
        {
            _platforms.Add(platform);
        }
        public SceneGeometry()
        {
        }
    }
}