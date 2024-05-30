using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class sound : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider BGMslider;
    [SerializeField] Slider SESlider;

    private void start()
    {
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMslider.value = bgmVolume;

        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = seVolume;
    }
}