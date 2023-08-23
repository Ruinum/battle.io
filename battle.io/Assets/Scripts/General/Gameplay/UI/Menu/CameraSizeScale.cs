using UnityEngine;

public class CameraSizeScale : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private float _maxScale;

    private void Update()
    {
        if (_camera.orthographicSize >= _maxScale) return;
        
        _camera.orthographicSize += Time.deltaTime * _scaleSpeed;

        if (_camera.orthographicSize >= _maxScale) _camera.orthographicSize = _maxScale;
    }
}