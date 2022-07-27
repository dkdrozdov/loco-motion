using System;

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
        public ServerConfig(int tickIntervalMs = 0)
        {
            this.tickIntervalMs = tickIntervalMs;
        }
    }

    public class Server
    {
        private ILifecycleManager _lm;
        private IScene _scene;
        private IPhysics _physics;
        private IServerConfig _serverConfig;
        private IClock _clock;
        private int _accumulatedTimeDeltaMs = 0;

        public Server(ILifecycleManager lm, IScene scene, IPhysics physics, IServerConfig config, IClock clock)
        {
            _lm = lm;
            _scene = scene;
            _physics = physics;
            _serverConfig = config;
            _clock = clock;
        }

        public void Start()
        {
            Console.WriteLine("Starting server");
            _clock.TickEvent += new TickEventHandler(this.TickEventHandler);
        }

        public void Stop()
        {
            Console.WriteLine("Stopping server");
            _clock.TickEvent -= new TickEventHandler(this.TickEventHandler);
        }

        private void TickEventHandler(IClock sender, TickEventArgs e)
        {
            _accumulatedTimeDeltaMs += e.VirtualTimeDeltaMs;
            if (_accumulatedTimeDeltaMs <= _serverConfig.tickIntervalMs)
            {
                return;
            }
            _accumulatedTimeDeltaMs = 0;
            Console.WriteLine("Server processing tick");
        }
    }
}