public class HitBoxEvents : EventHandler
{
    private DamageScale _damageScale;
    private HitBox[] _weaponHitBoxes;

    public HitBoxEvents(Level level, PlayerAnimatorController controller, WeaponInventory inventory) : base(controller, inventory)
    {
        controller.SubscribeOnTimelineEvent("HitBoxEnable", EnableHitBox);
        controller.SubscribeOnTimelineEvent("HitBoxDisable", DisableHitBox);

        _damageScale = new DamageScale(level);
        _weaponHitBoxes = new HitBox[0];
    }

    private void EnableHitBox()
    {
        for (int i = 0; i < _weaponHitBoxes.Length; i++)
        {
            _weaponHitBoxes[i].Enable(_damageScale.ScaleBaseDamage(_weaponInfo.Damage), _damageScale.ScaleRandomDamage(_weaponInfo.RandomDamage));
        }
    }

    private void DisableHitBox()
    {
        for (int i = 0; i < _weaponHitBoxes.Length; i++)
        {
            _weaponHitBoxes[i].Disable();
        }
    }

    protected override void WeaponChange()
    {
        _weaponHitBoxes = _weapon.GetComponentsInChildren<HitBox>();
    }
}
