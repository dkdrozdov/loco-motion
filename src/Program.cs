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
            //  Initializing
            PhysicalObject po = new PhysicalObject(new SceneObjectData("id!!!", new Vector2D(1f, 2f)));
            po.Velocity = new Vector2D(3f, 4f);
            po.Rotation = new Vector2D(5f, 6f);
            po.Force = new Vector2D(7f, 8f);
            po.Mass = 9f;
            po.CollisionBoxWidth = 10f;
            po.CollisionBoxHeight = 11f;
            po.isGrounded = true;

            //  Serializing to .bin
            using (var file = File.Create("po.bin"))
            {
                Serializer.Serialize(file, po);
            }

            //  Deserializing
            PhysicalObject newpo;
            using (var file = File.OpenRead("po.bin"))
            {
                newpo = Serializer.Deserialize<PhysicalObject>(file);
            }
        }

        static void Main(string[] args)
        {
            // MainLoop();
            TestSerialization();
        }
    }
}