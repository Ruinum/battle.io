using System;
using System.Collections.Generic;

[Serializable]
public struct AnimationTimeline
{
    public AnimationTimelineData Data;
    public Action<string, AnimationData> CallbackEvent;
    
    private AnimationData _data;
    private List<AnimationTimelineKey> _timelineKeys;

    public void Initialize(AnimationData data)
    {
        _timelineKeys = new List<AnimationTimelineKey>();

        AnimationTimelineKey[] timelineKeys = Data.TimelineKeys;
        for (int i = 0; i < timelineKeys.Length; i++)
        {
            _timelineKeys.Add(timelineKeys[i]);
        }

        _data = data;
        _data.OnAnimationTimeChange += OnTimeChange;
    }

    private void OnTimeChange(float currentTime)
    {
        for (int i = 0; i < _timelineKeys.Count; i++)
        {
            if (currentTime >= _timelineKeys[i].Time) InvokeKey(_timelineKeys[i]);
        }
    }

    private void InvokeKey(AnimationTimelineKey key)
    {
        switch (key.Type)
        {
            case AnimationTimelineKeyType.Event:
                key.OnKeyInvoke?.Invoke();
                UnityEngine.Debug.Log($"{key.Name}, {_data.Clip}");
                CallbackEvent?.Invoke(key.Name, _data);
                break;
        }

        _timelineKeys.Remove(key);
    }

    public List<AnimationTimelineKey> GetTimelineKeys() => _timelineKeys;
}