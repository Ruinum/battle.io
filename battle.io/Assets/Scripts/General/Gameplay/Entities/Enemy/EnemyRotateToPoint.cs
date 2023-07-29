using UnityEngine;

public class EnemyRotateToPoint 
{
    private Transform _transform;
    private Quaternion _rotation;
    private float _speed = 225f;

    private float _baseSpeed = 100f;
    private float _minRandomSpeed = 50f;
    private float _maxRandomSpeed = 100f;

    public EnemyRotateToPoint(Transform transform)
    {
        _transform = transform;
    }

    public void Execute()
    {
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, _rotation, _speed * Time.deltaTime);
    }

    public void SetPoint(Vector3 point)
    {
        _rotation = Quaternion.LookRotation(point - _transform.position, new Vector3(0, 0, 1));
        _speed = _baseSpeed + Random.Range(_minRandomSpeed, _maxRandomSpeed);
    }
}
