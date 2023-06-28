using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
    private Collider2D _collider => GetComponent<Collider2D>();
    private float _damage;

    private void Start()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Level>(out var level)) return;
        level.RemoveExp(_damage);
    }

    public void Enable(float damage)
    {
        Debug.LogError("Enable Hit box");
        _damage = damage;
        _collider.enabled = true;
    }

    public void Disable()
    {
        Debug.LogError("Disable Hit box");
        _damage = 0;
        _collider.enabled = false;
    }
}
