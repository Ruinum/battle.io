using UnityEngine;

public abstract class EventHandler
{
    protected Transform _transform;
    protected PlayerAnimatorController _controller;
    protected WeaponInfo _mainWeaponInfo;
    protected WeaponInfo _subWeaponInfo;
    protected GameObject _mainWeapon;
    protected GameObject _subWeapon;

    public EventHandler(PlayerAnimatorController controller, WeaponInventory inventory)
    {
        _controller = controller;
        _transform = _controller.transform;
        inventory.OnWeaponChange += OnMainWeaponChange;
        inventory.OnWeaponSubChange += OnSubWeaponChange;
    }

    protected abstract void WeaponMainChange();
    protected abstract void WeaponSubChange();

    private void OnMainWeaponChange(WeaponInfo weaponInfo, GameObject weapon)
    {
        _mainWeaponInfo = weaponInfo;
        _mainWeapon = weapon;

        WeaponMainChange();
    }

    private void OnSubWeaponChange(WeaponInfo weaponInfo, GameObject weapon)
    {
        _subWeaponInfo = weaponInfo;
        _subWeapon = weapon;

        WeaponSubChange();
    }
}