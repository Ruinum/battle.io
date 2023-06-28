using UnityEngine;

[CreateAssetMenu(fileName = nameof(AnimationDatasConfig), menuName = "Ruinum/Animation/AnimationDataConfig")]
public sealed class AnimationDatasConfig : ScriptableObject
{
    public AnimationData[] AnimationDatas;
}