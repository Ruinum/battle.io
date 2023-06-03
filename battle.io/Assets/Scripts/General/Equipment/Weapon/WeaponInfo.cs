using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Data/Weapon" + nameof(WeaponInfo))]
public class WeaponInfo : ScriptableObject
{
    public GameObject Prefab;
    public float Damage;
    public float MovementModifier;
    public WeaponType Type;
    public WeaponHandType HandType;
}
