namespace LocoMotionServer
{
    public interface IServerConfig
    {
        int Port { get; }
        int tickIntervalMs { get; }
    }

    public class ServerConfig : IServerConfig
    {
        public int Port { get; set; } = 8080;
        public int tickIntervalMs { get; } = 500;
    }

    public class Server
    {
        private ILifecycleManager _lm;
        private IScene _scene;
        private IPhysics _physics;
        private IServerConfig _serverConfig;
        private Clcok _clock;

        public Server(ILifecycleManager lm, IScene scene, IPhysics physics, IServerConfig config)
        {
            _lm = lm;
            _scene = scene;
            _physics = physics;
            _serverConfig = config;
            _clock = new Clcok();
        }

        public void Start()
        {
            Console.WriteLine("Starting server");
            _clock.TickEvent += new TickEventHandler(this.TickEventHandler);
            _clock.Run();
        }

        public void Stop()
        {

        }

        private void TickEventHandler(object sender, TickEventArgs e)
        {
            Console.WriteLine("Processing tick");
        }
    }
}