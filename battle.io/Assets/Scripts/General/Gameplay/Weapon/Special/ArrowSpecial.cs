using Ruinum.Core.Systems;
using Ruinum.Utils;
using UnityEngine;

[RequireComponent(typeof(HitBox))]
public class ArrowSpecial : Special
{
    [SerializeField] private GameObject _destroyParticle;
    [SerializeField] private float _speed = 350f;
    [SerializeField] private float _lifeTime = 0.5f;
    
    protected override void OnInitialize()
    {
        var hitBox = GetComponent<HitBox>();
        hitBox.Ignore(_creator);

        var damageScale = new DamageScale(_level);
        TimerSystem.Singleton.StartTimer(0.15f, () => hitBox.Enable(damageScale.ScaleBaseDamage(_weaponInfo.Damage), damageScale.ScaleRandomDamage(_weaponInfo.RandomDamage)));
        SimulatePhysicUtils.Impulse(gameObject, _creator.transform.up, _speed, _lifeTime, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var destroyParticle = Instantiate(_destroyParticle, transform.position, Quaternion.identity, null);
        Destroy(destroyParticle, 1f);
        Destroy(gameObject);
    }
}