using UnityEngine;

public class HitBoxController
{
    private WeaponInfo _currentWeaponInfo;
    private GameObject _currentWeapon;
    private AnimationData[] _animationDatas;
    private HitBox[] _weaponHitBoxes;

    public HitBoxController(PlayerAnimatorController controller, WeaponInventory inventory, AnimationData[] animationDatas)
    {
        _animationDatas = animationDatas;
        inventory.OnWeaponChange += OnWeaponChange;

        for (int i = 0; i < _animationDatas.Length; i++)
        {
            controller.SubscribeOnTimelineEvent(_animationDatas[i], "HitBoxEnable", EnableHitBox);
            controller.SubscribeOnTimelineEvent(_animationDatas[i], "HitBoxDisable", DisableHitBox);
        }

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