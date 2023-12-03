using UnityEngine;

public class Magnite
{
    private Transform _transform;
    private float _speed;
    private float _radius;

    public Magnite(Transform transform, float speed, float radius)
    {
        _transform = transform;
        _speed = speed;
        _radius = radius;
    }

    public void Execute()
    {
        var colliders = Physics2D.OverlapCircleAll(_transform.position, _radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].TryGetComponent(out IMagnite magniteObject)) continue;
            magniteObject.Transform.position = Vector2.MoveTowards(magniteObject.Transform.position, _transform.position, _speed * Time.deltaTime);
        }
    }
}
