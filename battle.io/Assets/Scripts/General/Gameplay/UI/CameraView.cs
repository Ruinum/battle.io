using UnityEngine;

public class CameraView
{
    public Camera _camera;

    private Level _level;
    private float _currentSize;
    private float _baseSize = 7;
    private float _maxSize = 10;
    private float _speed = 0.15f;

    private float _yModifier = 1;

    public CameraView(Camera camera, Level level)
    {
        _camera = camera;
        _level = level;
        
        level.OnExpChange += ChangeCameraView;
    }

    public void Execute()
    {
        if (_camera.orthographicSize >= _maxSize) return;
        if (_camera.orthographicSize >= _currentSize) return;
        
        _camera.orthographicSize += _speed * Time.deltaTime;

        if (_camera.orthographicSize >= _currentSize) _camera.orthographicSize = _currentSize;
        if (_camera.orthographicSize >= _maxSize) _camera.orthographicSize = _maxSize;
    }

    private void ChangeCameraView(float currentExpAmount)
    {
        _currentSize = _baseSize + CalculateModifier(currentExpAmount);
    }

    private float CalculateModifier(float currentExp)
    {
        return _yModifier / _level.ExpNeeded * currentExp + _level.PlayerLevel * _yModifier;
    }    
}