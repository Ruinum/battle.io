using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(LocalizationConfig), menuName = EditorConstants.ConfigPath + nameof(LocalizationConfig))]
public class LocalizationConfig : UniqueObject
{
    [Header("Fonts Settings")]
    public TMP_FontAsset RussianFont;
    public TMP_FontAsset EnglishFont;
    public TMP_FontAsset TurkishFont;

    [Header("Localization")]
    public LocalizationData[] LocalizationDatas;
}