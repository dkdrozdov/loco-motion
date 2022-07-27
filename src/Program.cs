// Server app startup includes:
// - process environment variables
// - process cli args
// - pick implementations
// - load resources (e.g., scene file)
// - init scene
// - init server config
// - init server
// - run server main loop

namespace LocoMotionServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var lm = new LifecycleManager();
            var scene = new Scene();
            var physics = new Physics(scene);
            var config = new ServerConfig();
            var server = new Server(lm, scene, physics, config);
            server.Start();
        }
    }
}