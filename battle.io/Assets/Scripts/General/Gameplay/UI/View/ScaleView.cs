using UnityEngine;

public class ScaleView
{
    private Transform _transform;
    private Vector3 _startScale;
    private float _scaleModifier = 1;
    private float _scaleCap = 1.5f;

    public ScaleView(Transform transform)
    {
        _transform = transform;
        _startScale = new Vector3(1, 1, 1);
    }

    public void ChangeScale(float scaleModifier)
    {
        if (_transform.localScale.x >= _scaleCap) return;

        _scaleModifier = scaleModifier;
        _transform.localScale = _startScale * _scaleModifier;

        if (_transform.localScale.x >= _scaleCap) _transform.localScale = new Vector3(_scaleCap, _scaleCap, _scaleCap);
    }
}