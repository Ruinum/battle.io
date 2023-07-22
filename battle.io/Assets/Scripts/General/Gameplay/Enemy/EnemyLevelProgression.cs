using UnityEngine;
using System.Collections.Generic;

public sealed class EnemyLevelProgression
{
    private WeaponInventory _inventory;
    private List<int> _weapons = new List<int>();

    public EnemyLevelProgression(Level level, WeaponInventory inventory)
    {
        _inventory = inventory;

        level.OnLevelChange += Progress;
    }

    public void Progress(int level)
    {
        LevelStructure levelStructure = LevelProgressionSystem.Singleton.LevelStructure.GetLevel(_weapons.ToArray(), 0);
        if (levelStructure.NextLevel.Length == 0) return;
        
        int rnd = Random.Range(0, levelStructure.NextLevel.Length);
        levelStructure = levelStructure.NextLevel[rnd];
        _weapons.Add(rnd);
        
        _inventory.UnarmAll();

        if (levelStructure.MainWeapon == null)
        {
            UnityEngine.Debug.LogWarning($"There is no main weapon in {nameof(LevelStructure)} of {level}");
            return;
        }

        _inventory.EquipWeapon(levelStructure.MainWeapon);

        if (levelStructure.AdditionalWeapon == null) return;
        _inventory.EquipWeapon(levelStructure.AdditionalWeapon);
    }
}