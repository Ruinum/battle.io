using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int PlayerLevel => _currentLevel;

    [SerializeField] private int _currentLevel = 1;
    [SerializeField]private float _currentExp = 0;
    private float _nextLevelExp = 100;

    public Action<int> OnLevelChange;
    public Action<float> OnExpChange;
    public Action<float> OnNextExpChange;
    public Action<float> OnExpRemove;
    public Action OnDead;

    private void Start()
    {
        InvokeEvents();
    }

    public void AddExp(float value)
    {
        _currentExp += value;
        OnExpChange?.Invoke(_currentExp);

        if (_currentExp < _nextLevelExp) return;

        _currentExp = 0;
        _currentLevel += 1;
        _nextLevelExp = 100; //TODO: Level exp scaling

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

        _currentExp = 0;
        _nextLevelExp = 100; //TODO: Level exp scaling

        InvokeEvents();
    }

    private void InvokeEvents()
    {
        OnNextExpChange?.Invoke(_nextLevelExp);
        OnLevelChange?.Invoke(_currentLevel);
        OnExpChange?.Invoke(_currentExp);
    }

    private void CheckDeath()
    {
        if (_currentLevel > 0) return;
        OnDead?.Invoke();
        Debug.Log("Player death logic");
        Destroy(gameObject);
    }
}