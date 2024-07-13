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
    float injureddisplay2 = 2f;
    float timedisplay = 2.5f;
    float timedisplay2 = 3f;
    float hpdisplay = 3.5f;
    float hpdisplay2 = 4f;
    float faileddisplay = 4.5f;
    float faileddisplay2 = 5f;
    float totaldisplay = 6f;
    float display = 0.1f;
    public GameObject ResultCanvas;
    private int score;
    private int hantei = SampleManager.hantei;
    private Text textScore;
    public  Text textPeople;
    public Text textPeople2;
    public Text textTime;
    public Text textTime2;
    public Text textHP;
    public Text textHP2;
    public Text textFailure;
    public Text textFailure2;
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
    int people2;
    int timepoint2;
    int hppoint2;
    int failedpeople;
    int failedtime;
    int failedhp;
    bool clearflag;
    int failedtotal;
    int pt;

    int RescueCnt;
    int random;
    void Start()
    {
        Time.timeScale = 1.0f;
        a = CollGauge.Collapse;
        hp = LIFE.HitPoint;
        //textPeople = GameObject.Find("People").GetComponent<Text>();
        //textTime = GameObject.Find("Time").GetComponent<Text>();
        //textHP = GameObject.Find("HP").GetComponent<Text>();
        //textTotal = GameObject.Find("Total").GetComponent<Text>();
        textPeople2 = GameObject.Find("People2").GetComponent<Text>();
        textTime2 = GameObject.Find("Time2").GetComponent<Text>();
        textHP2 = GameObject.Find("HP2").GetComponent<Text>();
        textFailure = GameObject.Find("Failure").GetComponent<Text>();
        textFailure2 = GameObject.Find("Failure2").GetComponent<Text>();
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
        people2 = people;
        
        timepoint = (int)a * 3;
        timepoint2 = timepoint;
        hppoint = hp * 100;
        hppoint2 = hppoint;
        
        Result(PlayerPrefs.GetString("Result"));
    }
    private void Update()
    {
        total = (int)(people + timepoint + hppoint);
        countscore += Time.deltaTime;
        displaytime += Time.deltaTime;
        if(countscore >= injureddisplay)
        {
                textPeople.text = "People:" + people.ToString();
            textPeople2.text = "People\n" + people2.ToString();
            
        }
        if (countscore >= injureddisplay2)
        {
            textTotal.text = "0+" + people.ToString();
        }
        if (countscore >= timedisplay)
        {
                textTime.text = "Time:" + timepoint.ToString();
            textPeople2.text = "";
            textTime2.text = "Time\n" + timepoint2.ToString();
            

        }
        if (countscore >= timedisplay2)
        {
            textTotal.text = people.ToString() + "+" + timepoint.ToString();
        }
        if (countscore >= hpdisplay)
        {
                textHP.text = "HP:" + hppoint.ToString();
            textTime2.text = "";
            textHP2.text = "HP\n" + hppoint2.ToString();
            
        }
        if (countscore >= hpdisplay2)
        {
            pt = (int)(people + timepoint);
            textTotal.text = pt.ToString() + "+" + hppoint.ToString();
        }
        if (countscore >= faileddisplay)
        {
            textHP2.text = "";
            if (clearflag == false)
            {
                textFailure.text = "Failed:-500";
                textFailure2.text = "Failed\n-500";
                
            }
            else
            {
                textFailure.text = "Failed:0";
            }
            
        }
        if (countscore >= faileddisplay2)
        {
            
            if (clearflag == false)
            {
                textTotal.text = total.ToString() + "-500";
            }
        }

        if (countscore >= totaldisplay)
        {
            textFailure2.text = "";
            if (clearflag == false)
            {
                failedtotal = (int)(people + timepoint + hppoint) - 500;
                
                if (failedtotal > 0)
                {
                    textTotal.text = failedtotal.ToString();
                }
                else
                {
                    textTotal.text = total.ToString();
                }
            }
            else if(clearflag == true)
            {
                total = (int)(people + timepoint + hppoint);
                textTotal.text = total.ToString();
            }
            
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
