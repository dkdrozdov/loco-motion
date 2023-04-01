namespace Server
{
  public class TickEventArgs
  {
    public int VirtualTimeDeltaMs { get; }
    public int RealTimeDeltaMs { get; }

    public TickEventArgs(int virtualTimeDeltaMs, int realTimeDeltaMs)
    {
      this.VirtualTimeDeltaMs = virtualTimeDeltaMs;
      RealTimeDeltaMs = realTimeDeltaMs;
    }
  }

  public delegate void TickEventHandler(IClock sender, TickEventArgs e);

  public interface IClock
  {
    event TickEventHandler? TickEvent;
    float virtualTimeMultiplier { get; set; }
    void Run();
    void Stop();
  }

  public class Clock : IClock
  {
    public event TickEventHandler? TickEvent;
    public float virtualTimeMultiplier { get; set; } = 1.0f;
    private PeriodicTimer _systemTimer;
    private bool _isRunning = false;
    private int _realFrequencyMs;

    public Clock(int realFrequencyMs = 50)
    {
      _systemTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(realFrequencyMs));
      this._realFrequencyMs = realFrequencyMs;
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
        var args = new TickEventArgs((int)(_realFrequencyMs * virtualTimeMultiplier), _realFrequencyMs);
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