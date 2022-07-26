class SceneTest
{
    static void Main()
    {
        Scene testScene = new Scene();
        Vector2D size = new Vector2D(10f, 10f);

        IEditableSceneData esd = new EditableSceneData(size);
        esd.AddPlatform(new Platform(1f, 2f));
        testScene.LoadData(esd);
    }
}