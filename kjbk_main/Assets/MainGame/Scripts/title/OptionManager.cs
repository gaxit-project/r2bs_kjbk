using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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

    private void Start()
    {
        OnChangedBGMVolume();
        OnChangedSEVolume();
    }

    public void OnChangedBGMVolume()
    {
        Audio.GetInstance().BGMVolume = bgmSlider.value;
        //bgmValue.text = string.Format("{0:0.00}", bgmSlider.value);

    }
    public void OnChangedSEVolume()
    {
        Audio.GetInstance().SEVolume = seSlider.value;
        //seValue.text = string.Format("{0:0.00}", seSlider.value);
    }

    public void Back()
    {
        TitleUI.SetActive(true);
        OptionUI.SetActive(false);
    }

    public void Quit()
    {
        Scene.GetInstance().EndGame();
    }
}
