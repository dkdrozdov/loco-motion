using System.IO;
using System.Text.Json;

namespace LocoMotionServer
{
    class SerializationTest
    {
        static void Main()
        {
            //  Initializing scene
            Scene scene = new Scene();
            Vector2D size = new Vector2D(10f, 10f);
            IEditableSceneData esd = new EditableSceneData(size);
            esd.AddPlatform(new Platform(1f, 2f));
            scene.LoadData(esd);

            //  Unloading scene data into .json file
            SceneData sceneData = (SceneData)scene.Snapshot();

            File.WriteAllText("SceneData.json", sceneData.Serialize());

            //  Reading file and loading scene data into new scene
            string serializedSceneData = File.ReadAllText("SceneData.json");
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            SceneData newSceneData = JsonSerializer.Deserialize<SceneData>(serializedSceneData, options)!;
            Scene newScene = new Scene();
            newScene.LoadData(newSceneData);
            //SceneData newSceneData = new SceneData();
            //newSceneData.Deserialize(serializedSceneData);
        }
    }
}