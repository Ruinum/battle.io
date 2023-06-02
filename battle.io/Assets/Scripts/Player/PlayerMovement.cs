using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
     
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {        
        _rigidbody2D.velocity = new Vector2(_direction.x * _speed, _direction.y * _speed);
    }
}
