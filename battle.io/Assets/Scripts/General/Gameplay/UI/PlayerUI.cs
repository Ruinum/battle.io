using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private WeaponChooseUI _weaponChooseUI;

    public LevelUI LevelUI => _levelUI;
    public WeaponChooseUI WeaponUI => _weaponChooseUI;

    public void Initialize(IPlayer player)
    {
        _levelUI.Initialize(player.Level);
        _weaponChooseUI.Initialize(player);
    }
}