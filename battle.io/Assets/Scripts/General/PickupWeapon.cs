using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private WeaponInfo _weaponInfo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<WeaponInventory>(out var weaponInventory)) return;
        weaponInventory.EquipWeapon(_weaponInfo);
    }
}
