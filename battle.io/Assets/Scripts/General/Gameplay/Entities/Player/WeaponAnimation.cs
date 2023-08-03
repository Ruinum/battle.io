using UnityEngine;

public class WeaponAnimation
{
    private PlayerAnimatorController _animatorController;
    public WeaponAnimation(PlayerAnimatorController animatorController, WeaponInventory weaponInventory)
    {
        _animatorController = animatorController;
        weaponInventory.OnWeaponChange += AddWeaponAnimationTimeline;
    }

    private void AddWeaponAnimationTimeline(WeaponInfo weaponInfo, GameObject @object)
    {
        _animatorController.AddTimeline(weaponInfo.Animation);
    }
}