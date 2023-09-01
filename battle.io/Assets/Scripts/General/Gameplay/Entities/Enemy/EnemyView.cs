using UnityEngine;
using DG.Tweening;

public class EnemyView : MonoBehaviour
{
    private SpriteRenderer[] _spriteRenderers;

    private float _currentAlpha = 0;
    private float _speed = 1.5f;
    private bool _isShow;

    private void Start()
    {
        Initialize();
        Hide();
    }

    public void Initialize()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_isShow) return;

        _currentAlpha += Time.deltaTime * _speed;
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            var spriteRendered = _spriteRenderers[i];
            spriteRendered.color = new Color(spriteRendered.color.r, spriteRendered.color.g, spriteRendered.color.b, _currentAlpha);
        }

        if (_currentAlpha >= 1) _isShow = false;
    }

    public void Show()
    {
        _isShow = true;
    }    

    public void Hide()
    {
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            var spriteRendered = _spriteRenderers[i];
            spriteRendered.color = new Color(spriteRendered.color.r, spriteRendered.color.g, spriteRendered.color.b, 0);
        }
    }
}