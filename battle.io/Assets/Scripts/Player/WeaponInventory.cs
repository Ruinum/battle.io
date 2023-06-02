using UnityEngine;
using Ruinum.Utils;

public class WeaponInventory : MonoBehaviour
{
    private RightArm _rightArm;
    private LeftArm _leftArm;

    [SerializeField] WeaponInfo _startWeapon;
    private WeaponInfo _currentWeaponInfo;

    private void Start()
    {
        _rightArm = GetComponentInChildren<RightArm>();
        _leftArm = GetComponentInChildren<LeftArm>();
        
        EquipWeapon(_startWeapon);
    }

    public void EquipWeapon(WeaponInfo weaponInfo)
    {
        _currentWeaponInfo = weaponInfo;
        GameObject createdWeapon = Object.Instantiate(_currentWeaponInfo.Prefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity, transform);
        HandleArms(createdWeapon);
    }

    private void HandleArms(GameObject createdWeapon)
    {
        createdWeapon.TryGetComponentInObject(out RightArmPosition rightArm);
        createdWeapon.TryGetComponentInObject(out LeftArmPosition leftArm);

        if (rightArm) _rightArm.SetChild(createdWeapon.transform);
        if (leftArm) _leftArm.SetChild(createdWeapon.transform);
        
        if (rightArm && leftArm) 
        { 
            _rightArm.SetChild(createdWeapon.transform, false); 
            _leftArm.SetChild(createdWeapon.transform, false); 
        }
    }
}
