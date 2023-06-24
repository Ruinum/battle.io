using System.Collections.Generic;
using UnityEngine;

public class EnemyVision
{
    public List<IInterestPoint> InterestPoint => _interestPoints;
    public List<IPlayer> Enemies => _enemies;
    public IInterestPoint NearestInterest => _nearestInterest;
    public IPlayer NearestEnemy => _nearestEnemy;

    private Transform _transform;
    private List<IInterestPoint> _interestPoints;
    private List<IPlayer> _enemies;

    private IInterestPoint _nearestInterest;
    private IPlayer _nearestEnemy;

    private float _visionRadius;
    private float _distance;

    public EnemyVision(Transform transform, float visionRadius)
    {
        _transform = transform;
        _visionRadius = visionRadius;

        _interestPoints = new List<IInterestPoint>();
        _enemies = new List<IPlayer>();
    }

    public void Execute()
    {
        _enemies.Clear();
        _interestPoints.Clear();

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
        for (int i = 0; i < _interestPoints.Capacity; i++)
        {
            if (Vector2.Distance(_interestPoints[i].Transform.position, _transform.position) <= _distance) _nearestInterest = _interestPoints[i];
        }
    }

    private void FindNearestEnemy()
    {
        if (_enemies.Count <= 0) return;

        _nearestEnemy = null;
        _distance = _visionRadius + 1;
        for (int i = 0; i < _enemies.Capacity; i++)
        {
            if (Vector2.Distance(_enemies[i].Transform.position, _transform.position) <= _distance) _nearestEnemy = _enemies[i];
        }
    }
}
