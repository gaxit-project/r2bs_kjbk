using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionControl : MonoBehaviour
{
    public GameObject OptionCanvas;
    void Start()
    {
        OptionCanvas.SetActive(false);
    }
    public void Back()
    {
        OptionCanvas.SetActive(false);
    }
    public void ToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Scene.Instance.EndGame();
    }
}
