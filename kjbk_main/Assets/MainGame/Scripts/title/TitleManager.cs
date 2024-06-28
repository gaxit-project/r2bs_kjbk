using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleManager : MonoBehaviour
{
    public GameObject TitleUI;
    public GameObject OptionUI;


    public Button OptionIcon2;

    void Start()
    {
        OptionUI.SetActive(false);
    }
    public void StartGame()
    {
        
        PlayerPrefs.SetInt("RescueCount", 0);
        Scene.GetInstance().GamePlay();
    }

    public void Quit()
    {
        Scene.GetInstance().EndGame();
    }

    public void Option()
    {
        OptionUI.SetActive(true);
        TitleUI.SetActive(false);
        OptionIcon2.Select();
    }

    public void Back()
    {
        OptionUI.SetActive(false);
        TitleUI.SetActive(true);
    }
}
