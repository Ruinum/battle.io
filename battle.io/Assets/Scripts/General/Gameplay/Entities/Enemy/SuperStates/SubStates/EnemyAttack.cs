using Ruinum.Core;
using Ruinum.Core.Systems;
using UnityEngine;

public sealed class EnemyAttack
{
    private Transform _transform;
    private PlayerAnimatorController _animator;
    private WeaponInventory _inventory;
    private WeaponInfo _weaponInfo;

    private IPlayer _enemy;
    private bool _isAttacking;

    public EnemyAttack(EnemyContext context)
    {
        _transform = context.Transform;
        _animator = context.Animator;
        _inventory = context.Inventory;

        _inventory.OnWeaponChange += SetWeapon;   
        _inventory.TryGetRightWeapon(out _weaponInfo);
    }

    public void Execute()
    {
        if (CheckAttack() && !_isAttacking)
        {
            _isAttacking = true;
            TimerSystem.Singleton.StartTimer(Random.Range(0, 0.1f), () => { _animator.PlayAnimation(_weaponInfo.Type + " Attack"); });
            TimerSystem.Singleton.StartTimer(_weaponInfo.Animation.Clip.length, () => _isAttacking = false);
        }
    }

    private bool CheckAttack()
    {
        if (_enemy == null) return false;
        if (_enemy.IsDestroyed) return false;
        if (_weaponInfo == null) return false;

        if (Vector2.Distance(_transform.position, _enemy.Transform.position) <= _weaponInfo.Distance + (_transform.lossyScale.x - 1)) return true;
        
        return false;
    }

    public void SetEnemy(IPlayer enemy) => _enemy = enemy;
    private void SetWeapon(WeaponInfo weaponInfo, GameObject weapon) { _weaponInfo = weaponInfo; }
}