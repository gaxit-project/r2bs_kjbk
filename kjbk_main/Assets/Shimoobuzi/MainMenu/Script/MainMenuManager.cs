using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    public GameObject OptionCanvas;
    public GameObject MainMenuCanvas;

    void Start()
    {
        OptionCanvas.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleMain");
    }

    public void Quit()
    {   
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Option()
    {
        OptionCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

    public void Back()
    {
        OptionCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
