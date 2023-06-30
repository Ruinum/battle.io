using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = nameof(AnimationData), menuName = "Ruinum/Animation/AnimationData")]
public sealed class AnimationData : ScriptableObject
{
    public AnimationClip Clip;
    public Timeline Timeline;    
}