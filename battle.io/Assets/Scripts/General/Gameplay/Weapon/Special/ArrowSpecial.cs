using Ruinum.Core;
using Ruinum.Core.Systems;
using UnityEngine;

[RequireComponent(typeof(HitBox))]
public class ArrowSpecial : Special
{
    [SerializeField] private float _speed = 350f;
    [SerializeField] private float _lifeTime = 0.5f;

    protected override void OnInitialize()
    {
        var hitBox = GetComponent<HitBox>();
        TimerSystem.Singleton.StartTimer(0.1f, () => hitBox.Enable(_weaponInfo.Damage, _weaponInfo.RandomDamage));
        SimulatePhysicUtils.Impulse(gameObject, _creator.transform.up, _speed, _lifeTime, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}