using Ruinum.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressionSystem : BaseSingleton<LevelProgressionSystem>
{
    public LevelStructure levelStructure;
}
[System.Serializable]
public class LevelStructure
{
    public WeaponInfo Left;
    public WeaponInfo Right;

    public LevelStructure GetLevel(int[] vs,int pass)
    {
        if (pass == vs.Length) return this;
        return nextLevel[vs[pass]].GetLevel(vs,++pass);
    }

    public LevelStructure[] nextLevel;
}