using Ruinum.Core;
using UnityEngine;

public sealed class EnemyAttack
{
    private Transform _transform;
    private PlayerAnimatorController _animator;
    private WeaponInventory _inventory;
    private WeaponInfo _weapon;

    private IPlayer _enemy;
    private bool _isAttacking = false;

    public EnemyAttack(EnemyContext context)
    {
        _transform = context.Transform;
        _animator = context.Animator;
        _inventory = context.Inventory;

        _inventory.OnWeaponChange += SetWeapon;
        _inventory.TryGetRightWeapon(out _weapon);
    }

    public void Execute()
    {
        if (CheckAttack() && !_isAttacking)
        {
            _isAttacking = true;
            TimerManager.Singleton.StartTimer(Random.Range(0, 0.15f), () => { _animator.TryPlayAttackAnimation(_weapon); _isAttacking = false; });
        }
    }

    private bool CheckAttack()
    {
        if (_enemy.Transform == null || _enemy == default || _weapon == null) return false;
        if (Vector2.Distance(_transform.position, _enemy.Transform.position) <= _weapon.Distance + (_transform.lossyScale.x - 1)) return true;
        
        return false;
    }

    public void SetEnemy(IPlayer enemy) => _enemy = enemy;
    private void SetWeapon(WeaponInfo weaponInfo) => _weapon = weaponInfo;
}