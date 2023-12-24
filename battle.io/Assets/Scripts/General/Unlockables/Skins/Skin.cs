using UnityEngine;

[CreateAssetMenu(fileName = nameof(Skin), menuName = EditorConstants.UnlockablesPath + nameof(Skin))]
public class Skin : Unlockable
{
    [Header("Skin Settings")]
    public int Cost;
    public Sprite Body;
    public Sprite Arm;
}