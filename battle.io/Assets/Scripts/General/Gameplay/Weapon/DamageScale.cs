public class DamageScale
{
    private Level _level;

    private float _baseDamageScale => BASE_DAMAGE_MODIFIER + MODIFIER_SCALE * _level.PlayerLevel;
    private float _randomDamageScale => RANDOM_DAMAGE_MODIFIER + MODIFIER_SCALE * _level.PlayerLevel;

    private const float BASE_DAMAGE_MODIFIER = 5f;
    private const float RANDOM_DAMAGE_MODIFIER = 3f;
    private const float MODIFIER_SCALE = 2f;

    public DamageScale(Level level)
    {
        _level = level;
    }

    public float ScaleBaseDamage(float damage) => damage + (damage / _level.ExpNeeded * _level.Exp + _baseDamageScale * _level.PlayerLevel);
    public float ScaleRandomDamage(float damage) => damage + (damage / _level.ExpNeeded * _level.Exp + _randomDamageScale * _level.PlayerLevel);
}