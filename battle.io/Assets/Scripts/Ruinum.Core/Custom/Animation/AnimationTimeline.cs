using System;
using System.Collections.Generic;

[Serializable]
public sealed class AnimationTimeline
{
    public AnimationTimelineData Data;

    private Dictionary<float, AnimationTimelineKey> _timelineKeys;
    private float _duration;

    public void Initialize(float duration, AnimationData data)
    {
        _timelineKeys = new Dictionary<float, AnimationTimelineKey>();
        _duration = duration;

        AnimationTimelineKey[] timelineKeys = Data.TimelineKeys;
        for (int i = 0; i < timelineKeys.Length; i++)
        {
            timelineKeys[i].SetID(i);
            _timelineKeys.Add(timelineKeys[i].KeyTime, timelineKeys[i]);
        }

        data.OnAnimationTimeChange += OnTimeChange;
    }

    private void OnTimeChange(float currentTime)
    {
        if (!_timelineKeys.TryGetValue(currentTime, out AnimationTimelineKey timelineKey)) return;        
        
    }
}