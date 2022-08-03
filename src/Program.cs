﻿// Server app startup includes:
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
            Scene.AddPlatform(p1);
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
            PhysicalObject po = new PhysicalObject(new SceneObjectData("id!!!", new Vector2D(1f, 2f)));

            po.Velocity = new Vector2D(3f, 4f);
            po.Rotation = new Vector2D(5f, 6f);
            po.Force = new Vector2D(7f, 8f);
            po.Mass = 9f;
            po.CollisionBoxWidth = 10f;
            po.CollisionBoxHeight = 11f;
            po.isGrounded = true;

            var obj = new TestType(new Vector2D(12f, 13f));
            obj.AddPlatform(new Platform(new SceneObjectData("n2", new Vector2D(1f, 2f)), 10f, 11f));
            obj.AddObject(po);

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

        static void Main(string[] args)
        {
            // MainLoop();
            TestSerialization();
        }
    }
}