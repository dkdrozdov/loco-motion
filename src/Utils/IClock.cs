using System;
using System.Threading;

namespace LocoMotionServer
{
    public class TickEventArgs
    {
        public int virtualTimeDeltaMs { get; }
        public int realTimeDeltaMs { get; }

        public TickEventArgs(int virtualTimeDeltaMs)
        {
            this.virtualTimeDeltaMs = virtualTimeDeltaMs;
        }
    }

    public delegate void TickEventHandler(IClock sender, TickEventArgs e);

    public interface IClock
    {
        public event TickEventHandler? TickEvent;
        void Run();
        void Stop();
    }

    public class Clock : IClock
    {
        public event TickEventHandler? TickEvent;
        private PeriodicTimer _systemTimer;
        private bool _isRunning = false;
        private int _frequencyMs;

        public Clock(int frequencyMs = 0)
        {
            _systemTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(frequencyMs));
            this._frequencyMs = frequencyMs;
        }

        public async void Run()
        {
            Console.WriteLine("Running clock");
            _isRunning = true;
            while (await _systemTimer.WaitForNextTickAsync())
            {
                if (!_isRunning)
                {
                    return;
                }
                var args = new TickEventArgs(_frequencyMs);
                TickEvent?.Invoke(this, args);
            }
        }

        public void Stop()
        {
            Console.WriteLine("Stopping clock");
            _isRunning = false;
        }
    }
}