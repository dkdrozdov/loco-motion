// Server app startup includes:
// - process environment variables
// - process cli args
// - pick implementations
// - load resources (e.g., scene file)
// - init scene
// - init server config
// - init server
// - run server main loop
using TestType = LocoMotionServer.Scene;
using System;
using System.IO;
using ProtoBuf;
using System.Collections.Generic;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace LocoMotionServer
{
    class MyScene
    {
        public Scene Scene;
        public MyScene()
        {
            Scene = new Scene();
            var size = new Vector2D(20, 10);
            var p1 = new Platform(3, 1);
            p1.Position = new Vector2D(3, 3);
            var o1 = new PhysicalObject();
            o1.Position = new Vector2D(3, 6);
            Scene.Size = size;
            Scene.AddObject(p1);
            Scene.AddObject(o1);
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
            //  TODO: load from file
            var loadedScene = new MyScene();
            var scene = loadedScene.Scene;
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
            var o = new PhysicalObject();
            o.Id = "id!!!";
            o.Position = new Vector2D(1f, 2f);
            o.Velocity = new Vector2D(3f, 4f);
            o.Rotation = new Vector2D(5f, 6f);
            o.Force = new Vector2D(7f, 8f);
            o.Mass = 9f;
            o.CollisionBoxWidth = 10f;
            o.CollisionBoxHeight = 11f;
            o.isGrounded = true;

            var platform = new Platform();
            platform.Id = "n2";
            platform.Position = new Vector2D(1f, 2f);
            platform.CollisionBoxHeight = 10f;
            platform.CollisionBoxWidth = 11f;

            var obj = new TestType(new Vector2D(12f, 13f));
            obj.AddObject(platform);
            obj.AddObject(o);

            //  Serializing to .bin
            using (var file = File.Create("obj.bin"))
            {
                Serializer.Serialize(file, obj);
            }

            //  Deserializing
            TestType nobj;
            using (var file = File.OpenRead("obj.bin"))
            {
                nobj = Serializer.Deserialize<TestType>(file);
            }
        }

        static void TestSerializationMini()
        {
            IList<ISceneObject> objs = new List<ISceneObject>();
            var o1 = new SceneObject();
            o1.Id = "hello, world 1";
            var o2 = new SceneObject();
            o2.Id = "hello, world 2";
            objs.Add(o1);
            objs.Add(o2);
            using (var file = File.Create("obj.bin"))
            {
                Serializer.Serialize(file, objs);
            }

            List<ISceneObject> dobjs;
            using (var file = File.OpenRead("obj.bin"))
            {
                dobjs = Serializer.Deserialize<List<ISceneObject>>(file);
            }
            dobjs.ForEach(dobj => Console.WriteLine(dobj.Id));
        }


        static void Main(string[] args)
        {
            // MainLoop();
            // TestSerialization();
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "loco-motion",
                Flags = ContextFlags.ForwardCompatible,
            };

            SceneRenderer renderer = new SceneRenderer();
            using (var window = new Window(renderer, GameWindowSettings.Default, nativeWindowSettings))
            {
                ResourceItemRenderer o = new ResourceItemRenderer();
                o.LoadTexture("resources/sprite.png");
                renderer.AddObject(o);

                window.Run();
            }
        }
    }
}