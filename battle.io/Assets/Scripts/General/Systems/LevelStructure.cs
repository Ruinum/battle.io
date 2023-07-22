using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = nameof(LevelStructure), menuName = EditorConstants.DataPath + nameof(LevelStructure))]
public class LevelStructure : ScriptableObject
{
    public Sprite Icon;
    public WeaponInfo MainWeapon;
    public WeaponInfo AdditionalWeapon;

    public LevelStructure GetLevel(int[] vs,int pass)
    {
        if (pass == vs.Length) return this;
        return NextLevel[vs[pass]].GetLevel(vs,++pass);
    }

    public LevelStructure[] NextLevel;
}