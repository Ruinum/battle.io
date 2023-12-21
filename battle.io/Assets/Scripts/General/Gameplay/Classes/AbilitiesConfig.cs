using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AbilitiesConfig), menuName = EditorConstants.ConfigPath + nameof(AbilitiesConfig))]
public class AbilitiesConfig : ScriptableObject 
{
    private Dictionary<string, IClassAbility> _abilities;

    public void Initialize()
    {
        _abilities = new Dictionary<string, IClassAbility>();
        _abilities.Add("Knight", new KnightAbility());
    }

    public bool TryGetAbility(string className, out IClassAbility classAbility) => _abilities.TryGetValue(className, out classAbility);
}