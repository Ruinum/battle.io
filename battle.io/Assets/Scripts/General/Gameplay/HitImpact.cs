using Ruinum.Core.Systems;
using UnityEngine;

public class HitImpact
{
    private Transform _transform;
    [InjectAsset("ExpOrb")] private GameObject _expOrb;

    public HitImpact(Level level, Transform transform)
    {
        level.OnExpRemove += OnExpRemove;
        _transform = transform;
    }

    private void OnExpRemove(float removedExp)
    {
        int expOrbAmount = Random.Range(4, 8);
        float expOrbValue = removedExp / expOrbAmount * 0.75f;

        for (int i = 0; i < expOrbAmount; i++)
        {
            ExpOrb expOrb = Object.Instantiate(_expOrb, _transform.position, Quaternion.identity, null).GetComponent<ExpOrb>();
            var collider2d = expOrb.GetComponent<Collider2D>();
            collider2d.enabled = false;
            expOrb.SetExp(expOrbValue);

            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            float randomSpeed = Random.Range(50f, 150f);

            SimulatePhysicUtils.Impulse(expOrb.gameObject, direction, randomSpeed);
            TimerSystem.Singleton.StartTimer(1f, () => collider2d.enabled = true);
        }
    }
}