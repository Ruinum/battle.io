using Ruinum.Core.Systems;
using Ruinum.Utils;
using UnityEngine;

public class HitImpact
{
    [InjectAsset("ExpOrb")] private GameObject _expOrb;
    [InjectAsset("Blood")] private GameObject _bloodParticle;
    [InjectAsset("Hit_0_0_Audio")] private AudioConfig _hitAudio;

    private Transform _transform;
    private float _expCutModifier = 0.25f;

    public HitImpact(Level level, Transform transform)
    {
        level.OnExpRemove += OnExpRemove;
        _transform = transform;
    }

    private void OnExpRemove(float removedExp)
    {
        int expOrbAmount = Random.Range(4, 8);
        float expOrbValue = removedExp / expOrbAmount * _expCutModifier;

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

        AudioUtils.PlayAudio(_hitAudio, _transform.position);

        var bloodParticle = ObjectUtils.CreateGameObject<Transform>(_bloodParticle);
        bloodParticle.transform.position = _transform.position;
        Object.Destroy(bloodParticle.gameObject, 1.5f);
    }
}