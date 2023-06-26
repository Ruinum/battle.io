using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = nameof(AnimationData), menuName = "Ruinum/Animation/AnimationData")]
public sealed class AnimationData : ScriptableObject
{
    public AnimationClip Clip;
    public AnimationTimeline AnimationTimeline;

    private float _animationDuration;
    
    public Action<float> OnAnimationTimeChange;

    private void Initialize()
    {
        _animationDuration = Clip.length;
        AnimationTimeline.Initialize(_animationDuration, this);
    }

    public void Play(Animator animator)
    {
        
    }
}