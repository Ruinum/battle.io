using UnityEngine;

[CreateAssetMenu(fileName = nameof(Unlockable), menuName = EditorConstants.DataPath + nameof(Unlockable))]
public class Unlockable : UniqueObject
{
    public Sprite Icon;
    public string Name;
    public string Description;
    public bool Unlocked;
}
