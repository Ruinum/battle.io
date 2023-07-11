using UnityEngine;

public class HitBoxController
{
    private WeaponInfo _currentWeaponInfo;
    private HitBox[] _weaponHitBoxes;

    public HitBoxController(PlayerAnimatorController controller, WeaponInventory inventory)
    {
        inventory.OnWeaponChange += OnWeaponChange;

        controller.SubscribeOnTimelineEvent("HitBoxEnable", EnableHitBox);
        controller.SubscribeOnTimelineEvent("HitBoxDisable", DisableHitBox);

        _weaponHitBoxes = new HitBox[0];
    }

    private void OnWeaponChange(WeaponInfo currentWeapon, GameObject weapon)
    {
        _currentWeaponInfo = currentWeapon;
        _weaponHitBoxes = weapon.GetComponentsInChildren<HitBox>();
    }

    private void EnableHitBox()
    {
        for (int i = 0; i < _weaponHitBoxes.Length; i++)
        {
            _weaponHitBoxes[i].Enable(_currentWeaponInfo.Damage, _currentWeaponInfo.RandomDamage);
        }
    }

    private void DisableHitBox()
    {
        for (int i = 0; i < _weaponHitBoxes.Length; i++)
        {
            _weaponHitBoxes[i].Disable();
        }
    }
}