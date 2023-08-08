using UnityEngine;

public class WeaponAnimation
{
    private PlayerAnimatorController _animatorController;
    public WeaponAnimation(PlayerAnimatorController animatorController, WeaponInventory weaponInventory)
    {
        _animatorController = animatorController;

        weaponInventory.OnWeaponChange += AddWeaponAnimationTimeline;
        weaponInventory.OnWeaponSubChange += AddWeaponAnimationTimeline;
    }

    private void AddWeaponAnimationTimeline(WeaponInfo weaponInfo, GameObject @object)
    {
        Debug.Log(weaponInfo.name);
        Debug.Log(weaponInfo.Animation.Clip);
        _animatorController.AddTimeline(weaponInfo.Animation);
    }
}