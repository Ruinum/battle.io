using System;
using System.Collections.Generic;


namespace Ruinum.Core
{
    public class TimerManager : BaseSingleton<TimerManager>, IExecute
    {
        private List<Timer> _timers = new List<Timer>();

        public void Start()
        {
            GameManager.Singleton.AddExecuteObject(this);
        }

        public Timer StartTimer(float time, Action OnTimerEnd)
        {
            var timer = new Timer(_timers, time, OnTimerEnd);
            _timers.Add(timer);

            return timer;
        }

        public Timer StartReverseTimer(float time, Action OnTimerEnd)
        {
            var timer = new ReverseTimer(_timers, time, OnTimerEnd);
            _timers.Add(timer);

            return timer;
        }

        public void Execute()
        {
            for (int i = 0; i < _timers.Count; i++)
            {
                _timers[i].Execute();
            }
        }

        public void DeleteTimer(Timer timer)
        {
            _timers.Remove(timer);
            timer = null;
        }

        private void OnDestroy()
        {
            GameManager.Singleton.RemoveExecuteObject(this);
        }
    }
}
