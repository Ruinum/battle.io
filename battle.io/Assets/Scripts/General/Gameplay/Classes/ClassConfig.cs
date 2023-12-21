using UnityEngine;

[CreateAssetMenu(fileName = nameof(ClassConfig), menuName = EditorConstants.ConfigPath + nameof(ClassConfig))]
public class ClassConfig : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public GameObject Particle;
    public AnimationDataConfig Animation;
    public AudioConfig Audio;
    public float Cooldown;
}