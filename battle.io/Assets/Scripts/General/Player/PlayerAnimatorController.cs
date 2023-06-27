using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] AnimationData[] _animationDatas;

    private AnimationData _currentAnimationData;
    private Dictionary<AnimationClip, AnimationData> _animationClips;
    private Dictionary<AnimationData, List<TimelineEvent>> _animationEvents;

    private float _currentTime;
    private float _clipDuration;

    private void Start()
    {
        _animationEvents = new Dictionary<AnimationData, List<TimelineEvent>>();
        _animationClips = new Dictionary<AnimationClip, AnimationData>();
        for (int i = 0; i < _animationDatas.Length; i++)
        {
            _animationClips.Add(_animationDatas[i].Clip, _animationDatas[i]);
            _animationDatas[i].Initialize();
        }
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
        if (!_animationEvents.ContainsKey(data))
        {
            _animationEvents.Add(data, new List<TimelineEvent>());

            _animationEvents.TryGetValue(data, out var list2);
            var timelineKeys = data.GetTimelineKeys();
            for (int i = 0; i < timelineKeys.Count; i++)
            {
                list2.Add(new TimelineEvent(timelineKeys[i].Name));
            }

            data.SubscribeOnCallback(CallbackTimelineEvent); 
        }

        _animationEvents.TryGetValue(data, out var list);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name != name) continue;

            list[i].OnEventInvoke.Add(@event);
        }
    }

    public void UnSubscribeOnTimelineEvent(AnimationData data, string name, Action @event)
    {
        if (!_animationEvents.ContainsKey(data)) return;

        _animationEvents.TryGetValue(data, out var list);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name != name) continue;

            list[i].OnEventInvoke.Remove(@event);            
        }
    }

    private void CallbackTimelineEvent(string name, AnimationData data)
    {
        Debug.LogWarning(name);
        if (!_animationEvents.TryGetValue(data, out var events)) return;
        Debug.LogWarning(events.Count);
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].Name != name) continue;
            
            var subscribedEvents = events[i].OnEventInvoke;
            for (int j = 0; j < subscribedEvents.Count; j++)
            {
                subscribedEvents[j].Invoke();
                Debug.Log("2");
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

        _animationClips.TryGetValue(firstLayerAnimation, out var animationData1);
        _animationClips.TryGetValue(secondLayerAnimation, out var animationData2);

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
    }
}