using UnityEngine;

public class CameraView
{
    public Camera _camera;

    private float _currentSize = 8;
    private float _speed = 0.75f;

    private float _yModifier = 1;
    private int _level = 1;

    public CameraView(Camera camera, Level level)
    {
        _camera = camera;
        level.OnExpChange += ChangeCameraView;
        level.OnLevelChange += OnLevelChange;
    }

    public void Execute()
    {
        if (_camera.orthographicSize >= _currentSize) return;
        
        _camera.orthographicSize += _speed * Time.deltaTime;

        if (_camera.orthographicSize >= _currentSize) _camera.orthographicSize = _currentSize;
    }

    private void ChangeCameraView(float currentExpAmount)
    {
        _currentSize = 8 + CalculateModifier(currentExpAmount);
    }

    private void OnLevelChange(int level)
    {
        _level = level;
    }

    private float CalculateModifier(float currentExp)
    {
        return _yModifier / 100 * currentExp + _level * _yModifier;
    }    
}