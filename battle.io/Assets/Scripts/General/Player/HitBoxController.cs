using UnityEngine;

public class HitBoxController
{
    private WeaponInfo _currentWeaponInfo;
    private GameObject _currentWeapon;
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
        _currentWeapon = weapon;

        _weaponHitBoxes = _currentWeapon.GetComponents<HitBox>();
    }

    private void EnableHitBox()
    {
        Debug.LogWarning($"Enable Hit Box");
        for (int i = 0; i < _weaponHitBoxes.Length; i++)
        {
            _weaponHitBoxes[i].Enable(_currentWeaponInfo.Damage);
        }
    }

    private void DisableHitBox()
    {
        Debug.LogWarning("Disable Hit Box");
        for (int i = 0; i < _weaponHitBoxes.Length; i++)
        {
            _weaponHitBoxes[i].Disable();
        }
    }
}