using Ruinum.Core.Systems;
using System.Collections.Generic;
using UnityEngine;

public class SimulatePhysicSystem : System<SimulatePhysicSystem>
{
    private List<Rigidbody2D> _rigidbodies;
    private float _deceleration = 1.25f;

    public override void Initialize()
    {
        _rigidbodies = new List<Rigidbody2D>();
    }

    public override void Execute()
    {
        for (int i = 0; i < _rigidbodies.Count; i++)
        {
            var rigidbody = _rigidbodies[i];
            float value = _deceleration * Time.deltaTime;
            rigidbody.velocity -= rigidbody.velocity - new Vector2(value, value);

            if (rigidbody.velocity.x <= 0 && rigidbody.velocity.y <= 0) { _rigidbodies.Remove(rigidbody); }
        }
    }

    public void ImpulseObject(GameObject gameObject, Vector3 direction, float speed, float lifeTime = 0.5f, float mass = 0)
    {
        if (!gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody2d)) rigidbody2d = gameObject.AddComponent<Rigidbody2D>();
        if (rigidbody2d == null) return;
        if (mass == 0) rigidbody2d.mass = Random.Range(0.5f, 2);
        else rigidbody2d.mass = 1;
        rigidbody2d.gravityScale = 0;
        rigidbody2d.AddForce(direction * speed);

        TimerSystem.Singleton.StartTimer(lifeTime, () => { Object.Destroy(rigidbody2d); _rigidbodies.Remove(rigidbody2d); });
    }
}
