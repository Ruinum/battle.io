using Ruinum.Core.Systems;
using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    
    private TimelineInvoker _timelineInvoker;
    protected bool _isDestroyed;

    protected string _idleName = "Idle";

    private void Awake()
    {
        _timelineInvoker = new TimelineInvoker();
    }

    public void PlayAnimation(string name, float crossfade = 0)
    {
        if (_animator == null) return;

        _animator.CrossFade(name, crossfade);
        StartCoroutine(StartTimeline());
    }

    public void AddTimeline(AnimationDataConfig data)
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

        if (_animator.GetCurrentAnimatorClipInfoCount(0) <= 0) yield return new WaitForEndOfFrame();

        AnimationClip animation = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        if (animation != null)
        {
            _timelineInvoker.PlayAnimation(animation);
            TimerSystem.Singleton.StartTimer(animation.length, PlayIdle);
        }

        if (_animator.layerCount <= 1) StopCoroutine(StartTimeline());

        animation = _animator.GetCurrentAnimatorClipInfo(1)[0].clip;
        if(animation != null)
        {
            _timelineInvoker.PlayAnimation(animation);
        }
    }

    private void PlayIdle()
    {
        if (_isDestroyed) return;
        Debug.Log("Go to idle");
        _animator.CrossFade(_idleName, 0.3f);
    }

    private void Update()
    {
        _timelineInvoker.Execute();
    }

    private void OnDestroy()
    {
        _isDestroyed = true;
    }
}