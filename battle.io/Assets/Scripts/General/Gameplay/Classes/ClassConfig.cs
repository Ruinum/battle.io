using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ClassConfig), menuName = EditorConstants.DataPath + nameof(ClassConfig))]
public class ClassConfig : ScriptableObject
{
    public Sprite Icon;
    public GameObject Particle;
    public AnimationDataConfig Animation;
    public AudioConfig Audio;
    public float Cooldown;
}

public interface IClassAbility
{
    void Initialize(GameObject player);
    void UseAbility();
}

public sealed class Class
{
    private Transform _transform;
    private ClassConfig _currentClass = null;

    private Dictionary<string, IClassAbility> _abilities;

    public Class(Transform transform) 
    { 
        _transform = transform;
        
    }

    public void AddClass(ClassConfig classConfig)
    {
        _currentClass = classConfig;        
    }
}