using ProtoBuf;

namespace LocoMotionServer
{
    [ProtoContract]
    [ProtoInclude(1, typeof(SceneObject))]
    public class ManagableObject : IManagableObject
    {
        public ManagableObject() { }

        public void OnCreate()
        {
            // NOOP.
        }

        public void OnDestroy()
        {
            // NOOP.
        }
    }
}