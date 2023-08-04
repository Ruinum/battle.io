using Ruinum.Core.Interfaces;
using Ruinum.Core.Systems;
using UnityEngine;

public sealed class Class : IExecute
{
    private GameObject _gameObject;
    private PlayerAnimatorController _animator;
    private AbilitiesConfig _abilitiesConfig;

    private ClassConfig _currentClass = null;
    private IClassAbility _currentAbility = null;

    private bool _canUseAbility;

    public Class(GameObject gameObject, PlayerAnimatorController animator) 
    {
        _animator = animator;
        _gameObject = gameObject;
        _abilitiesConfig = Game.Context.AbilitiesConfig;
    }

    public void Execute()
    {
        if (_currentClass == null) return;
        if (Input.GetKeyDown(KeyCode.E) && _canUseAbility) UseAbility();
    }

    public void AddClass(ClassConfig classConfig)
    {
        Debug.LogWarning("Try Adding class");
        _currentClass = classConfig;

        if (!_abilitiesConfig.TryGetAbility(_currentClass.Name, out _currentAbility)) { Debug.LogError("There is no ability for that class"); return; }
        if (_currentClass.Animation) _animator.AddTimeline(_currentClass.Animation);

        Debug.LogWarning("Initializing a class");
        _currentAbility.Initialize(_gameObject, _currentClass);
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
