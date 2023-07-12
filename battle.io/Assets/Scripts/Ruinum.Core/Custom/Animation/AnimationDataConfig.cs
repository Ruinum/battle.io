using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = nameof(AnimationDataConfig), menuName = EditorConstants.DataPath + nameof(AnimationDataConfig))]
public sealed class AnimationDataConfig : ScriptableObject
{
    public AnimationClip Clip;
    public Timeline Timeline;    
}