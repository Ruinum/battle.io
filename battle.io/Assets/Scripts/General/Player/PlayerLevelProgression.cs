using System.Collections.Generic;

public class PlayerLevelProgression
{
    public WeaponInventory _inventory;
    private List<int> _weapons = new List<int>();
    private int _previousLevel = 1;
    private int _levelPoints = 0;

    public PlayerLevelProgression(Level level, WeaponInventory inventory)
    {
        _inventory = inventory;
        level.OnLevelChange += LevelUp;
    }

    public void TakeLevel(LevelStructure level, int i)
    {
        _weapons.Add(i);
        _inventory.UnarmAll();

        if (level.MainWeapon == null)
        {
            UnityEngine.Debug.LogWarning($"There is no main weapon in {nameof(LevelStructure)} of {level}");
            return;
        }

        _inventory.EquipWeapon(level.MainWeapon);
        _levelPoints--;

        if (level.AdditionalWeapon == null) return;
        _inventory.EquipWeapon(level.AdditionalWeapon);
    }

    public void LevelUp(int level)
    {
        if (level <= _previousLevel) _levelPoints--;
        else _levelPoints++;
        _previousLevel = level;

        LevelStructure structure = LevelProgressionSystem.Singleton.LevelStructure.GetLevel(_weapons.ToArray(), 0);
        WeaponChooseUI.Singleton.GetChoose(structure);
    }
}