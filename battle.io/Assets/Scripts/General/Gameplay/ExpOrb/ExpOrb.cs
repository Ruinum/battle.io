using UnityEngine;

public class ExpOrb : Interactable, IInterestPoint
{
    [SerializeField] private float _expAmount;

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
        ImpactUtils.CreatePopUp(Mathf.RoundToInt(_expAmount).ToString(), collision.transform.position, Color.black);
    }

    public void SetExp(float value)
    {
        _expAmount = value;
    }

    public void Active(Vector3 position, Quaternion rotation)
    {
        transform.localPosition = position;
        transform.localRotation = rotation;
        gameObject.SetActive(true);
        transform.SetParent(null);
    }

    private void ReturnToPool()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        gameObject.SetActive(false);
        transform.SetParent(RootPool);

        if (!RootPool) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        IsDestroyed = true;
    }
}