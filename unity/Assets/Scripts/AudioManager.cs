using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource sfxSource;
    public AudioSource sharedSFXSource;
    public AudioSource musicSource;    

    public float volumeIncrement = 0.1f;

    public float startingVolume = 0.8f;
    public float maxVolume = 1.0f;
    public float minVolume = 0.0f;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        if (this == null) { return; }

        AudioListener.volume = startingVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Raise Volume"))
        {
            SetVolume(AudioListener.volume + volumeIncrement);
        }

        if (Input.GetButtonDown("Lower Volume"))
        {
            SetVolume(AudioListener.volume - volumeIncrement);
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }

    public void StartMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic(bool pause)
    {
        if (pause)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.UnPause();
        }
    }

    public void DuckMusic()
    {

    }

    public void UnduckMusic()
    {

    }
    
}
