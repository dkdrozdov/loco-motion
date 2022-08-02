// Server app startup includes:
// - process environment variables
// - process cli args
// - pick implementations
// - load resources (e.g., scene file)
// - init scene
// - init server config
// - init server
// - run server main loop

using System;
using System.IO;
using ProtoBuf;

namespace LocoMotionServer
{
    class MyScene
    {
        public EditableSceneData SceneData;
        public MyScene()
        {
            var size = new Vector2D(20, 10);
            SceneData = new EditableSceneData(size);
            var p1 = new Platform(3, 1);
            p1.Position = new Vector2D(3, 3);
            var o1 = new PhysicalObject();
            o1.Position = new Vector2D(3, 6);
            SceneData.AddPlatform(p1);
            SceneData.AddObject(o1);
        }
    }
    class Program
    {
        static void MainLoop()
        {
            Console.WriteLine("Starting server application");
            int CLOCK_FREQUENCY_MS = 100;
            int SERVER_TICK_INTERVAL_MS = 500;

            var lm = new LifecycleManager();
            var loadedScene = new MyScene();
            var scene = new Scene();
            scene.LoadData(loadedScene.SceneData);
            var physics = new Physics(scene);
            var config = new ServerConfig(SERVER_TICK_INTERVAL_MS);
            var clock = new Clock(CLOCK_FREQUENCY_MS);
            var server = new Server(lm, scene, physics, config, clock);
            clock.Run();
            server.Start();
            Console.WriteLine("Server application is running, press any key to shutdown");
            Console.Read();
        }

        static void TestSerialization()
        {
            //  Initializing scene
            Vector2D size = new Vector2D(10f, 10f);
            Scene scene = new Scene(size, new SceneGeometry());

            //  Serialize to .bin
            using (var file = File.Create("scene.bin"))
            {
                Serializer.Serialize(file, scene);
            }

            //  Deserializing and loading scene into new scene
            Scene newScene = new Scene();
            using (var file = File.OpenRead("scene.bin"))
            {
                newScene = Serializer.Deserialize<Scene>(file);
            }
        }

        static void Main(string[] args)
        {
            // MainLoop();
            // TestSerialization();

            SceneObject so = new SceneObject("id!!!", new Vector2D(1f, 2f));

            //  Serialize to .bin
            using (var file = File.Create("so.bin"))
            {
                Serializer.Serialize(file, so);
            }

            //  Deserializing
            SceneObject newso;
            using (var file = File.OpenRead("so.bin"))
            {
                newso = Serializer.Deserialize<SceneObject>(file);
            }
        }
    }
}