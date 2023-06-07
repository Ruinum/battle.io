using Ruinum.Core;
using UnityEngine;

public class Player : Executable
{
    [SerializeField] private PlayerAnimatorController _animationController;
    [SerializeField] private WeaponInventory _inventory;
    [SerializeField] private Movement _movement;

    [SerializeField] private float _magniteRadius;
    [SerializeField] private float _magniteSpeed;

    private Magnite _magnite;

    public override void Start()
    {
        _magnite = new Magnite(transform, _magniteSpeed, _magniteRadius);

        base.Start();
    }

    public override void Execute()
    {
        _magnite.Execute();

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
