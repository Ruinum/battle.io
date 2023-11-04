using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int PlayerLevel => _currentLevel;
    public float Exp => _currentExp;
    public float ExpNeeded => _nextLevelExp;
    public bool Died => _died;

    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private float _currentExp = 0;
    
    private float _nextLevelExp = 100;
    private float _baseExp = 100;
    private float _baseExpModifier = 0.25f;
    private float _expModifier = 0.25f;
    private bool _died = false;

    private List<Multiplier> _multipliers;

    public Action<int> OnLevelChange;
    public Action<float> OnExpChange;
    public Action<float> OnNextExpChange;
    public Action<float> OnExpRemove;
    public Action<Level> OnDead;

    private void Start()
    {
        _multipliers = new List<Multiplier>();
        InvokeEvents();
    }

    public void AddExp(float value)
    {
        _currentExp += MultiplyExp(value, MultiplierType.Positive);
        OnExpChange?.Invoke(_currentExp);

        if (_currentExp < _nextLevelExp) return;

        var plusValue = _currentExp - _nextLevelExp;
        _currentExp = 0 + plusValue;
        _currentLevel += 1;
        ScaleExp();

        InvokeEvents();
    }

    public void RemoveExp(float value)
    {
        _currentExp -= MultiplyExp(value, MultiplierType.Negative);

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

    public void AddMultiplier(Multiplier multiplier) => _multipliers.Add(multiplier);
    public void RemoveMultiplier(Multiplier multiplier) => _multipliers.Remove(multiplier);

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

    private float MultiplyExp(float value, MultiplierType multiplierType)
    {
        if (_multipliers == null) return value;
        if (_multipliers.Count == 0) return value;

        for (int i = 0; i < _multipliers.Count; i++)
        {
            switch (multiplierType)
            {
                case MultiplierType.None:
                    break;
                case MultiplierType.Positive:
                    if (_multipliers[i].Type != MultiplierType.Positive) continue;
                    value += value * _multipliers[i].Value;
                    break;
                case MultiplierType.Negative:
                    if (_multipliers[i].Type != MultiplierType.Negative) continue;
                    value += value * _multipliers[i].Value;
                    break;
                case MultiplierType.All:
                    value += value * _multipliers[i].Value;
                    break;
                default:
                    break;
            }
        }

        return value;
    }

    private void CheckDeath()
    {
        if (_currentLevel > 0) return;
        _died = true;
        OnDead?.Invoke(this);
        Destroy(gameObject);
    }
}

public struct Multiplier
{
    public float Value;
    public MultiplierType Type;
}

public enum MultiplierType
{
    None     = 0,
    Positive = 1,
    Negative = 2,
    All      = 3
}