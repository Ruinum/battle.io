using Ruinum.Core;
using UnityEngine;

public class Player : Executable
{
    [SerializeField] private PlayerAnimatorController _animationController;
    [SerializeField] private WeaponInventory _inventory;
    [SerializeField] private Movement _movement;

    public override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!_inventory.TryGetRightWeapon(out var weaponInfo)) return;
            _animationController.PlayAttackAnimation(weaponInfo);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!_inventory.TryGetLeftWeapon(out var weaponInfo)) return;
            _animationController.PlayAttackAnimation(weaponInfo);
        }
    }
}
