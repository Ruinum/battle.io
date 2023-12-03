using UnityEngine;

[CreateAssetMenu(fileName = nameof(Skin), menuName = EditorConstants.DataPath + nameof(Skin))]
public class Skin : Unlockable
{
    public int Cost;
    public Sprite Body;
    public Sprite Arm;
}
