using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    public SoundDictionary MusicSound, SFXSound;
    private Dictionary<string, Sound> musicDictionary, sfxDictionary;

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
    }

    private void Start()
    {
        musicDictionary = MusicSound.ToDictionary();
        sfxDictionary = SFXSound.ToDictionary();
        PlayMusic("BGM");
    }

    public void PlayMusic(string name)
    {
        if (!musicDictionary.TryGetValue(name, out var sound)) return;
        musicSource.clip = sound.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        if (!sfxDictionary.TryGetValue(name, out var sound)) return;
        sfxSource.PlayOneShot(sound.clip);
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