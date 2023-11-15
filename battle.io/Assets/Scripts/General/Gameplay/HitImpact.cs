using Ruinum.Core.Systems;
using Ruinum.Utils;
using UnityEngine;

public class HitImpact
{
    [InjectAsset("Blood")] private GameObject _bloodParticle;
    [InjectAsset("Hit_0_0_Audio")] private AudioConfig _hitAudio;

    private ExpOrbPool _pool;
    private Transform _transform;
    private float _expCutModifier = 0.25f;
    private int _unloadDistance;

    public HitImpact(Level level, Transform transform)
    {
        level.OnExpRemove += OnExpRemove;
        _transform = transform;
        _pool = Game.Context.ExpOrbHitImpactPool;
        _unloadDistance = GameConstants.IMPACT_UNLOAD_DISTANCE;
    }

    private void OnExpRemove(float removedExp)
    {
        if (Game.Context.Player == null || Game.Context.Player == default) return;
        if (Vector2.Distance(_transform.position, Game.Context.Player.Transform.position) > _unloadDistance) return;

        int expOrbAmount = Random.Range(4, 8);
        float expOrbValue = removedExp / expOrbAmount * _expCutModifier;

        for (int i = 0; i < expOrbAmount; i++)
        {
            if (!_pool.TryGetPoolObject(out ExpOrb expOrb)) return;

            expOrb.Active(_transform.position, Quaternion.identity);
            var collider2d = expOrb.GetComponent<Collider2D>();
            collider2d.enabled = false;
           
            expOrb.SetExp(expOrbValue);           
            expOrb.DelayFade(5.5f);

            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            float randomSpeed = Random.Range(50f, 150f);

            SimulatePhysicUtils.Impulse(expOrb.gameObject, direction, randomSpeed, 0.75f);
            TimerSystem.Singleton.StartTimer(1f, () => collider2d.enabled = true);
        }

        AudioUtils.PlayAudio(_hitAudio, _transform.position);

        var bloodParticle = ObjectUtils.CreateGameObject<Transform>(_bloodParticle);
        bloodParticle.transform.position = _transform.position;
        Object.Destroy(bloodParticle.gameObject, 1.5f);
    }
}