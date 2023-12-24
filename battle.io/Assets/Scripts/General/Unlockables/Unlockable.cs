using UnityEngine;

public class Unlockable : UniqueObject
{
    [Header("Unlockable Settings")]
    public Sprite Icon;
    public string Name;
    public string Description;
    public bool Unlocked;

    [Header("Localization Settings")]
    public LocalizationData NameLocalization;
    public LocalizationData DescriptionLocalization;
}