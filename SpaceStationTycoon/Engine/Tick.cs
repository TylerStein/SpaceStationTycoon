using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SpaceStationTycoon
{
    public class Tick
    {
        private Timer _timer;
        private Action<double> _elapsedCallback;
        private Action _disposedCallback;
        private double _totalTime = 0.0;
        private double _intervalMs;

        public Tick(double intervalSeconds, Action<double> elapsedCallback, Action disposedCallback) {
            _intervalMs = intervalSeconds * 1000;
            _elapsedCallback = elapsedCallback;
            _disposedCallback = disposedCallback;
            _timer = new Timer(_intervalMs);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Disposed += OnTimerDisposed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        public void OnTimerElapsed(Object sender, ElapsedEventArgs e) {
            _totalTime += _intervalMs;
             _elapsedCallback.Invoke(_totalTime);
        }

        public void OnTimerDisposed(Object sender, EventArgs e) {
            _disposedCallback.Invoke();
        }

        public void PauseTimer() {
            _timer.Stop();
        }

        public void StartTimer() {
            _timer.Start();
        }
    }
}
