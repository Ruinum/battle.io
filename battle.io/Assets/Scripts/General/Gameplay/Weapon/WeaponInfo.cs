using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Ruinum/Weapon" + nameof(WeaponInfo))]
public class WeaponInfo : ScriptableObject
{
    public GameObject Prefab;
    public float Damage;
    public float Distance;
    public WeaponType Type;
    public WeaponHandType HandType;
}
