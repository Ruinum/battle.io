using UnityEngine;
using Ruinum.Utils;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] private WeaponInfo _weaponInfo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponentInObject(out WeaponInventory weaponInventory)) return;
        weaponInventory.EquipWeapon(_weaponInfo);
        Destroy(gameObject);
    }
}
