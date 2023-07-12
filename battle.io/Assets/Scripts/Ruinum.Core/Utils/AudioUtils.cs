using UnityEngine;

public static class AudioUtils
{
    public static AudioSource PlayAudio(AudioConfig config, Vector3 position)
    {
        AudioSource audioSource = new GameObject(config.Clip.ToString(), typeof(AudioSource)).GetComponent<AudioSource>();
        audioSource.transform.position = position;

        audioSource.clip = config.Clip;
        audioSource.loop = config.Loop;
        audioSource.pitch = config.Pitch;
        audioSource.panStereo = config.StereoPan;
        audioSource.spatialBlend = config.SpatialBlend;
        audioSource.reverbZoneMix = config.ReverbZoneMix;

        audioSource.Play();

        return audioSource;
    }
}