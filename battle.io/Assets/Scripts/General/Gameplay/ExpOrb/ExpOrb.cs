using UnityEngine;

public class ExpOrb : Interactable, IInterestPoint
{
    [SerializeField] private float _expAmount;

    private Transform _rootPool;
    private float _baseExpAmount = 5;
    private float _scaleModifier = 0.05f;
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

    protected override void Interact(Collider2D collision)
    {
        if (!collision.TryGetComponent<Level>(out var level)) return;

        level.AddExp(_expAmount);
        ReturnToPool();

        OnInteract?.Invoke();
    }

    public void SetExp(float value)
    {
        _expAmount = value;
    }

    private void ChangeScale()
    {
        transform.localScale = new Vector2(1 + (_expAmount - _baseExpAmount) * _scaleModifier, 1);
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
}