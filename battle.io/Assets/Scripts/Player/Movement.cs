using Ruinum.Core;
using UnityEngine;

public class Movement : Executable
{
    [SerializeField] private float _speed;
     
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private float _currentSpeed;

    public override void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentSpeed = _speed;
        
        base.Start();
    }

    public override void Execute()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {        
        _rigidbody2D.velocity = new Vector2(_direction.x * _currentSpeed, _direction.y * _speed);
    }
}
