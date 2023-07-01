using System;
using System.Collections.Generic;
using UnityEngine;

public class TimelineInvoker
{
    private Dictionary<AnimationClip, Timeline> _timelines;
    private Dictionary<string, List<Action>> _subscribedEvents;

    private List<TimelineKey> _timelineKeys;
    private float _animationDuration;
    private float _currentTime;

    private bool _playAnimation;

    public TimelineInvoker()
    {
        _timelines = new Dictionary<AnimationClip, Timeline>();
        _subscribedEvents = new Dictionary<string, List<Action>>();
        _timelineKeys = new List<TimelineKey>();
    }

    public void SubscribeOnTimelineEvent(string name, Action @event)
    {
        if (!_subscribedEvents.ContainsKey(name)) _subscribedEvents.Add(name, new List<Action>());
        _subscribedEvents.TryGetValue(name, out List<Action> events);
        events.Add(@event);
    }

    public void UnSubscribeOnTimelineEvent(string name, Action @event)
    {
        if (!_subscribedEvents.ContainsKey(name))
        {
            Debug.LogError($"No one event with {name} is finded");
            return;
        }

        _subscribedEvents.TryGetValue(name, out List<Action> events);
        events.Remove(@event);
    }

    public void AddTimelines(AnimationData[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            AddTimeline(datas[i]);
        }
    }
    
    public void AddTimeline(AnimationData data)
    {
        if (_timelines.ContainsKey(data.Clip))
        {
            Debug.Log($"Clip {data.Clip} is already added to Dictionary");
            return;
        }

        _timelines.Add(data.Clip, data.Timeline);
    }

    public void Execute()
    {
        if (!_playAnimation) return;

        _currentTime += Time.deltaTime;
        InvokeTimelineKeys(_currentTime);

        if (_currentTime < _animationDuration) return; 

        _currentTime = 0;
        _animationDuration = 0;
        _playAnimation = false;
    }

    public void PlayAnimation(AnimationClip clip)
    {
        if (!_timelines.TryGetValue(clip, out Timeline timeline))
        {
            Debug.LogError($"Didn't have Timeline for {clip}");
            return;
        }

        _timelineKeys.Clear();
        _timelineKeys.AddRange(timeline.TimelineKeys);
        _currentTime = 0;
        _animationDuration = clip.length;
        _playAnimation = true;
    }

    private void InvokeTimelineKeys(float time)
    {
        for (int i = 0; i < _timelineKeys.Count; i++)
        {
            if (_timelineKeys[i].Time > time) continue;
            
            if (!_subscribedEvents.TryGetValue(_timelineKeys[i].Name, out List<Action> events))
            {
                Debug.LogError($"There is no {_timelineKeys[i].Name} key in Dictionary");
                continue;
            }

            InvokeEvents(events);
            _timelineKeys.Remove(_timelineKeys[i]);
        }
    }

    private void InvokeEvents(List<Action> events)
    {
        for (int i = 0; i < events.Count; i++)
        {
            events[i]?.Invoke();
        }
    }
}