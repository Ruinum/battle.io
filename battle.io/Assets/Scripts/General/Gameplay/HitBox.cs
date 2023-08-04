using Ruinum.Utils;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
    private Collider2D _collider;
    private float _damage;
    private float _randomDamage;

    private bool _initialized;

    private GameObject _owner;
    private Dictionary<Collider2D, byte> _collisions;

    private void Start()
    {
        if (_initialized) return;
        Initialize();
    }

    public void Initialize()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;

        gameObject.layer = 6; //Layer HitBox
        _collisions = new Dictionary<Collider2D, byte>();
        
        _initialized = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Level>(out var level)) return;
        if (_owner != null) if (collision == _owner) return; 
        if (_collisions.TryGetValue(collision, out var @byte)) return;
        _collisions.Add(collision, 0);

        var damage = _damage + Random.Range(0, _randomDamage);
        level.RemoveExp(damage);

        ImpactUtils.CreatePopUp(Mathf.RoundToInt(damage).ToString(), collision.transform.position, Color.red);    
    }

    public void Ignore(GameObject owner) => _owner = owner;

    public void Enable(float damage, float randomDamage)
    {
        _damage = damage;
        _randomDamage = randomDamage;
        _collider.enabled = true;

        _collisions.Clear();
    }

    public void Disable()
    {
        _damage = 0;
        _collider.enabled = false;

        _collisions.Clear();
    }
}
