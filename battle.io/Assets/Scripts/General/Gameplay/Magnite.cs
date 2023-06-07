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
            if (!colliders[i].TryGetComponent(out ExpOrb expOrb)) continue;
            expOrb.transform.position = Vector2.MoveTowards(expOrb.transform.position, _transform.position, _speed * Time.deltaTime);
        }
    }
}
