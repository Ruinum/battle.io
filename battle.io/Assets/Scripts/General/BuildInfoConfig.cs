using UnityEngine;

[CreateAssetMenu(fileName = nameof(BuildInfoConfig), menuName = EditorConstants.DataPath + nameof(BuildInfoConfig))]
public class BuildInfoConfig : ScriptableObject
{
    public BuildType BuildType = BuildType.None;
}