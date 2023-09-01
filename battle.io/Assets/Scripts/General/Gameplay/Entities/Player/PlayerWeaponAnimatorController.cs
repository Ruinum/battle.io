using Ruinum.Core.Systems;
using UnityEngine;

public class PlayerWeaponAnimatorController : PlayerAnimatorController
{
    [SerializeField] private WeaponInventory _inventory;
    private bool _isHeavyEquipped = false;

    private void Start()
    {
        _inventory.OnWeaponChange += ChangeIdle;
        _idleName = "Arm Idle";
    }

    public void PlayWeaponAttackAnimation(WeaponInfo weaponInfo)
    {
        string animationName = weaponInfo.Type.ToString();
        if (weaponInfo.IsHeavy) animationName +=" Heavy";

        switch (weaponInfo.HandType)
        {
            case WeaponHandType.Left:
                animationName += " Left";
                break;
            case WeaponHandType.Right:
                animationName += " Right";
                break;
            case WeaponHandType.Both:
                animationName += " " + weaponInfo.MainHandType;
                break;
        }

        animationName += " Attack";

        PlayAnimation(animationName);
    }

    private void ChangeIdle(WeaponInfo weaponInfo, GameObject gameObject)
    {
        if (!weaponInfo.IsHeavy && _isHeavyEquipped)
        {
            _idleName = "Arm Idle";
            var timer = TimerSystem.Singleton.StartTimer(1, () => { if (!_isDestroyed) { _animator.SetLayerWeight(1, 1); _isHeavyEquipped = false; } });
            timer.SetSpeed(2f);
            timer.OnTimeChange += (x, y) => { if (!_isDestroyed) _animator.SetLayerWeight(1, x); };
        }
        else if (weaponInfo.IsHeavy && !_isHeavyEquipped)
        {
            _idleName = "Heavy Idle";
            var timer = TimerSystem.Singleton.StartReverseTimer(1, () => { if (!_isDestroyed) { _animator.SetLayerWeight(1, 0); _isHeavyEquipped = true; } });
            timer.SetSpeed(2f);
            timer.OnTimeChange += (x, y) => { if (!_isDestroyed) _animator.SetLayerWeight(1, x); };
        }
    }
}
