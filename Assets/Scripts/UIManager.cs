using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        bgmSlider.value = SoundManager.Instance.musicSource.volume;
        sfxSlider.value = SoundManager.Instance.sfxSource.volume;
    }

    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(bgmSlider.value);
    }

    public void SfxVolume()
    {
        SoundManager.Instance.SfxVolume(sfxSlider.value);
    }
}
