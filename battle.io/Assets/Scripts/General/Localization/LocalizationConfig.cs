using UnityEngine;

[CreateAssetMenu(fileName = nameof(LocalizationConfig), menuName = EditorConstants.ConfigPath + nameof(LocalizationConfig))]
public class LocalizationConfig : UniqueObject
{
    public LocalizationData[] LocalizationDatas;
}