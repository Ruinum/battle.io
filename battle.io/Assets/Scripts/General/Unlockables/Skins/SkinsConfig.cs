using UnityEngine;

[CreateAssetMenu(fileName = nameof(SkinsConfig), menuName = EditorConstants.ConfigPath + nameof(SkinsConfig))]
public class SkinsConfig : UniqueObject
{
    public Skin[] Skins;
}