using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : IMovement
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private Vector2 _point = new Vector2(0, 0);
    
    private float _speed;
    private float _modifier = 1;
    private float _magnitude = 0.8f;

    public Action OnDestination;

    public float Speed => _speed * Modifier;
    public float Modifier { get => _modifier; set { _modifier = value; } }

    public EnemyMovement(NavMeshAgent agent, float speed)
    {
        _agent = agent;
        _transform = _agent.transform;
        _speed = speed;
    }

    public void SetPoint(Vector2 point)
    {
        _point = point;
        _agent.SetDestination(point);
    }

    public void Execute()
    {
        if (Vector2.Distance(_transform.position, _point) <= _magnitude) OnDestination?.Invoke();
    }
}