using System.Collections.Generic;
using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    [ProtoInclude(1, typeof(SceneGeometry))]
    public interface ISceneGeometry
    {
        public void AddPlatform(CollidableObject platform);
        IEnumerable<CollidableObject> Platforms { get; }
    }

    [ProtoContract]
    public class SceneGeometry : ISceneGeometry
    {
        [ProtoMember(1)]
        public IEnumerable<CollidableObject> Platforms => _platforms;

        private List<CollidableObject> _platforms = new List<CollidableObject>();


        public SceneGeometry() { }

        public void AddPlatform(CollidableObject platform)
        {
            _platforms.Add(platform);
        }
    }
}