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
            var o1 = new AgentObject();
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
            int CLOCK_FREQUENCY_MS = 20;
            int SERVER_TICK_INTERVAL_MS = 40;

            double renderFrequency = 60;

            var renderer = new GLRenderer();
            var resourceManager = new ResourceManager();
            var lm = new LifecycleManager();
            var scene = resourceManager.LoadScene("resources/scenes/TestScene");
            var physics = new Physics(scene);
            var config = new ServerConfig(SERVER_TICK_INTERVAL_MS);
            var window = WindowManager.CreateWindow(renderer, CLOCK_FREQUENCY_MS, renderFrequency);
            // var clock = new Clock(CLOCK_FREQUENCY_MS);
            var server = new Server(lm, scene, physics, config, window);
            resourceManager.InitRenderer(renderer);
            // clock.Run();
            server.Start();
            WindowManager.StartWindow(window);
            Console.WriteLine("Server application is running, press any key to shutdown");
            Console.Read();
        }
        static void TestSerialization()
        {
            //  Initializing
            var o = new AgentObject();
            o.Id = "id!!!";
            o.Position = new Vector2D(1f, 2f);
            o.Velocity = new Vector2D(3f, 4f);
            o.Rotation = 5f;
            o.Force = new Vector2D(7f, 8f);
            o.Mass = 9f;
            o.BoxWidth = 10f;
            o.BoxHeight = 11f;
            o.isGrounded = true;

            var platform = new Platform();
            platform.Id = "n2";
            platform.Position = new Vector2D(1f, 2f);
            platform.BoxHeight = 10f;
            platform.BoxWidth = 11f;

            var obj = new TestType(new Vector2D(12f, 13f));
            obj.AddObject(platform);
            obj.AddObject(o);

            //  Serializing to .bin
            using (var file = File.Create("obj.bin"))
            {
                ProtoBuf.Serializer.Serialize(file, obj);
            }

            //  Deserializing
            TestType nobj;
            using (var file = File.OpenRead("obj.bin"))
            {
                nobj = ProtoBuf.Serializer.Deserialize<TestType>(file);
            }
        }

        static void TestSerializationMini()
        {
            IList<ISceneObject> objs = new List<ISceneObject>();
            var o1 = new Cat();
            o1.Id = "hello, world 1";
            var o2 = new FlippedCat();
            o2.Id = "hello, world 2";
            objs.Add(o1);
            objs.Add(o2);
            using (var file = File.Create("obj.bin"))
            {
                ProtoBuf.Serializer.Serialize(file, objs);
            }

            List<ISceneObject> dobjs;
            using (var file = File.OpenRead("obj.bin"))
            {
                dobjs = ProtoBuf.Serializer.Deserialize<List<ISceneObject>>(file);
            }
            dobjs.ForEach(dobj => Console.WriteLine(dobj.Id));
        }

        static void InitResources()
        {
            AgentObject agent1 = new AgentObject();
            agent1.Position = new Vector2D(0.3f, 0.4f);

            AgentObject agent2 = new AgentObject();
            agent2.Position = new Vector2D(-0.3f, 0.0f);

            AgentObject agent3 = new AgentObject();
            agent3.Position = new Vector2D(0.2f, -0.2f);

            Platform platform1 = new Platform(0.6f, 0.1f);
            platform1.Position = new Vector2D(0.3f, 0.2f);

            Platform platform2 = new Platform(0.4f, 0.1f);
            platform2.Position = new Vector2D(-0.3f, -0.3f);

            // Init and serialize TestScene
            SceneManifest testScene = new SceneManifest();
            testScene.Id = "TestScene";
            testScene.Size.X = 1.5f;
            testScene.Size.Y = 1.5f;
            testScene.ResourcePacks = new List<string>();
            testScene.ResourcePacks.Add("Common");
            testScene.SceneObjects = new List<SceneObject>();
            testScene.SceneObjects.Add(agent1);
            testScene.SceneObjects.Add(agent2);
            testScene.SceneObjects.Add(agent3);
            testScene.SceneObjects.Add(platform1);
            testScene.SceneObjects.Add(platform2);
            testScene.Serialize();

            // Init and serialize resource packs
            ResourcePackManifest common = new ResourcePackManifest();
            common.Id = "Common";
            common.ResourceItems = new List<ResourceItem>();
            common.ResourceItems.Add(new ResourceItem(ResourceItemKind.Sprite.ToString(), "platform.png"));
            common.ResourceItems.Add(new ResourceItem(ResourceItemKind.Sprite.ToString(), "agent.png"));
            common.Serialize();

            // Init and serialize scene object defaults
            SceneObjectDefaults defaults = new SceneObjectDefaults();
            defaults.ObjectIdToTextureId.Add(typeof(AgentObject).Name, "Common/agent.png");
            defaults.ObjectIdToTextureId.Add(typeof(Platform).Name, "Common/platform.png");
            defaults.Serialize();
        }

        static void TestResources()
        {
            IRenderer renderer = new GLRenderer();
            ResourceManager resourceManager = new ResourceManager();
            resourceManager.LoadScene("resources/scenes/TestScene");
            resourceManager.InitRenderer(renderer);

            // WindowManager.StartWindow(renderer);
        }

        static void Main(string[] args)
        {
            InitResources();
            MainLoop();
        }
    }
}