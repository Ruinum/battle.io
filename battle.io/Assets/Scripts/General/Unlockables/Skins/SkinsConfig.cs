using UnityEngine;

[CreateAssetMenu(fileName = nameof(SkinsConfig), menuName = EditorConstants.DataPath + nameof(SkinsConfig))]
public class SkinsConfig : UniqueObject
{
    public Skin[] Skins;
}