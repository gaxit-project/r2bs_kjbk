using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioSetting : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider seSlider;
    public AudioSource bgmAudioSource;
    public AudioSource seAudioSource;

    void Start()
    {
        // PlayerPrefsから保存された値を取得してスライダーに適用
        float savedBGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        float savedSEVolume = PlayerPrefs.GetFloat("SEVolume", 1.0f);
        
        bgmSlider.value = savedBGMVolume;
        seSlider.value = savedSEVolume;
        
        // スライダーの値をAudioSourceに適用
        bgmAudioSource.volume = savedBGMVolume;
        seAudioSource.volume = savedSEVolume;

        // スライダーの値が変更されたときのリスナーを追加
        bgmSlider.onValueChanged.AddListener(delegate { OnBGMVolumeChange(); });
        seSlider.onValueChanged.AddListener(delegate { OnSEVolumeChange(); });
    }

    // BGMの値が変更されたときの処理
    public void OnBGMVolumeChange()
    {
        float newBGMVolume = bgmSlider.value;
        bgmAudioSource.volume = newBGMVolume; // AudioSourceに適用
        PlayerPrefs.SetFloat("BGMVolume", newBGMVolume); // 保存
        PlayerPrefs.Save();
    }

    // SEの値が変更されたときの処理
    public void OnSEVolumeChange()
    {
        float newSEVolume = seSlider.value;
        seAudioSource.volume = newSEVolume; // AudioSourceに適用
        PlayerPrefs.SetFloat("SEVolume", newSEVolume); // 保存
        PlayerPrefs.Save();
    }
}
