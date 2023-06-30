using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private TimelineInvoker _timelineInvoker;

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
       
        var firstLayerAnimation = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;

        if (firstLayerAnimation != null) _timelineInvoker.PlayAnimation(firstLayerAnimation);
    }

    private void Update()
    {
        _timelineInvoker.Execute();
    }
}