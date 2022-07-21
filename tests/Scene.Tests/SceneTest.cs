class SceneTest
{
    static void Main()
    {
        SceneData testSceneData = new SceneData(
            new Vector2D(10, 10),
            new SceneGeometry(new List<Platform>() { new Platform(0f, 1f) }));

        Scene testScene = new Scene();
        testScene.LoadData(testSceneData);

        IEditableSceneData esd = new EditableSceneData(new Vector2D(10, 10));
        esd.AddPlatform(new Platform(0f, 0f));
        testScene.LoadData(esd);
    }
}