using UnityEngine;

[CreateAssetMenu(fileName = nameof(Achievement), menuName = EditorConstants.DataPath + nameof(Achievement))]
public class Achievement : UniqueObject
{
    public Sprite Icon;
    public string Name;
    public string Description;
    public bool Unlocked;
}
