using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = nameof(AnimationData), menuName = "Ruinum/Animation/AnimationData")]
public sealed class AnimationData : ScriptableObject
{
    public AnimationClip Clip;
    public AnimationData NextAnimation;
    public AnimationTimeline Timeline;

    public Action<float> OnAnimationTimeChange;

    public void InitInstance(AnimationClip clip, AnimationData nextAnimationData, AnimationTimeline timeline, string id)
    {
        Clip = clip;
        NextAnimation = nextAnimationData;
        Timeline = timeline;
    }

    public void Initialize()
    {
        Timeline.Initialize(this);
    }

    public void SubscribeOnCallback(Action<string, AnimationData> callback)
    {
        Timeline.CallbackEvent += callback;
        Debug.Log("Subscribed on callback event");
    }

    public List<AnimationTimelineKey> GetTimelineKeys() => Timeline.GetTimelineKeys();
}