using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds, sfxSounds;

    public AudioSource musicSource, sfxSource;
    
    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loops;
        }
    }

    private void Start()
    {
        PlayMusic("BGM");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found");
            return;
        }
        
        sfxSource.PlayOneShot(s.clip);
    }

    public void MusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void SfxVolume(float value)
    {
        sfxSource.volume = value;
    }
}