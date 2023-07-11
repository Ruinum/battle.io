using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
    private Collider2D _collider => GetComponent<Collider2D>();
    private float _damage;
    private int _randomDamage;

    private void Start()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Level>(out var level)) return;
        var damage = _damage + Random.Range(0, _randomDamage);
        level.RemoveExp(damage);

        ImpactUtils.CreatePopUp(damage.ToString(), collision.transform.position);    
    }

    public void Enable(float damage, float randomDamage)
    {
        _damage = damage;
        _randomDamage = (int)randomDamage;
        _collider.enabled = true;
    }

    public void Disable()
    {
        _damage = 0;
        _collider.enabled = false;
    }
}
