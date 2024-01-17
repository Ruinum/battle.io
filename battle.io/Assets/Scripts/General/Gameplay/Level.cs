using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int PlayerLevel => _currentLevel;
    public float Exp => _currentExp;
    public float ExpNeeded => _nextLevelExp;
    public bool Died => _died;

    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private float _currentExp = 0;

    private float _maxLevel = 7;
    private float _nextLevelExp = 100;
    private float _baseExp = 100;
    private float _baseExpModifier = 0.25f;
    private float _expModifier = 0.25f;
    private bool _died = false;

    public Action<int> OnLevelChange;
    public Action<float> OnExpChange;
    public Action<float> OnNextExpChange;
    public Action<float> OnExpRemove;
    public Action<Level> OnDead;

    private void Start()
    {
        InvokeEvents();
    }

    public void AddExp(float value)
    {
        _currentExp += value;
        OnExpChange?.Invoke(_currentExp);

        if (_currentExp < _nextLevelExp) return;
        if (_currentLevel == _maxLevel) return;

        var plusValue = _currentExp - _nextLevelExp;
        _currentExp = 0 + plusValue;
        _currentLevel += 1;
        ScaleExp();

        InvokeEvents();
    }

    public void RemoveExp(float value)
    {
        _currentExp -= value;

        OnExpChange?.Invoke(_currentExp);
        OnExpRemove?.Invoke(value);

        if (_currentExp >= 0) return;

        _currentLevel -= 1;
        CheckDeath();

        var minusValue = _currentExp;
        ScaleExp();

        _currentExp = _nextLevelExp + minusValue;

        InvokeEvents();
    }

    public void InvokeEvents()
    {
        OnNextExpChange?.Invoke(_nextLevelExp);
        OnLevelChange?.Invoke(_currentLevel);
        OnExpChange?.Invoke(_currentExp);
    }

    private void ScaleExp()
    {
        _nextLevelExp = _baseExp + _baseExp * (_baseExpModifier + _currentLevel * _expModifier);
    }

    private void CheckDeath()
    {
        if (_currentLevel > 0) return;
        _died = true;
        OnDead?.Invoke(this);
        Destroy(gameObject);
    }
}