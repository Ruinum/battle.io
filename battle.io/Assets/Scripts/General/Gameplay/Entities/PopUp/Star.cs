using DG.Tweening;
using Ruinum.Core.Interfaces;
using Ruinum.Core.Systems;
using Ruinum.Utils;
using UnityEngine;

public class Star : PoolObject, IExecute, IInterestPoint, IMagnite
{
    [SerializeField] private AudioConfig _audioConfig;

    private SpriteRenderer _spriteRenderer;
    private Color _color;

    private float _time;
    private bool _delayDestroy;

    public Transform Transform => transform;
    public bool IsDestroyed { get; set; }

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        SimpleScale();

        Game.Context.Stars.Add(this);
        ExecuteSystem.Singleton.AddExecute(this);
    }

    private void Awake()
    {
        gameObject.layer = 9; //Layer Interactable
        OnReturnToPool += OnReturnPool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<IPlayer>(out var player)) return;
        if (collision.tag == "Player") Interact(collision);
        else ReturnToPool();
    }

    private void Interact(Collider2D collision)
    {
        AudioUtils.PlayAudio(_audioConfig, collision.transform.position);
        ImpactUtils.TryCreatePopUp("1", collision.transform.position, Color.black, out var tmp);
        StatsSystem.Singleton.OnStarPickedEvent?.Invoke(1);
        ReturnToPool();
    }

    private void OnReturnPool()
    {
        Game.Context.Stars.Remove(this);
        ExecuteSystem.Singleton.RemoveExecute(this);
    }

    public void DelayDestroy(float time)
    {
        _delayDestroy = true;
        _time = time;
    }

    private void SimpleScale()
    {
        transform.DOScale(1.2f, 1f).OnComplete(() => transform.DOScale(0.8f, 1f).OnComplete(() => SimpleScale()));
    }

    public void Execute()
    {
        if (!_delayDestroy) return;
        _time -= Time.deltaTime;

        if (_time <= 1) _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, _time);

        if (_time >= 0) return;
        ReturnToPool();
    }
}