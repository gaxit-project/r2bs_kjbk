using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleManager : MonoBehaviour
{
    public GameObject TitleUI;
    public GameObject OptionUI;

    void Start()
    {
        OptionUI.SetActive(false);
    }
    public void StartGame()
    {
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
    }

    public void Back()
    {
        OptionUI.SetActive(false);
        TitleUI.SetActive(true);
    }
}
