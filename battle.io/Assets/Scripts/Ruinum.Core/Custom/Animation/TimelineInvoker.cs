using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class TimelineInvoker
{
    private Dictionary<AnimationClip, Timeline> _timelines;
    private Dictionary<string, List<Action>> _subscribedEvents;
    private Dictionary<AnimationClip, byte> _playAnimations;

    private List<TimelineExecutor> _timelineExecutors;

    public TimelineInvoker()
    {
        _timelineExecutors = new List<TimelineExecutor>();

        _timelines = new Dictionary<AnimationClip, Timeline>();
        _subscribedEvents = new Dictionary<string, List<Action>>();
        _playAnimations = new Dictionary<AnimationClip, byte>();
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

    public void AddTimelines(AnimationDataConfig[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            AddTimeline(datas[i]);
        }
    }
    
    public void AddTimeline(AnimationDataConfig data)
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
        if (_timelineExecutors.Count <= 0) return;
        for (int i = 0; i < _timelineExecutors.Count; i++)
        {
            _timelineExecutors[i].Execute();
        }
    }

    public void InvokeTimelineKey(string name)
    {
        if (!_subscribedEvents.TryGetValue(name, out List<Action> events))
        {
            Debug.LogError($"There is no {name} key in Dictionary");
            return;
        }

        InvokeEvents(events);
    }

    public void PlayAnimation(AnimationClip clip)
    {
        if (!_timelines.TryGetValue(clip, out Timeline timeline))
        {
            Debug.LogWarning($"Didn't have Timeline for {clip}");
            return;
        }

        if (_playAnimations.Count >= 1) 
        {
            if (_playAnimations.TryGetValue(clip, out var @byte))
            {
                Debug.LogWarning($"Clip {clip} is already plays");
                return;
            }
        }

        TimelineExecutor executor = new TimelineExecutor(this, timeline, clip);
        executor.OnTimelineEnd += OnExecutorEnd;

        _playAnimations.Add(clip, 0);
        _timelineExecutors.Add(executor);
    }

    private void InvokeEvents(List<Action> events)
    {
        for (int i = 0; i < events.Count; i++)
        {
            events[i]?.Invoke();
        }
    }

    private void OnExecutorEnd(TimelineExecutor executor, AnimationClip clip)
    {
        _timelineExecutors.Remove(executor);
        _playAnimations.Remove(clip);
    }
}

public sealed class TimelineExecutor
{
    private TimelineInvoker _invoker;
    private AnimationClip _clip;
    private List<TimelineKey> _timelineKeys;

    private float _animationDuration;
    private float _currentTime;

    public Action<TimelineExecutor, AnimationClip> OnTimelineEnd;

    public TimelineExecutor(TimelineInvoker invoker, Timeline timeline, AnimationClip clip)
    {
        _invoker = invoker;
        _clip = clip;

        _timelineKeys = new List<TimelineKey>();
        _timelineKeys.AddRange(timeline.TimelineKeys);

        _animationDuration = _clip.length;
        _currentTime = 0;
    }

    public void Execute()
    {
        _currentTime += Time.deltaTime;
        InvokeTimelineKeys(_currentTime);

        if (_currentTime < _animationDuration) return;

        OnTimelineEnd?.Invoke(this, _clip);
    }

    private void InvokeTimelineKeys(float time)
    {
        for (int i = 0; i < _timelineKeys.Count; i++)
        {
            if (_timelineKeys[i].Time > time) continue;

            _invoker.InvokeTimelineKey(_timelineKeys[i].Name);
            _timelineKeys.Remove(_timelineKeys[i]);
        }
    }
}