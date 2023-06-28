using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private AnimationData _currentAnimationData;

    private AnimationData[] _animationDatas;
    private Dictionary<AnimationClip, AnimationTimeline> _clipTimelines;
    private Dictionary<string, List<TimelineEvent>> _timelinesKeyEvents;

    private float _currentTime;
    private float _clipDuration;

    private void Awake()
    {
        _timelinesKeyEvents = new Dictionary<string, List<TimelineEvent>>();
        _clipTimelines = new Dictionary<AnimationClip, AnimationTimeline>();
    }

    public void InitializeAnimations(AnimationDatasConfig animationDatasConfig)
    {
        InitializeAnimations(animationDatasConfig.AnimationDatas);
    }

    public void InitializeAnimations(AnimationData[] animationDatas)
    {
        _animationDatas = animationDatas;
        for (int i = 0; i < _animationDatas.Length; i++)
        {
            var instance = CreateAnimationDataInstance(animationDatas[i]);

            _clipTimelines.Add(instance.Clip, instance);
            instance.Initialize();
        }
    }

    private AnimationData CreateAnimationDataInstance(AnimationData data)
    {
        if (data.ID == null || data.ID == default) data.ID = Guid.NewGuid().ToString();

        var dataInstance = ScriptableObject.CreateInstance<AnimationData>();
        dataInstance.name = data.name;
        dataInstance.InitInstance(data.Clip, data.NextAnimation, data.Timeline, data.ID);
        return dataInstance;
    }

    public void PlayAnimation(AnimationData data, float crossfade = 0)
    {
        PlayAnimation(data.ToString(), crossfade);
    }

    public void PlayAnimation(string name, float crossfade = 0)
    {
        _animator.CrossFade(name, crossfade);

        StartCoroutine(StartTimeline(name, crossfade));
    }

    public void SubscribeOnTimelineEvent(AnimationData data, string name, Action @event)
    {
        if (!_timelinesKeyEvents.ContainsKey(data.ID))
        {
            data.Initialize();
            List<TimelineEvent> dataEvents = new List<TimelineEvent>();
            _timelinesKeyEvents.Add(data.ID, dataEvents);

            var timelineKeys = data.GetTimelineKeys();
            for (int i = 0; i < timelineKeys.Count; i++)
            {
                dataEvents.Add(new TimelineEvent(timelineKeys[i].Name));
                Debug.Log($"Added an {timelineKeys[i].Name} to data {data.Clip}");
            }

            data.SubscribeOnCallback(CallbackTimelineEvent); 
        }

        _timelinesKeyEvents.TryGetValue(data.ID, out var list);
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log($"Try subscribe on TimelineEvent {list[i].Name}, {name}");

            if (list[i].Name != name && !list[i].Name.Contains(name)) continue;

            Debug.Log($"Subscribed on {list[i].Name}, {name}");

            list[i].OnEventInvoke.Add(@event);
        }
    }

    public void UnSubscribeOnTimelineEvent(AnimationData data, string name, Action @event)
    {
        if (!_timelinesKeyEvents.ContainsKey(data.ID)) return;

        _timelinesKeyEvents.TryGetValue(data.ID, out var list);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name != name) continue;

            list[i].OnEventInvoke.Remove(@event);            
        }
    }

    private void CallbackTimelineEvent(string name, AnimationData data)
    {
        Debug.LogError("Callback Timeline Event triggered");
        if (!_timelinesKeyEvents.TryGetValue(data.ID, out var events)) return;
        for (int i = 0; i < events.Count; i++)
        {
            Debug.Log($"Event name: {events[i].Name}, Invoked EventName {name}");
                    
            if (events[i].Name != name) continue;
            
            var subscribedEvents = events[i].OnEventInvoke;
            Debug.Log($"Subscribed events on {events[i].Name} {subscribedEvents.Count}");
            for (int j = 0; j < subscribedEvents.Count; j++)
            {
                subscribedEvents[j].Invoke();
                Debug.Log("Invoke event");
            }
        }
    }

    private void RefreshTimeline(AnimationClip clip)
    {
        _currentAnimationData.Initialize();
        _clipDuration = clip.length;
        _currentTime = 0f;
    }

    private IEnumerator StartTimeline(string name, float crossfade = 0)
    {
        yield return new WaitForEndOfFrame();

        _animator.CrossFade(name, crossfade);
        var firstLayerAnimation = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        var secondLayerAnimation = _animator.GetCurrentAnimatorClipInfo(1)[0].clip;

        _clipTimelines.TryGetValue(firstLayerAnimation, out var animationData1);
        _clipTimelines.TryGetValue(secondLayerAnimation, out var animationData2);

        if (animationData1 != null) _currentAnimationData = animationData1;
        if (animationData2 != null) _currentAnimationData = animationData2;
        
        if (_currentAnimationData != null)
        RefreshTimeline(firstLayerAnimation);
    }

    private void Update()
    {
        if (_currentAnimationData == null || _currentAnimationData == default) return;
        if (_currentTime >= _clipDuration) return;

        _currentTime += Time.deltaTime;
        _currentAnimationData.OnAnimationTimeChange?.Invoke(_currentTime);

        if (_currentTime >= _clipDuration && _currentAnimationData.NextAnimation != null)
        {
            PlayAnimation(_currentAnimationData.NextAnimation);
        }
    }
}

public class TimelineEvent
{
    public string Name;
    public List<Action> OnEventInvoke;

    public TimelineEvent(string name)
    {
        Name = name;
        OnEventInvoke = new List<Action>();
    }
}