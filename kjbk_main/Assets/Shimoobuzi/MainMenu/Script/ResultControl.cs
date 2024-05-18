using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultControl : MonoBehaviour
{
    public GameObject ResultCanvas;
    private int score;
    private int hantei = SampleManager.hantei;
    private Text textScore;
    private GameObject Clear;
    private GameObject Failed;
    private GameObject FireRoof;
    void Start()
    {
        
        textScore=GameObject.Find("Score").GetComponent<Text>();
        Clear=GameObject.Find("Clear");
        Failed=GameObject.Find("Failed");
        FireRoof=GameObject.Find("FireRoof");
        Failed.SetActive(false);
        Clear.SetActive(false);
        FireRoof.SetActive(false);
        score = 1;
        //score = Rescue_Counter.RescueNum * 100; //吉田さんのスクリプトRescue_CounterのRescueNumを public static int 型にかえてください。
        textScore.text="Score:"+score.ToString();
        Result(hantei);
    }

    public void Result(int h)
    {
        if(h==1)
        {
            Clear.SetActive(true);
        }
        else if(h==0)
        {
            Failed.SetActive(true);
            FireRoof.SetActive(true);
        }
    }
    public void ToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {   
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
