using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loops;
    
    [HideInInspector]
    public AudioSource source;

    public Sound(AudioClip audio, float mVolume, float mPitch, bool loop)
    {
        clip = audio;
        volume = mVolume;
        pitch = mPitch;
        loops = loop;
    }
}

[System.Serializable]
public class SoundDictProperty
{
    public string key;
    public Sound sound;
}

[System.Serializable]
public class SoundDictionary
{
    public List<SoundDictProperty> soundDict;

    public Dictionary<string, Sound> ToDictionary()
    {
        Dictionary<string, Sound> newSound = new Dictionary<string, Sound>();

        foreach (var dictionary in soundDict)
        {
            newSound.Add(dictionary.key, dictionary.sound);
        }

        return newSound;
    }
}
