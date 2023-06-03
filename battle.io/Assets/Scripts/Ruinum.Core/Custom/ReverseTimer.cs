using System;
using System.Collections.Generic;

using UnityEngine;


namespace Ruinum.Core
{
    public class ReverseTimer : Timer
    {
        public ReverseTimer(List<Timer> timers = null, float time = 1, Action onTimerEnd = null) : base(timers, time, onTimerEnd)
        {
            _currentTime = 0f;
        }

        public override void Execute()
        {
            _currentTime += Time.deltaTime;
            OnTimeChange?.Invoke(_currentTime, _startingTime);

            if (_currentTime >= _startingTime) { OnTimerEnd?.Invoke(); RemoveTimer(); }
        }
    }
}