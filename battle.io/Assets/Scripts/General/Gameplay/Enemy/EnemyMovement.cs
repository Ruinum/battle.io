using UnityEngine;

public class EnemyMovement
{
    private Rigidbody2D rigidbody;
    private Vector2 _point = new Vector2(0, 0);
    private float _speed;

    public EnemyMovement(Rigidbody2D rigidBody, float speed)
    {
        rigidbody = rigidBody;
        _speed = speed;
    }

    public void SetPoint(Vector2 point) => _point = point;
    
    public void Move()
    {
        rigidbody.position = Vector2.MoveTowards(rigidbody.position, _point, _speed * Time.deltaTime);
        rigidbody.velocity = (_point - rigidbody.position).normalized * _speed;
    }
}