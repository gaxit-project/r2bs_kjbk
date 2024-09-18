using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject TitleUI;
    public GameObject OptionUI;

    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Text bgmValue;
    [SerializeField] Text seValue;

    public Button TitleIcon2;

    private void Start()
    {
        OnChangedBGMVolume();
        OnChangedSEVolume();
    }

    public void OnChangedBGMVolume()
    {
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
        Audio.GetInstance().BGMVolume = bgmSlider.value;
        //bgmValue.text = string.Format("{0:0.00}", bgmSlider.value);

    }
    public void OnChangedSEVolume()
    {
        PlayerPrefs.SetFloat("SE", seSlider.value);
        Audio.GetInstance().SEVolume = seSlider.value;
        //seValue.text = string.Format("{0:0.00}", seSlider.value);
    }

    public void Back()
    {
        Audio.GetInstance().PlaySound(0);
        TitleUI.SetActive(true);
        OptionUI.SetActive(false);
        TitleIcon2.Select();
    }

    public void Quit()
    {
        Scene.GetInstance().EndGame();
    }
}
