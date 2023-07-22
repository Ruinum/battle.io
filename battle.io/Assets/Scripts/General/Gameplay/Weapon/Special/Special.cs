using UnityEngine;

public abstract class Special : MonoBehaviour
{
    protected GameObject _creator;
    protected WeaponInfo _weaponInfo;

    protected abstract void OnInitialize();
    public void Initialize(WeaponInfo weaponInfo, GameObject creator)
    {
        _creator = creator;
        _weaponInfo = weaponInfo;

        OnInitialize();
    }
}