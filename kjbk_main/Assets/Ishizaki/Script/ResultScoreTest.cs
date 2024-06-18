using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScoreTest : MonoBehaviour
{
    public GameObject ResultCanvas;
    private int score;
    private int hantei = SampleManager.hantei;
    private Text textScore;
    private GameObject Clear;
    private GameObject Failed;
    private GameObject FireRoof;

    int RescueCnt;
    void Start()
    {
        textScore = GameObject.Find("Score").GetComponent<Text>();
        Clear = GameObject.Find("Clear");
        Failed = GameObject.Find("Failed");
        FireRoof = GameObject.Find("FireRoof");
        Failed.SetActive(false);
        Clear.SetActive(false);
        FireRoof.SetActive(false);
        score = 1;
        RescueCnt = PlayerPrefs.GetInt("RescueCount");
        score = RescueCnt * 100;
        textScore.text = "Score:" + score.ToString();
        Result(PlayerPrefs.GetString("Result"));
    }

    public void Result(string str)
    {
        if (str == "CLEAR")
        {
            Clear.SetActive(true);
        }
        else if (str == "GAMEOVER")
        {
            Failed.SetActive(true);
            FireRoof.SetActive(true);
        }
    }
    public void ToTitle()
    {
        Scene.Instance.Title();
    }
    public void Quit()
    {
        Scene.Instance.EndGame();
    }
}
