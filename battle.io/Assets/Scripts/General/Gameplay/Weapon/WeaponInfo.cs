using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Ruinum/Weapon" + nameof(WeaponInfo))]
public class WeaponInfo : ScriptableObject
{
    public GameObject Prefab;
    public AnimationData Animation;
    public float Damage;
    public float RandomDamage;
    public float Distance;
    public WeaponType Type;
    public WeaponHandType HandType;
}
