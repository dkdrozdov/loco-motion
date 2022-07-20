class SceneTest
{
    static void Main()
    {
        SceneData testSceneData = new SceneData(
            new Vector2D(10, 10),
            new SceneGeometry(new List<Platform>() { new Platform(new Vector2D(0, 0), new Vector2D(1, 1)) }));

        Scene testScene = new Scene();
        testScene.LoadData(testSceneData);

        IEditableSceneData esd = new EditableSceneData(new Vector2D(10, 10));
        esd.AddPlatform(new Platform(new Vector2D(), new Vector2D()));
        testScene.LoadData(esd);
    }
}