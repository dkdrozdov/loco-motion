namespace LocoMotionServer
{
    public class TickEventArgs
    {
        int virtualTimeDelta;
        int realTimeDelta;
    }

    public delegate void TickEventHandler(object sender, TickEventArgs e);

    public class Clcok
    {
        public event TickEventHandler? TickEvent;

        public void Run()
        {
            TickEvent?.Invoke(this, new TickEventArgs());

        }
    }
}