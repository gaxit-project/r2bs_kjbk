using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    float rankdisplay = 7f;
    public GameObject ResultCanvas;
    private int score;
    private int hantei = SampleManager.hantei;
    private Text textScore;
    public Text textPeople;
    public Text textPeople2;
    public Text textTime;
    public Text textTime2;
    public Text textHP;
    public Text textHP2;
    public Text textFailure;
    public Text textFailure2;
    public Text textTotal;
    public Text textRank;
    private GameObject Clear;
    private GameObject Failed;
    private GameObject FireRoof;

    [SerializeField] GameObject PeopleImage;
    [SerializeField] GameObject TimeImage;
    [SerializeField] GameObject HPImage;
    [SerializeField] GameObject FailedImage;
    [SerializeField] GameObject TotalImage;
    [SerializeField] GameObject ClearImage;
    [SerializeField] GameObject FalseImage;

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
    int point;
    int i = 0;

    int RescueCnt;
    int random;
    bool coutineFlag = true;
    bool coutineFlag1 = true;
    bool coutineFlag2 = true;
    bool coutineFlag4 = true;

    [SerializeField] private TMP_Text RankText;

    bool RSFlag = false;
    void Start()
    {
        
        Time.timeScale = 1.0f;
        a = CollGauge.Collapse;
        hp = LIFE.HitPoint;
        textPeople2 = GameObject.Find("People2").GetComponent<Text>();
        textTime2 = GameObject.Find("Time2").GetComponent<Text>();
        textHP2 = GameObject.Find("HP2").GetComponent<Text>();
        textFailure = GameObject.Find("Failure").GetComponent<Text>();
        textFailure2 = GameObject.Find("Failure2").GetComponent<Text>();
        textRank = GameObject.Find("Rank").GetComponent<Text>();
        Clear = GameObject.Find("Clear");
        Failed = GameObject.Find("Failed");
        FireRoof = GameObject.Find("FireRoof");
        PeopleImage.SetActive(false);
        TimeImage.SetActive(false);
        HPImage.SetActive(false);
        FailedImage.SetActive(false);
        FalseImage.SetActive(false);
        TotalImage.SetActive(true);
        ClearImage.SetActive(false);
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
        if(clearflag == true)
        {
            ClearImage.SetActive(true);
            FalseImage.SetActive(false);
        }
        else
        {
            FalseImage.SetActive(true);
            ClearImage.SetActive(false);
        }
        total = (int)(people + timepoint + hppoint);
        countscore += Time.deltaTime;
        
        if(countscore >= injureddisplay)
        {
            PeopleImage.SetActive(true);
                textPeople.text = people.ToString();
            textPeople2.text = "People\n" + people2.ToString();
            
        }
        if (countscore >= injureddisplay2)
        {
            textTotal.text = "0+" + people.ToString();
        }
        if (countscore >= timedisplay)
        {
            TimeImage.SetActive(true);
                textTime.text = timepoint.ToString();
            textPeople2.text = "";
            textTime2.text = "Time\n" + timepoint2.ToString();
            

        }
        if (countscore >= timedisplay2)
        {
            textTotal.text = people.ToString() + "+" + timepoint.ToString();
        }
        if (countscore >= hpdisplay)
        {
            HPImage.SetActive(true);
                textHP.text = hppoint.ToString();
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
            FailedImage.SetActive(true);
            textHP2.text = "";
            if (clearflag == false)
            {
                textFailure.text = "-500";
                textFailure2.text = "Failed\n-500";
                
            }
            else
            {
                textFailure.text = "0";
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
            TotalImage.SetActive(true);
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
                    textTotal.text = "0";
                }
            }
            else if(clearflag == true)
            {
                total = (int)(people + timepoint + hppoint);
                textTotal.text = total.ToString();
            }
            
        }
        if (countscore >= rankdisplay)
        {
            if (clearflag == true)
            {

                {
                    if (total >= 2000)
                    {
                        RankText.SetText("<sprite=0>");
                    }
                    else if (total >= 1700)
                    {
                        RankText.SetText("<sprite=1>");
                    }
                    else if (total >= 1400)
                    {
                        RankText.SetText("<sprite=2>");
                    }
                    else
                    {
                        RankText.SetText("<sprite=3>");
                    }
                }
            }
            if(clearflag == false)
            {
                RankText.SetText("<sprite=4>");
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

    }

    public void ToTitle()
    {
        Scene.Instance.Title();
    }
    public void Quit()
    {
        Scene.Instance.EndGame();
    }

    public void ScoreText()
    {
        textTotal.text = i.ToString();
    }

    IEnumerator RandomScore()
    {
        int i,cnt=0;
        for(i=0;i<10;i++)
        {
            cnt = Random.Range(0, 100);
            //cntをテキストに入れる
            textTotal.text = cnt.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        RSFlag = true;
        coutineFlag = false;
    }
}