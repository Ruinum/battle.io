using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayAttackAnimation(WeaponInfo weaponInfo)
    {
        _animator.CrossFade($"{weaponInfo.Type} Attack", 0.15f);
    }
}
