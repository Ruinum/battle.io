using UnityEngine;

[CreateAssetMenu(fileName = nameof(LocalizationConfig), menuName = EditorConstants.ConfigPath + nameof(LocalizationConfig))]
public class LocalizationConfig : UniqueObject
{
    public LocalizationData[] LocalizationDatas;
}

[System.Serializable]
public class LocalizationData
{
    public string Key;
    public TextData[] Values;
}

[System.Serializable]
public struct TextData
{
    public LanguageEnum Language;
    public string Text;
}

public enum LanguageEnum
{
    None = 0,
    RU   = 1,
    EN   = 2,
    TR   = 3
}