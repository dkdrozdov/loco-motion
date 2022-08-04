namespace LocoMotionServer
{
    // TODO: finish draft.
    // It should load and validate all resources required for scene.
    // Each renderable scene object must
    //  - define assiciation with resource kind (texture or animatable).
    // Each scene object of animatable kind must
    //  - define animations it requires (it could use only animations from this definition)
    // Each scene object of animatable kind must
    //  - define texture it requires?
    public interface IResourceManager
    {
        // TODO: should load resource pack scene refers to.
        void LoadScene(string serializedScene);
    }

    // TODO: finish draft.
    public interface IResourceLoader
    {
        void LoadResourceItem();
    }
}