using Ruinum.Utils;
using UnityEngine;

public class AudioEvents : EventHandler
{
    private Transform _transform;

    public AudioEvents(PlayerAnimatorController controller, WeaponInventory inventory) : base(controller, inventory)
    {
        _transform = controller.transform;

        controller.SubscribeOnTimelineEvent("PlayAudio", PlayAudio);
        controller.SubscribeOnTimelineEvent("PlaySubAudio", PlaySubAudio);
    }

    protected override void WeaponMainChange() { }
    protected override void WeaponSubChange() { }

    private void PlayAudio()
    {
        AudioUtils.PlayAudio(_mainWeaponInfo.Audio, _transform.position);
    }

    private void PlaySubAudio()
    {
        AudioUtils.PlayAudio(_subWeaponInfo.Audio, _transform.position);
    }
}