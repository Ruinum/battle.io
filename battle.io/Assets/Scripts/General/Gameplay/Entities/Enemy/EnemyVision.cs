using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision
{
    public List<IInterestPoint> InterestPoint => _interestPoints;
    public List<IPlayer> Enemies => _enemies;
    public IInterestPoint NearestInterest => _nearestInterest;
    public IPlayer NearestEnemy => _nearestEnemy;

    private Transform _transform;
    private Level _level;
    private List<IInterestPoint> _interestPoints;
    private List<IPlayer> _enemies;

    private IInterestPoint _nearestInterest;
    private IPlayer _nearestEnemy;

    private float _visionModifier = 0.5f;
    private float _baseVisionRadius;
    private float _visionRadius;
    private float _distance;

    public Action<IInterestPoint> OnNearestInterestChange;
    public Action<IPlayer> OnNearestEnemyChange;

    public EnemyVision(Transform transform, Level level, float visionRadius)
    {
        _transform = transform;
        _baseVisionRadius = visionRadius;
        _visionRadius = visionRadius;
        _level = level;
        _level.OnExpChange += ChangeVisionRadius;

        _interestPoints = new List<IInterestPoint>();
        _enemies = new List<IPlayer>();
    }

    public void Execute()
    {
        _enemies.Clear();
        _interestPoints.Clear();

        if (Game.Context.FinalStage) _enemies.Add(Game.Context.Player);

        var colliders = Physics2D.OverlapCircleAll(_transform.position, _visionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject == _transform.gameObject) continue;
            if (colliders[i].TryGetComponent(out IInterestPoint interestPoint)) _interestPoints.Add(interestPoint);
            if (colliders[i].TryGetComponent(out IPlayer player)) _enemies.Add(player);
        }

        FindNearestPoint();
        FindNearestEnemy();
    }

    private void FindNearestPoint()
    {
        if (_interestPoints.Count <= 0) return;
        _nearestInterest = null;

        _distance = _visionRadius + 1;
        for (int i = 0; i < _interestPoints.Count; i++)
        {
            float distance = Vector2.Distance(_interestPoints[i].Transform.position, _transform.position);
            if (distance > _distance) continue;

            _distance = distance;
            _nearestInterest = _interestPoints[i];
            OnNearestInterestChange?.Invoke(_nearestInterest);
        }
    } 

    private void FindNearestEnemy()
    {
        if (_enemies.Count <= 0) return;
        _nearestEnemy = null;

        if (Game.Context.FinalStage)
        {
            _nearestEnemy = Game.Context.Player;
            OnNearestEnemyChange?.Invoke(_nearestEnemy);
            return;
        }

        _distance = _visionRadius + 1;
        for (int i = 0; i < _enemies.Count; i++)
        {
            float distance = Vector2.Distance(_enemies[i].Transform.position, _transform.position);
            if (distance > _distance) continue;

            _distance = distance;
            _nearestEnemy = _enemies[i];
            OnNearestEnemyChange?.Invoke(_nearestEnemy);
        }
    }

    private void ChangeVisionRadius(float currentExp)
    {
        _visionRadius = _baseVisionRadius + (_visionModifier / _level.ExpNeeded * currentExp + _level.PlayerLevel * _visionModifier);
    }
}
