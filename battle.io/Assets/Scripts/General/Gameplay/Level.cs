using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    private int _currentLevel = 1;
    private float _currentExp = 0;
    private float _nextLevelExp = 100;

    public Action<int> OnLevelChange;
    public Action<float> OnExpChange;
    public Action<float> OnLevelUp;

    public void AddExp(float value)
    {
        _currentExp += value;
        OnExpChange?.Invoke(_currentExp);
        
        if (_currentExp >= _nextLevelExp)
        {
            _currentExp = 0;
            _currentLevel += 1;

            OnLevelUp?.Invoke(_nextLevelExp);
            OnLevelChange?.Invoke(_currentLevel);
            OnExpChange?.Invoke(_currentExp);
        }
    }
}