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
        if (weaponInfo.HandType == WeaponHandType.Both) EquipBoth(weaponInfo);
    }

    private void EquipBoth(WeaponInfo weaponInfo)
    {
        var createdWeapon = CreateWeapon(weaponInfo);

        _currentRightWeaponInfo = weaponInfo;
        _currentLeftWeaponInfo = weaponInfo;
        
        Destroy(_currentLeftWeapon);
        _currentLeftWeapon = createdWeapon;
        
        Destroy(_currentRightWeapon);
        _currentRightWeapon = createdWeapon;

        if (weaponInfo.MainHandType == WeaponMainHandType.Right)
        {
            if (!createdWeapon.TryGetComponentInObject(out RightArmPosition rightPosition)) return;
            _rightArm.SetWeaponPosition(createdWeapon.transform, rightPosition);
            OnWeaponChange?.Invoke(weaponInfo, createdWeapon);
        }

        if (weaponInfo.MainHandType == WeaponMainHandType.Left)
        {
            if (!createdWeapon.TryGetComponentInObject(out LeftArmPosition leftArmPosition)) return;
            _leftArm.SetWeaponPosition(createdWeapon.transform, leftArmPosition);
            OnWeaponChange?.Invoke(weaponInfo, createdWeapon);
        }
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

    public void Unarm(WeaponHandType handType)
    {
        switch (handType)
        {
            case WeaponHandType.None:
                break;
            case WeaponHandType.Left:
                _leftArm.DestroyWeapon();
                _currentLeftWeaponInfo = null;
                break;
            case WeaponHandType.Right:
                _rightArm.DestroyWeapon();
                _currentRightWeaponInfo = null;
                break;
            case WeaponHandType.Both:
                _rightArm.DestroyWeapon();
                _leftArm.DestroyWeapon();
                _currentLeftWeapon = null;
                _currentRightWeapon = null;
                break;
            default:
                break;
        }
    }

    public void UnarmAll()
    {
        Unarm(WeaponHandType.Both);
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