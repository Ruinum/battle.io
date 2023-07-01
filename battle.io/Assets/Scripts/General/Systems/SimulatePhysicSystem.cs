using Ruinum.Core;
using System.Collections.Generic;
using UnityEngine;

public class SimulatePhysicSystem : BaseSingleton<SimulatePhysicSystem>
{
    private List<Impulse> _impulses;
    private float _deceleration = 1f;

    private void Start()
    {
        _impulses = new List<Impulse>();
    }

    private void Update()
    {
        for (int i = 0; i < _impulses.Count; i++)
        {
            var impulse = _impulses[i];
            
            var interpolation = impulse.Speed * Time.deltaTime;
            impulse.Speed -= _deceleration * Time.deltaTime;

            Debug.LogWarning(impulse.Speed);
            Debug.LogWarning(interpolation);

            impulse.Transform.position = Vector3.Lerp(impulse.StartPosition, impulse.Destination, interpolation);           

            if (Vector2.Distance(impulse.Transform.position, impulse.Destination) <= 0.15f) _impulses.Remove(impulse);
            if (impulse.Speed <= 0) _impulses.Remove(impulse);
        }
    }

    public void ImpulseObject(GameObject gameObject, Vector3 position, float speed)
    {
        _impulses.Add(new Impulse(gameObject.transform, position, speed));
    }

    private class Impulse
    {
        public Impulse(Transform transform, Vector2 destination, float speed)
        {
            Transform = transform;
            Destination = destination;
            StartPosition = transform.position;
            Speed = speed;
        }

        public Transform Transform;
        public Vector2 Destination;
        public Vector2 StartPosition;
        public float Speed;
    }
}
