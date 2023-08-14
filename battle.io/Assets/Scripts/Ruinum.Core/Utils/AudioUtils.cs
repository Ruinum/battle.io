using UnityEngine;

namespace Ruinum.Utils
{
    public static class AudioUtils
    {
        public static AudioSource PlayAudio(AudioConfig config, Vector3 position)
        {
            AudioSource audioSource = new GameObject(config.Clip.ToString(), typeof(AudioSource)).GetComponent<AudioSource>();
            position.z = -11;
            audioSource.transform.position = position;

            audioSource.clip = config.Clip;
            audioSource.outputAudioMixerGroup = config.MixerGroup;
            audioSource.priority = config.Priority;
            audioSource.volume = config.Volume;
            audioSource.loop = config.Loop;
            audioSource.pitch = config.Pitch;
            audioSource.panStereo = config.StereoPan;
            audioSource.spatialBlend = config.SpatialBlend;
            audioSource.reverbZoneMix = config.ReverbZoneMix;

            audioSource.Play();

            if (!config.Loop) Object.Destroy(audioSource.gameObject, config.Clip.length);

            return audioSource;
        }
    }
}