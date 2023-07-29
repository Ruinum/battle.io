using Ruinum.Core;
using UnityEngine;

public class Movement : Executable, IMovement
{
    [SerializeField] private float _speed;
     
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private float _modifier = 1;

    public float Speed => _speed * Modifier;
    public float Modifier { get => _modifier; set { _modifier = value; } }

    public override void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        base.Start();
    }

    public override void Execute()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody2D.velocity = new Vector2(_direction.x * Speed, _direction.y * Speed);
    }
}
