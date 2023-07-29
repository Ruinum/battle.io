using System.Collections.Generic;

public sealed class PlayerLevelProgression
{
    public WeaponInventory _inventory;

    private List<int> _weapons = new List<int>();   
    private int _previousLevel = 0;
    private int _levelPoints = 0;
    private bool _isUIShowed = false;

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
        _isUIShowed = false;

        if (level.AdditionalWeapon == null) return;
        _inventory.EquipWeapon(level.AdditionalWeapon);
    }

    public void Execute()
    {
        if (_levelPoints <= 0 || _isUIShowed) return;
        ShowUi();
    }

    public void LevelUp(int level)
    {
        if (level <= _previousLevel) _levelPoints--;
        else _levelPoints++;
        _previousLevel = level;        
    }

    private void ShowUi()
    {
        LevelStructure structure = Game.Context.LevelStructure.GetLevel(_weapons.ToArray(), 0);
        Game.Context.PlayerUI.WeaponUI.GetChoose(structure);
        _isUIShowed = true;
    }
}