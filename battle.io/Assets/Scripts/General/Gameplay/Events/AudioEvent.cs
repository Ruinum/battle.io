using Ruinum.Utils;
using UnityEngine;

public class AudioEvent : EventHandler
{
    private Transform _transform;

    public AudioEvent(PlayerAnimatorController controller, WeaponInventory inventory) : base(controller, inventory)
    {
        _transform = controller.transform;

        controller.SubscribeOnTimelineEvent("PlayAudio", PlayAudio);
    }

    protected override void WeaponChange() { }

    private void PlayAudio()
    {
        var audioSource = AudioUtils.PlayAudio(_weaponInfo.Audio, _transform.position);
        Object.Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}