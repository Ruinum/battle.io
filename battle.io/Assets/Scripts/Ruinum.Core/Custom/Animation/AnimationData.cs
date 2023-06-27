using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = nameof(AnimationData), menuName = "Ruinum/Animation/AnimationData")]
public sealed class AnimationData : ScriptableObject
{
    public AnimationClip Clip;
    public AnimationData NextAnimation;
    public AnimationTimeline _animationTimeline;

    public Action<float> OnAnimationTimeChange;

    public void Initialize()
    {
        _animationTimeline.Initialize(this);
    }

    public void SubscribeOnCallback(Action<string, AnimationData> callback)
    {
        _animationTimeline.CallbackEvent += callback;
        Debug.Log("Subscribed on callback event");
    }

    public List<AnimationTimelineKey> GetTimelineKeys() => _animationTimeline.GetTimelineKeys();
}