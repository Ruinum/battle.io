using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private TimelineInvoker _timelineInvoker;
    private AnimationClip _lastAnimation;

    private void Awake()
    {
        _timelineInvoker = new TimelineInvoker();
    }

    public void PlayAnimation(string name, float crossfade = 0)
    {
        _animator.CrossFade(name, crossfade);
        StartCoroutine(StartTimeline());
    }

    public void AddTimeline(AnimationData data)
    {
        _timelineInvoker.AddTimeline(data);
    }

    public void SubscribeOnTimelineEvent(string name, Action @event)
    {
        _timelineInvoker.SubscribeOnTimelineEvent(name, @event);
    }

    public void UnSubscribeOnTimeline(string name, Action @event)
    {
        _timelineInvoker.UnSubscribeOnTimelineEvent(name, @event);
    }

    private IEnumerator StartTimeline()
    {
        yield return new WaitForEndOfFrame();

        AnimationClip animation = null;

        if (_animator.GetCurrentAnimatorClipInfoCount(0) >= 1)
        {
            animation = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            _lastAnimation = animation;
        }

        if (animation != null) _timelineInvoker.PlayAnimation(animation);
        else
        {
            Debug.LogWarning($"Can't take animation from 0 layer of {_animator}, {this}");
            if (_lastAnimation != null) _timelineInvoker.PlayAnimation(_lastAnimation);
        }
    }

    private void Update()
    {
        _timelineInvoker.Execute();
    }
}