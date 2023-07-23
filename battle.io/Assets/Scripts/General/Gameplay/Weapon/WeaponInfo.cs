using UnityEngine;

[CreateAssetMenu(fileName = nameof(WeaponInfo), menuName = EditorConstants.DataPath + nameof(WeaponInfo))]
public class WeaponInfo : ScriptableObject
{
    public GameObject Prefab;
    public GameObject Special; 
    public AnimationDataConfig Animation;
    public AudioConfig Audio;
    public float Damage;
    public float RandomDamage;
    public float Distance;
    public WeaponType Type;
    public WeaponHandType HandType;
    public WeaponMainHandType MainHandType;
}