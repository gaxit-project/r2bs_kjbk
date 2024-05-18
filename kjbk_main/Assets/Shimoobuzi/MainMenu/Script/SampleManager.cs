using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SampleManager : MonoBehaviour
{
    public GameObject Option;
    public static int hantei = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if(Option.activeInHierarchy)
            {
                Option.SetActive(false);
            }
            else
            {
                Option.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            hantei = 1;
            SceneManager.LoadScene("Result");
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            hantei = 0;
            SceneManager.LoadScene("Result");
        }
    }
}
