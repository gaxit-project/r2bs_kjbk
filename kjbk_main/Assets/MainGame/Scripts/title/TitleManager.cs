using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleManager : MonoBehaviour
{
    public GameObject TitleUI;
    public GameObject OptionUI;

    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;


    public Button OptionIcon2;

    void Start()
    {
        PlayerPrefs.SetFloat("SE", 0.06f);
        PlayerPrefs.SetFloat("BGM", 0.06f);

        Audio.GetInstance().BGMVolume = PlayerPrefs.GetFloat("BGM");
        Audio.GetInstance().SEVolume = PlayerPrefs.GetFloat("SE");

        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        seSlider.value = PlayerPrefs.GetFloat("SE");


        OptionUI.SetActive(false);
    }
    public void StartGame()
    {
        Audio.GetInstance().PlaySound(0);
        PlayerPrefs.SetInt("RescueCount", 0);
        Scene.GetInstance().GamePlay();
    }

    public void Quit()
    {
        Audio.GetInstance().PlaySound(0);
        Scene.GetInstance().EndGame();
    }

    public void Option()
    {
        Audio.GetInstance().PlaySound(0);
        OptionUI.SetActive(true);
        TitleUI.SetActive(false);
        OptionIcon2.Select();
    }

    public void Back()
    {
        Audio.GetInstance().PlaySound(0);
        OptionUI.SetActive(false);
        TitleUI.SetActive(true);
    }
}
