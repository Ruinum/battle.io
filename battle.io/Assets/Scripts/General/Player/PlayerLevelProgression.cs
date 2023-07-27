using System.Collections.Generic;

public class PlayerLevelProgression
{
    public WeaponInventory _inventory;
    private List<int> _weapons = new List<int>();

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

        if (level.AdditionalWeapon == null) return;
        _inventory.EquipWeapon(level.AdditionalWeapon);
    }

    public void LevelUp(int i)
    {
        if (i == 1) return;
        LevelStructure structure = LevelProgressionSystem.Singleton.LevelStructure.GetLevel(_weapons.ToArray(), 0);
        WeaponChooseUI.Singleton.GetChoose(structure);

    }
}