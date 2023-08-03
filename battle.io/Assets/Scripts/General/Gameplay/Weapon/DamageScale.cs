public class DamageScale
{
    public float BaseDamageScale => BASE_DAMAGE_MODIFIER + MODIFIER_SCALE * _level.PlayerLevel;
    public float RandomDamageScale => RANDOM_DAMAGE_MODIFIER + MODIFIER_SCALE * _level.PlayerLevel;

    private Level _level;

    private const float BASE_DAMAGE_MODIFIER = 4f;
    private const float RANDOM_DAMAGE_MODIFIER = 0.35f;
    private const float MODIFIER_SCALE = 1.55f;

    public DamageScale(Level level)
    {
        _level = level;
    }

    public float ScaleBaseDamage(float damage) => damage + (damage / _level.ExpNeeded * _level.Exp + BaseDamageScale * _level.PlayerLevel);
    public float ScaleRandomDamage(float damage) => damage + (damage / _level.ExpNeeded * _level.Exp + RandomDamageScale * _level.PlayerLevel);
}