using UnityEngine;
using Ruinum.Utils;
using System;

public class WeaponInventory : MonoBehaviour
{
    private GameObject _currentRightWeapon;
    private GameObject _currentLeftWeapon;
    private WeaponInfo _currentRightWeaponInfo;
    private WeaponInfo _currentLeftWeaponInfo;

    private RightArm _rightArm;
    private LeftArm _leftArm;

    public Action<WeaponInfo, GameObject> OnWeaponChange;

    private void Start()
    {
        _rightArm = GetComponentInChildren<RightArm>();
        _leftArm = GetComponentInChildren<LeftArm>();       
    }

    public void EquipWeapon(WeaponInfo weaponInfo)
    {
        if (weaponInfo.HandType == WeaponHandType.Left) EquipLeft(weaponInfo);
        if (weaponInfo.HandType == WeaponHandType.Right) EquipRight(weaponInfo);
    }

    private void EquipRight(WeaponInfo weaponInfo)
    {
        var createdWeapon = CreateWeapon(weaponInfo);
        if (!createdWeapon.TryGetComponentInObject(out RightArmPosition rightPosition)) return;

        Destroy(_currentRightWeapon); 
        _currentRightWeapon = createdWeapon;
        _currentRightWeaponInfo = weaponInfo;

        _rightArm.SetWeaponPosition(createdWeapon.transform, rightPosition);

        OnWeaponChange?.Invoke(_currentRightWeaponInfo, _currentRightWeapon);
    }

    private void EquipLeft(WeaponInfo weaponInfo)
    {
        var createdWeapon = CreateWeapon(weaponInfo);
        if (!createdWeapon.TryGetComponentInObject(out LeftArmPosition leftPosition)) return;

        Destroy(_currentLeftWeapon); 
        _currentLeftWeapon = createdWeapon;
        _currentLeftWeaponInfo = weaponInfo;

        _leftArm.SetWeaponPosition(createdWeapon.transform, leftPosition);
    }

    private GameObject CreateWeapon(WeaponInfo weaponInfo)
    {
        return UnityEngine.Object.Instantiate(weaponInfo.Prefab, Vector3.zero, transform.rotation, transform);
    }

    public bool TryGetRightWeapon(out WeaponInfo weaponInfo)
    {
        weaponInfo = _currentRightWeaponInfo;

        if (!_currentRightWeaponInfo || _currentRightWeaponInfo == default) return false;
        return true;
    }

    public bool TryGetLeftWeapon(out WeaponInfo weaponInfo)
    {
        weaponInfo = _currentLeftWeaponInfo;

        if (!_currentLeftWeaponInfo || _currentLeftWeaponInfo == default) return false;
        return true;
    }
}