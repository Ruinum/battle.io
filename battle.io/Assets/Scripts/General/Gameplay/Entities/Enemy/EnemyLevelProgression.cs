using UnityEngine;
using System.Collections.Generic;
using Ruinum.Utils;

public sealed class EnemyLevelProgression : ILevelProgression
{
    private Level _level;
    private WeaponInventory _inventory;
    private List<int> _weapons = new List<int>();

    private int _lastMaxLevel = 0;
    private int _levelPoints = 0;

    public EnemyLevelProgression(Level level, WeaponInventory inventory)
    {
        _level = level;
        _inventory = inventory;

        level.OnLevelChange += LevelUp;
    }

    public void Execute() { }
    public void TakeLevel(LevelStructure level, int i) { }

    public void LevelUp(int level)
    {
        if (level <= _lastMaxLevel) _levelPoints--;
        else _levelPoints++;
        _lastMaxLevel = Mathf.Max(_lastMaxLevel, level);

        if (_levelPoints <= 0) return;

        ColorUtility.TryParseHtmlString("#ED7014", out Color color);
        if (level != 1 && level == _lastMaxLevel) ImpactUtils.TryCreatePopUp("LEVEL UP!", _level.transform.position, color, 1.1f, out var tmp);

        LevelStructure levelStructure = Game.Context.LevelStructure.GetLevel(_weapons.ToArray(), 0);
        if (levelStructure.NextLevel.Length == 0) return;
        
        int rnd = Random.Range(0, levelStructure.NextLevel.Length);
        levelStructure = levelStructure.NextLevel[rnd];
        
        _weapons.Add(rnd);       
        _inventory.UnarmAll();

        if (levelStructure.MainWeapon == null)
        {
            Debug.LogWarning($"There is no main weapon in {nameof(LevelStructure)} of {level}");
            return;
        }

        _inventory.EquipWeapon(levelStructure.MainWeapon);

        if (levelStructure.AdditionalWeapon == null) return;
        _inventory.EquipWeapon(levelStructure.AdditionalWeapon);
    }
}