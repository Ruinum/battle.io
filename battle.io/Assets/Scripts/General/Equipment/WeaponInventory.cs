using UnityEngine;
using Ruinum.Utils;

public class WeaponInventory : MonoBehaviour
{
    private GameObject _currentRightWeapon;
    private GameObject _currentLeftWeapon;
    private WeaponInfo _currentWeaponInfo;
    
    private RightArm _rightArm;
    private LeftArm _leftArm;

    private void Start()
    {
        _rightArm = GetComponentInChildren<RightArm>();
        _leftArm = GetComponentInChildren<LeftArm>();       
    }

    public void EquipWeapon(WeaponInfo weaponInfo)
    {
        _currentWeaponInfo = weaponInfo;
        var createdObject = Object.Instantiate(_currentWeaponInfo.Prefab, Vector3.zero, transform.rotation, transform);

        if (_currentWeaponInfo.RightHanded) { Destroy(_currentRightWeapon); _currentRightWeapon = createdObject; }
        if (_currentWeaponInfo.LeftHanded) { Destroy(_currentLeftWeapon); _currentLeftWeapon = createdObject; }

        HandleArms(createdObject);
    }

    private void HandleArms(GameObject createdWeapon)
    {
        if (createdWeapon.TryGetComponentInObject(out LeftArmPosition leftPosition)) _leftArm.SetWeaponPosition(createdWeapon.transform, leftPosition);
        if (createdWeapon.TryGetComponentInObject(out RightArmPosition rightPosition)) _rightArm.SetWeaponPosition(createdWeapon.transform, rightPosition);
    }
}
