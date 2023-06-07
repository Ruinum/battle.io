using UnityEngine;

public class ScaleView
{
    private Transform _transform;
    private Vector3 _startScale;
    private float _scaleModifier = 1;

    public ScaleView(Transform transform)
    {
        _transform = transform;
        _startScale = new Vector3(1, 1, 1);
    }

    public void ChangeScale(float scaleModifier)
    {
        _scaleModifier = scaleModifier;
        _transform.localScale = _startScale * _scaleModifier;
    }
}