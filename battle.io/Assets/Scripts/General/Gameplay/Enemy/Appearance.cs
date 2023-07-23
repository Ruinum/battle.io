using UnityEngine;

[CreateAssetMenu(fileName = nameof(Appearance), menuName = EditorConstants.DataPath + nameof(Appearance))]
public class Appearance : ScriptableObject
{
    public Sprite Body;
    public Sprite Arm;
}