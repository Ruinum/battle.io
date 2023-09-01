using Ruinum.Core;
using Ruinum.Core.Systems;
using Ruinum.Utils;
using UnityEngine;

public class ExpOrb : Interactable, IInterestPoint
{
    [SerializeField] private AudioConfig _audioConfig;
    [SerializeField] private float _expAmount;

    private SpriteRenderer _spriteRenderer;
    private Timer _timer;
    private Color _color;
    private float _time;
    private bool _delayDestroy;

    private Transform _rootPool;
    public Transform RootPool
    {
        get
        {
            if (_rootPool) return _rootPool;

            var find = GameObject.Find("Exp Orb Pool");
            _rootPool = find == null ? null : find.transform;
            return _rootPool;
        }
    }

    public Transform Transform => transform;
    public bool IsDestroyed { get; set; }

    protected override void Interact(Collider2D collision)
    {
        if (!collision.TryGetComponent<Level>(out var level)) return;

        ReturnToPool();
        level.AddExp(_expAmount);

        OnInteract?.Invoke();
        if (collision.tag == "Player") AudioUtils.PlayAudio(_audioConfig, collision.transform.position);
        ImpactUtils.CreatePopUp(Mathf.Max(1, Mathf.RoundToInt(_expAmount)).ToString(), collision.transform.position, Color.black);
    }

    private void Update()
    {
        if (!_delayDestroy) return;
        _time -= Time.deltaTime;

        if(_time <= 1) _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, _time);

        if (_time >= 0) return;
        ReturnToPool();
    }

    public void Active(Vector3 position, Quaternion rotation)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;

        transform.localPosition = position;
        transform.localRotation = rotation;
        gameObject.SetActive(true);
        transform.SetParent(null);
    }

    public void DelayFade(float time)
    {
        _time = time;
        _timer = TimerSystem.Singleton.StartTimer(_time, () => 
        { 
            if (gameObject.activeSelf) ReturnToPool();
            _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, 1);         
        });

        _timer.OnTimeChange += Fade;
    }

    public void DelayDestroy(float time)
    {
        _delayDestroy = true;
        _time = time;
    }

    private void Fade(float currentTime, float startTime)
    {
        _color = new Color(_color.r, _color.g, _color.b, currentTime / _time);
        _spriteRenderer.color = _color;
    }

    public void ReturnToPool()
    {
        if(_timer != null) _timer.RemoveTimer();

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
        transform.SetParent(RootPool);

        _time = 0;
        _delayDestroy = false;

        if (!RootPool) Destroy(gameObject);
    }

    public void SetExp(float value)
    {
        _expAmount = value;
    }

    private void OnDestroy()
    {
        IsDestroyed = true;
    }
}