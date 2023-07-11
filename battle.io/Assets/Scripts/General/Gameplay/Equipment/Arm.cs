using UnityEngine;

public class Arm : MonoBehaviour
{
    private Quaternion _startRotation;
    private Transform _currentWeapon;
    private void Start()
    {
        _startRotation = transform.rotation;
    }

    public void SetWeaponPosition(Transform weapon, ArmPosition armPosition)
    {
        _currentWeapon = weapon;

        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);

        weapon.parent = transform;
        weapon.localPosition = Vector3.zero;
        weapon.localPosition -= new Vector3(0f, armPosition.transform.localPosition.y, 0f);
        weapon.localRotation = transform.localRotation;
        
        transform.localRotation = _startRotation;
    }

    public void DestroyWeapon()
    {
        if (_currentWeapon) Destroy(_currentWeapon.gameObject);
    }
}