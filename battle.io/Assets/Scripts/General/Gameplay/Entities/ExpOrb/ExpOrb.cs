using DG.Tweening;
using Ruinum.Core;
using Ruinum.Core.Interfaces;
using Ruinum.Core.Systems;
using Ruinum.Utils;
using UnityEngine;

public class ExpOrb : Interactable, IInterestPoint, IExecute, IMagnite
{
    [SerializeField] private AudioConfig _audioConfig;
    [SerializeField] private float _expAmount;
    [SerializeField] private string _poolName = "Exp Orb Pool";

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

            var find = GameObject.Find(_poolName);
            _rootPool = find == null ? null : find.transform;
            return _rootPool;
        }
    }

    public Transform Transform => transform;
    public bool IsDestroyed { get; set; }

    protected override void Interact(Collider2D collision)
    {
        if (!collision.TryGetComponent<Level>(out var level)) return;

        level.AddExp(_expAmount);

        OnInteract?.Invoke();
        ImpactUtils.TryCreatePopUp(Mathf.Max(1, Mathf.RoundToInt(_expAmount)).ToString(), collision.transform.position, Color.black, out var tmp);
        ReturnToPool();
    }

    protected override void PlayerInteract(Collider2D collision)
    {
        if (!collision.TryGetComponent<Level>(out var level)) return;

        AudioUtils.PlayAudio(_audioConfig, collision.transform.position);
        StatsSystem.Singleton.OnExpPickedEvent?.Invoke(_expAmount);
        Interact(collision);
    }

    public void Execute()
    {
        if (!_delayDestroy) return;
        _time -= Time.deltaTime;

        if (_time <= 1) _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, _time);

        if (_time >= 0) return;
        ReturnToPool();
    }

    public void Active(Vector3 position, Quaternion rotation)
    {
        Game.Context.ExpOrbs.Add(this);
        ExecuteSystem.Singleton.AddExecute(this);
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;

        _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, 0);
        _spriteRenderer.DOFade(1, 0.8f);

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
        if (_timer != null) _timer.RemoveTimer();

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
        transform.SetParent(RootPool);

        _time = 0;
        _delayDestroy = false;
        _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, 1);

        Game.Context.ExpOrbs.Remove(this);
        ExecuteSystem.Singleton.RemoveExecute(this);
        
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