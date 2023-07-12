using UnityEngine;

public class AudioController
{
    private Transform _transform;
    private WeaponInfo _currentWeaponInfo;

    public AudioController(PlayerAnimatorController controller, WeaponInventory inventory)
    {
        _transform = controller.transform;
        inventory.OnWeaponChange += OnWeaponChange;

        controller.SubscribeOnTimelineEvent("PlayAudio", PlayAudio);
    }

    private void OnWeaponChange(WeaponInfo currentWeapon, GameObject weapon)
    {
        _currentWeaponInfo = currentWeapon;
    }

    private void PlayAudio()
    {
        var audioSource = AudioUtils.PlayAudio(_currentWeaponInfo.Audio, _transform.position);
        Object.Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}