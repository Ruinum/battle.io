using Ruinum.Core.Interfaces;
using Ruinum.Core.Systems;
using System.Collections.Generic;
using UnityEngine;

public sealed class Class : IExecute
{
    private GameObject _gameObject;
    private PlayerAnimatorController _animator;
    private ClassConfig _currentClass = null;
    private IClassAbility _currentAbility = null;

    private bool _canUseAbility;

    private Dictionary<string, IClassAbility> _abilities;

    public Class(GameObject gameObject, PlayerAnimatorController animator) 
    {
        _animator = animator;
        _gameObject = gameObject;
    }

    public void Execute()
    {
        if (_currentClass == null) return;
        if (Input.GetKeyDown(KeyCode.E) && _canUseAbility) UseAbility();
    }

    public void AddClass(ClassConfig classConfig)
    {
        _currentClass = classConfig;
        
        if (!_abilities.TryGetValue(_currentClass.Name, out _currentAbility)) return;
        if (_currentClass.Animation) _animator.AddTimeline(_currentClass.Animation);
        _currentAbility.Initialize(_gameObject);

        _canUseAbility = true;
    }

    private void UseAbility()
    {
        if (_currentAbility == null) return;

        _currentAbility.UseAbility();
        _canUseAbility = false;
        
        if (_currentClass.Animation) _animator.PlayAnimation(_currentClass.Animation.Clip.name);

        TimerSystem.Singleton.StartTimer(_currentClass.Cooldown, () => _canUseAbility = true);
    }
}