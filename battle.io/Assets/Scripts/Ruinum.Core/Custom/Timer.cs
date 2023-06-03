using System;
using System.Collections.Generic;

using UnityEngine;


namespace Ruinum.Core
{
    public class Timer : IExecute
    {
        private List<Timer> _timers;        
        protected float _startingTime;
        protected float _currentTime;

        public Action<float, float> OnTimeChange;
        public Action OnTimerEnd;

        public Timer(List<Timer> timers = null, float time = 1f, Action onTimerEnd = null)
        {
            _timers = timers;
            _startingTime = time;
            _currentTime = _startingTime;
            OnTimerEnd = onTimerEnd;
        }

        public virtual void Execute()
        {
            _currentTime -= Time.deltaTime;
            OnTimeChange?.Invoke(_currentTime, _startingTime);

            if (_currentTime <= 0f) { OnTimerEnd?.Invoke(); RemoveTimer(); }
        }

        protected void RemoveTimer()
        {
            _timers.Remove(this);
        }
    }
}