public class HitBoxEvents : EventHandler
{
    private DamageScale _damageScale;
    private HitBox[] _weaponMainHitBoxes;
    private HitBox[] _weaponSubHitBoxes;

    public HitBoxEvents(Level level, PlayerAnimatorController controller, WeaponInventory inventory) : base(controller, inventory)
    {
        controller.SubscribeOnTimelineEvent("HitBoxEnable", () => EnableHitBoxes(_weaponMainHitBoxes, _mainWeaponInfo));
        controller.SubscribeOnTimelineEvent("HitBoxDisable", () => DisableHitBoxes(_weaponMainHitBoxes, _mainWeaponInfo));

        controller.SubscribeOnTimelineEvent("HitBoxSubEnable", () => EnableHitBoxes(_weaponSubHitBoxes, _subWeaponInfo));
        controller.SubscribeOnTimelineEvent("HitBoxSubDisable", () => DisableHitBoxes(_weaponSubHitBoxes, _subWeaponInfo));

        _damageScale = new DamageScale(level);
        _weaponMainHitBoxes = new HitBox[0];
        _weaponSubHitBoxes = new HitBox[0];
    }

    private void EnableHitBoxes(HitBox[] hitBoxes, WeaponInfo weaponInfo)
    {
        for (int i = 0; i < hitBoxes.Length; i++)
        {
            hitBoxes[i].Enable(_damageScale.ScaleBaseDamage(weaponInfo.Damage), _damageScale.ScaleRandomDamage(weaponInfo.RandomDamage));
        }
    }

    private void DisableHitBoxes(HitBox[] hitBoxes, WeaponInfo weaponInfo)
    {
        for (int i = 0; i < hitBoxes.Length; i++)
        {
            hitBoxes[i].Disable();
        }
    }

    protected override void WeaponMainChange()
    {
        _weaponMainHitBoxes = _mainWeapon.GetComponentsInChildren<HitBox>();
    }

    protected override void WeaponSubChange()
    {
        UnityEngine.Debug.LogWarning("Change sub weapon");
        _weaponSubHitBoxes = _subWeapon.GetComponentsInChildren<HitBox>();
    }
}