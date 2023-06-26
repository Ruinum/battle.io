using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private string _lastAnimation;

    private void PlayAttackAnimation(WeaponInfo weaponInfo)
    {        
        _animator.CrossFade($"{weaponInfo.Type} Attack", 0.15f);
    }

    public bool TryPlayAttackAnimation(WeaponInfo weaponInfo)
    {
        if (_animator.GetCurrentAnimatorClipInfo(0).Length == 0 || _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == $"{weaponInfo.Type} Attack") return false;
        _animator.CrossFade($"{weaponInfo.Type} Attack", 0.15f);
        return true;
    }
}
