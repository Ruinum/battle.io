using Ruinum.Core;
using System.Collections.Generic;
using UnityEngine;

public class SimulatePhysicSystem : BaseSingleton<SimulatePhysicSystem>
{
    private List<Rigidbody2D> _rigidbodies;
    private float _deceleration = 1.25f;

    private void Start()
    {
        _rigidbodies = new List<Rigidbody2D>();
    }

    private void Update()
    {
        for (int i = 0; i < _rigidbodies.Count; i++)
        {
            var rigidbody = _rigidbodies[i];
            float value = _deceleration * Time.deltaTime;
            rigidbody.velocity -= rigidbody.velocity - new Vector2(value, value);

            if (rigidbody.velocity.x <= 0 && rigidbody.velocity.y <= 0) { _rigidbodies.Remove(rigidbody); }
        }
    }

    public void ImpulseObject(GameObject gameObject, Vector3 direction, float speed)
    {
        Rigidbody2D rigidbody2d = gameObject.AddComponent<Rigidbody2D>();
        rigidbody2d.mass = Random.Range(0.5f, 2);
        rigidbody2d.gravityScale = 0;
        rigidbody2d.AddForce(direction * speed);

        Object.Destroy(rigidbody2d, 0.5f);
    }
}
