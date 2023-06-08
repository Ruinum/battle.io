using System;
using UnityEngine;

public class EnemyMovement : IMovement
{
    private Rigidbody2D rigidbody;
    private Vector2 _point = new Vector2(0, 0);
    
    private float _speed;
    private float _modifier = 1;
    private float _magnitude = 0.8f;

    public Action OnDestination;

    public float Speed => _speed * Modifier;
    public float Modifier { get => _modifier; set { _modifier = value; } }

    public EnemyMovement(Rigidbody2D rigidBody, float speed)
    {
        rigidbody = rigidBody;
        _speed = speed;
    }

    public void SetPoint(Vector2 point) => _point = point; 
    public void Move()
    {
        if (Vector2.Distance(rigidbody.position, _point) <= _magnitude) OnDestination?.Invoke();

        rigidbody.position = Vector2.MoveTowards(rigidbody.position, _point, Speed * Time.deltaTime);
        rigidbody.velocity = (_point - rigidbody.position).normalized * Speed;
    }
}