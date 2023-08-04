using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ColliderRadiuis : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;

    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (_collider.radius >= _radius) Destroy(this);

        _collider.radius += _speed * Time.deltaTime;
        
        if (_collider.radius >= _radius) _collider.radius = _radius;
    }
}
