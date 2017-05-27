using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffectHandler : MonoBehaviour
{
    public string description;

    public List<AudioClip> clips;
    public bool playRandom = false;

    public float volume = 1.0f;
    public bool variableVolume = false;
    public float volumeMin = 0.9f;
    public float volumeMax = 1.0f;

    public float playDelay = 0.0f;
    public bool variableDelay = false;
    public float playDelayMin = 0.0f;
    public float playDelayMax = 0.01f;
    public bool ignoreTimeScale = false;

    public bool playOneShot = true;
    public bool playOnStart = false;

    public void Start()
    {
        if (playOnStart)
        {
            PlayEffect();
        }
    }

    public void PlayEffect()
    {
        if (playRandom && clips.Count > 1)
        {
            int random = Random.Range(0, clips.Count);
            AudioClip clip = clips[random];
            PlayClip(clip);
        }
        else
        {
            foreach (AudioClip clip in clips)
            {
                PlayClip(clip);
            }
        }
    }

    private void PlayClip(AudioClip clip)
    {
        float clipVolume;
        float clipDelay;

        if (variableVolume)
        {
            clipVolume = Random.Range(volumeMin, volumeMax);
        }
        else
        {
            clipVolume = volume;
        }

        if (variableDelay)
        {
            clipDelay = Random.Range(playDelayMin, playDelayMax);
        }
        else
        {
            clipDelay = playDelay;
        }

        if (clipDelay > 0.0f)
        {
            StartCoroutine(WaitThenPlay(clip, clipVolume, clipDelay));
        }
        else
        {
            Play(clip, clipVolume);
        }
    }

    private void Play(AudioClip clip, float clipVolume)
    {
        AudioSource source;

        if (playOneShot)
        {
            source = AudioManager.Instance.sfxSource;
            source.PlayOneShot(clip, clipVolume);
        }
        else
        {
            source = AudioManager.Instance.sharedSFXSource;
            source.clip = clip;
            source.volume = clipVolume;
            source.Play();
        }

    }



    private IEnumerator WaitThenPlay(AudioClip clip, float clipVolume, float clipDelay)
    {
        if (ignoreTimeScale)
        {
            float playTime = Time.realtimeSinceStartup + clipDelay;

            while (playTime > Time.realtimeSinceStartup)
            {
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(clipDelay);
        }

        Play(clip, clipVolume);
    }
}
