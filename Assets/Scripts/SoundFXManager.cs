using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    public float EffectVolume;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            EffectVolume = 0.5f;
        }
    }

    public void SetEffectVolume(float volume)
    {
        EffectVolume = volume;
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
