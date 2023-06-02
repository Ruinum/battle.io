using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
     
    private Rigidbody2D _rigidbody2D;    

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Horizontal")).normalized;
        _rigidbody2D.AddForce(new Vector2(direction.x * _speed, direction.y * _speed));
    }
}
