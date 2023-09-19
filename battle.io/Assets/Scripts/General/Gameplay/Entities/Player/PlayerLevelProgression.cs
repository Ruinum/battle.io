using Ruinum.Utils;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerLevelProgression
{
    private IPlayer _player;
    private Level _level;
    private WeaponInventory _inventory;

    private List<int> _weapons = new List<int>();   
    private int _lastMaxLevel = 0;
    private int _levelPoints = 0;
    private bool _isUIShowed = false;
    private bool _lastLevel;

    public PlayerLevelProgression(IPlayer player, Level level, WeaponInventory inventory)
    {
        _player = player;
        _level = level;
        _inventory = inventory;

        _level.OnLevelChange += LevelUp;
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

        if (level.Class != null) _player.Class.AddClass(level.Class);

        if (level.AdditionalWeapon == null) return;
        _inventory.EquipWeapon(level.AdditionalWeapon);
    }

    public void Execute()
    {
        if (_levelPoints <= 0 || _isUIShowed || _lastLevel) return;
        ShowUi();
    }

    public void LevelUp(int level)
    {
        if (level <= _lastMaxLevel) _levelPoints--;
        else 
        {
            _lastMaxLevel = Mathf.Max(_lastMaxLevel, level);
            
            ColorUtility.TryParseHtmlString("#ED7014", out Color color);
            if (level != 1 && level == _lastMaxLevel) ImpactUtils.TryCreatePopUp("LEVEL UP!", _level.transform.position, color, 1.1f, out var tmp);
            _levelPoints++;
        }

        _lastMaxLevel = Mathf.Max(_lastMaxLevel, level);        
    }

    private void ShowUi()
    {
        LevelStructure structure = Game.Context.LevelStructure.GetLevel(_weapons.ToArray(), 0);
        if (structure.NextLevel.Length <= 0) { _lastLevel = true; return; }
        Game.Context.PlayerUI.WeaponUI.GetChoose(structure);
        _isUIShowed = true;
    }
}