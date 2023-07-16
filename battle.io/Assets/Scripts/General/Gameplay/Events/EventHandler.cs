using UnityEngine;

public abstract class EventHandler
{
    protected PlayerAnimatorController _controller;
    protected WeaponInfo _weaponInfo;
    protected GameObject _weapon;

    public EventHandler(PlayerAnimatorController controller, WeaponInventory inventory)
    {
        _controller = controller;
        inventory.OnWeaponChange += OnWeaponChange;
    }

    protected abstract void WeaponChange();

    private void OnWeaponChange(WeaponInfo weaponInfo, GameObject weapon)
    {
        _weaponInfo = weaponInfo;
        _weapon = weapon;

        WeaponChange();
    }
}