using Ruinum.Core.Systems;
using Ruinum.Utils;
using UnityEngine;

public class KnightAbility : IClassAbility
{
    private Transform _transform;
    private ClassConfig _config;
    private Level _level;
    private Multiplier _passiveReduction;
    private Multiplier _activeReduction;

    private GameObject _wavePrefab;
    private DamageScale _damageScale;

    private float _baseDamage = 4f;
    private float _randomDamage = 2f;
    private float _activeReductionTime = 2f;

    public void Initialize(GameObject player, ClassConfig config)
    {
        _transform = player.transform;
        _config = config;
        _level = player.GetComponent<Level>();
        _level.AddMultiplier(_passiveReduction);

        _passiveReduction = new Multiplier() { Value = -0.05f, Type = MultiplierType.Negative };
        _activeReduction = new Multiplier() { Value = -0.15f, Type = MultiplierType.Negative };

        _wavePrefab = _config.Particle;
        _damageScale = new DamageScale(_level);
    }

    public void UseAbility()
    {
        _level.AddMultiplier(_activeReduction);
        TimerSystem.Singleton.StartTimer(_activeReductionTime, () => _level.RemoveMultiplier(_activeReduction));

        HitBox waveHitbox = ObjectUtils.CreateGameObject<HitBox>(_wavePrefab, _transform.position);
        waveHitbox.Ignore(_transform.gameObject);
        waveHitbox.Enable(_damageScale.ScaleBaseDamage(_baseDamage), _damageScale.ScaleRandomDamage(_randomDamage));
        
        Object.Destroy(waveHitbox, 1.5f);
    }
}
