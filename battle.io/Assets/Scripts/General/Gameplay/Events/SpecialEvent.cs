using Ruinum.Utils;
using UnityEngine;

public class SpecialEvent : EventHandler
{
    private Transform _transform;

    public SpecialEvent(PlayerAnimatorController controller, WeaponInventory inventory) : base(controller, inventory)
    {
        _transform = controller.transform;

        controller.SubscribeOnTimelineEvent("OnSpecial", Special);
    }

    protected override void WeaponChange() { }
    
    private void Special()
    {
        if (_weaponInfo.Special == null) 
        { 
            Debug.LogWarning($"Weapon Info {_weaponInfo} has null value of Special field"); 
            return; 
        };

        var specialObject = Object.Instantiate(_weaponInfo.Special, _transform.position, _transform.rotation, null);
        if (!specialObject.TryGetComponent(out Special special)) { Debug.LogError($"There is no special script on {specialObject}"); }
        
        special.Initialize(_transform.gameObject.GetComponentInObject<Level>(), _weaponInfo, _transform.gameObject);
    }
}