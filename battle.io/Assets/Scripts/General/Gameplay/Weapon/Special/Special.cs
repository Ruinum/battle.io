using UnityEngine;

public abstract class Special : MonoBehaviour
{
    protected Level _level; 
    protected GameObject _creator;
    protected WeaponInfo _weaponInfo;

    protected abstract void OnInitialize();
    public void Initialize(Level level, WeaponInfo weaponInfo, GameObject creator)
    {
        _level = level;
        _creator = creator;
        _weaponInfo = weaponInfo;

        OnInitialize();
    }
}