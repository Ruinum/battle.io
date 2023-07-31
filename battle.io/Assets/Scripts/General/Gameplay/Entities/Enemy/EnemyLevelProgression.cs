using UnityEngine;
using System.Collections.Generic;
using Ruinum.Core.Systems;

public sealed class EnemyLevelProgression
{
    private WeaponInventory _inventory;
    private List<int> _weapons = new List<int>();

    private int _previousLevel = 0;
    private int _levelPoints = 0;

    public EnemyLevelProgression(Level level, WeaponInventory inventory)
    {
        _inventory = inventory;

        Debug.Log("SetProgress");

        level.OnLevelChange += Progress;

    }

    public void Progress(int level)
    {
        if (level <= _previousLevel) _levelPoints--;
        else _levelPoints++;
        _previousLevel = level;

        if (_levelPoints <= 0) return;

        LevelStructure levelStructure = Game.Context.LevelStructure.GetLevel(_weapons.ToArray(), 0);
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

    public int GetLevelPoints()
    {
        return _levelPoints;
    }
}