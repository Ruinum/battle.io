using UnityEngine;

public class ExpOrb : Interactable, IInterestPoint
{
    [SerializeField] private float _expAmount;

    protected override void Interact(Collider2D collision)
    {
        if (!collision.TryGetComponent<Level>(out var level)) return;

        level.AddExp(_expAmount);
        Destroy(gameObject);
    }

    public void SetExp(float value)
    {
        _expAmount = value;
    }
}