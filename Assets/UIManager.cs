using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    public void MusicVolume()
    {
        SoundManager.Instance.MusicVolume(bgmSlider.value);
    }

    public void SfxVolume()
    {
        SoundManager.Instance.SfxVolume(sfxSlider.value);
    }
}
