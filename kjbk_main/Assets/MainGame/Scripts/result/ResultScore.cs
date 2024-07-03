using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    float countscore = 0;
    float displaytime = 0;
    float injureddisplay = 1.5f;
    float timedisplay = 2.0f;
    float hpdisplay = 2.5f;
    float totaldisplay = 3.5f;
    float display = 0.1f;
    public GameObject ResultCanvas;
    private int score;
    private int hantei = SampleManager.hantei;
    private Text textScore;
    public  Text textPeople;
    public Text textTime;
    public Text textHP;
    public Text textTotal;
    private GameObject Clear;
    private GameObject Failed;
    private GameObject FireRoof;

    int Best;
    int Normal;
    int Bad;
    int score2 = 0;
    int people;
    int total;
    float a;
    int hp;
    int timepoint;
    int hppoint;
    int failedpeople;
    int failedtime;
    int failedhp;
    bool clearflag;

    int RescueCnt;
    void Start()
    {
        a = CollGauge.Collapse;
        hp = LIFE.HitPoint;
        //textPeople = GameObject.Find("People").GetComponent<Text>();
        //textTime = GameObject.Find("Time").GetComponent<Text>();
        //textHP = GameObject.Find("HP").GetComponent<Text>();
        //textTotal = GameObject.Find("Total").GetComponent<Text>();
        Clear = GameObject.Find("Clear");
        Failed = GameObject.Find("Failed");
        FireRoof = GameObject.Find("FireRoof");
        Failed.SetActive(false);
        Clear.SetActive(false);
        FireRoof.SetActive(false);
        score = 0;
        RescueCnt = PlayerPrefs.GetInt("RescueCount");
        ScoreCount();
        score2 = RescueCnt * 100;
        people = score + score2;
        timepoint = (int)a * 5;
        hppoint = hp * 1000;
        failedpeople = people / 10;
        failedtime = timepoint / 10;
        failedhp = hppoint / 10;

        Result(PlayerPrefs.GetString("Result"));
    }
    private void Update()
    {
        
        countscore += Time.deltaTime;
        if(countscore >= injureddisplay)
        {
            if (clearflag == false)
            {
                textPeople.text = "People:" + failedpeople.ToString();
            }
            else if (clearflag == true)
            {
                textPeople.text = "People:" + people.ToString();
            }
        }
        if (countscore >= timedisplay)
        {
            if (clearflag == false)
            {
                textTime.text = "Time:" + failedtime.ToString();
            }
            else if (clearflag == true)
            {
                textTime.text = "Time:" + timepoint.ToString();
            }
        }
        if (countscore >= hpdisplay)
        {
            if (clearflag == false) 
            {
                textHP.text = "HP:" + failedhp.ToString();
            }
            else if (clearflag == true)
            {
                textHP.text = "HP:" + hppoint.ToString();
            }
        }
        if (countscore >= totaldisplay)
        {
            if (clearflag == false)
            {
                total = (int)(failedpeople + failedtime + failedhp);
            }
            else if(clearflag == true)
            {
                total = (int)(people + timepoint + hppoint);
            }
            textTotal.text = "Total:" + total.ToString();
        }
    }
    public void Result(string str)
    {
        if (str == "CLEAR")
        {
            clearflag = true;
            Clear.SetActive(true);
        }
        else if (str == "GAMEOVER")
        {
            clearflag = false;
            Failed.SetActive(true);
            FireRoof.SetActive(true);
        }
    }

    public void ScoreCount()
    {
        Best = PlayerPrefs.GetInt("ResCntBest");
        Normal = PlayerPrefs.GetInt("ResCntNormal");
        Bad = PlayerPrefs.GetInt("ResCntBad");

        score = Best * 200 + Normal * 100 + Bad * 50;

        //デバッグ
        Debug.Log(Best + "," + Normal + "," + Bad + " Score:" + score);
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
