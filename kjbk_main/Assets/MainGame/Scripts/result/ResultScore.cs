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
    float ItemDisplay = 4.5f;
    float ItemDisplay2 = 5f;
    float faileddisplay = 5.5f;
    float faileddisplay2 = 6f;
    float totaldisplay = 7f;
    float rankdisplay = 8f;
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
    public Text textItem;
    public Text textItem2;
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
    int ItemPoint;
    int ItemPoint2;
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
    public AudioClip numSE;
    public AudioClip SA_RankSE;
    public AudioClip BC_RankSE;
    public AudioClip D_RankSE;
    public AudioClip ClearBGM;
    public AudioClip OverBGM;
    public AudioClip Piramidroool;
    public AudioClip kira;

    private bool hasPlayedPeopleSE = false;
    private bool hasPlayedTimeSE = false;
    private bool hasPlayedHPSE = false;
    private bool hasPlayedItemSE = false;
    private bool hasPlayedFailureSE = false;
    private bool hasPlayedTotalSE = false;
    private bool hasPlayedRankSE = false;
    private Audioreslt Audioresult;
    bool RSFlag = false;
    public float moveSpeed = 0.1f; 
    private Vector3 targetPosition;

    public Image cooldown;

    public RectTransform piramidbow;
    public float fillSpeed = 0.1f; // fillAmount の変更速度
    private float targetFillAmount = 0f; // 目標の fillAmount
    bool doraroll=true;

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
        TotalImage.SetActive(false);
        ClearImage.SetActive(false);
        Failed.SetActive(false);
        Clear.SetActive(false);
        FireRoof.SetActive(false);
        score = 0;
        RescueCnt = PlayerPrefs.GetInt("RescueCount");
        ScoreCount();
        score2 = RescueCnt * 60;
        people = score + score2;
        people2 = people;
        
        timepoint = (int)a * 1;
        timepoint2 = timepoint;
        hppoint = hp * 10;
        hppoint2 = hppoint;

        ItemPoint = ItemTake.ResultItemCount * 150;
        ItemPoint2 = ItemPoint;

        Result(PlayerPrefs.GetString("Result"));
        Audioresult = FindObjectOfType<Audioreslt>();
        targetPosition = piramidbow.position;
        cooldown.fillAmount=0;
    }
    private void Update()
    {
        if(clearflag == true)
        {
            ClearImage.SetActive(true);
            FalseImage.SetActive(false);
           // Audioresult.PlayBGM(ClearBGM);  // BGM_List[0]を再生
        }
        else
        {
            FalseImage.SetActive(true);
            ClearImage.SetActive(false);
            //Audioresult.PlayBGM(OverBGM);  // BGM_List[0]を再生
        }
        total = (int)(people + timepoint + hppoint + ItemPoint);
        countscore += Time.deltaTime;
        
        if(countscore >= injureddisplay && !hasPlayedPeopleSE)
        {
            //PeopleImage.SetActive(true);
            textPeople.text = people.ToString();
            Audioresult.PlaySE(numSE);
            textPeople2.text = "People\n" + people2.ToString();
            hasPlayedPeopleSE=true;
        }
        if (countscore >= injureddisplay2)
        {
            //textTotal.text = "0+" + people.ToString();
        }
        if (countscore >= timedisplay && !hasPlayedTimeSE)
        {
            //TimeImage.SetActive(true);
            textTime.text = timepoint.ToString();
            Audioresult.PlaySE(numSE);
            textPeople2.text = "";
            textTime2.text = "Time\n" + timepoint2.ToString();
            hasPlayedTimeSE=true;
        }
        if (countscore >= timedisplay2)
        {
            //textTotal.text = people.ToString() + "+" + timepoint.ToString();
        }
        if (countscore >= hpdisplay && !hasPlayedHPSE)
        {
            //HPImage.SetActive(true);
            textHP.text = hppoint.ToString();
            Audioresult.PlaySE(numSE);
            textTime2.text = "";
            textHP2.text = "HP\n" + hppoint2.ToString();
            hasPlayedHPSE=true;
        }
        if (countscore >= hpdisplay2)
        {
            pt = (int)(people + timepoint);
            //textTotal.text = pt.ToString() + "+" + hppoint.ToString();
        }


        if (countscore >= ItemDisplay && !hasPlayedItemSE)
        {
            //HPImage.SetActive(true);
            textItem.text = ItemPoint.ToString();
            Audioresult.PlaySE(numSE);
            textHP2.text = "";
            textItem2.text = "Item\n" + ItemPoint2.ToString();
            hasPlayedItemSE=true;
        }
        if (countscore >= ItemDisplay2)
        {
            pt = (int)(people + timepoint + hppoint);
            //textTotal.text = pt.ToString() + "+" + ItemPoint.ToString();
        }


        if (countscore >= faileddisplay && !hasPlayedFailureSE)
        {
            //FailedImage.SetActive(true);
            textItem2.text = "";
            if (clearflag == false)
            {
                textFailure.text = "-500";
                Audioresult.PlaySE(numSE);
                textFailure2.text = "Failed\n-500";
                hasPlayedFailureSE=true;
            }
            else
            {
                textFailure.text = "0";
                Audioresult.PlaySE(numSE);
                hasPlayedFailureSE=true;
            }
            
        }
        if (countscore >= faileddisplay2)
        {
            
            if (clearflag == false)
            {
                //textTotal.text = total.ToString() + "-500";
            }
        }

        if (countscore >= totaldisplay && !hasPlayedTotalSE)
        {
            //TotalImage.SetActive(true);
            textFailure2.text = "";
            Pramidroll();
            if (clearflag == false)
            {
                failedtotal = (int)(people + timepoint + hppoint + ItemPoint) - 500;
                
                if (failedtotal > 0)
                {
                    textTotal.text = failedtotal.ToString();
                    Audioresult.PlaySE(numSE);
                    hasPlayedTotalSE=true;
                }
                else
                {
                    textTotal.text = "0";
                }
            }
            else if(clearflag == true)
            {
                total = (int)(people + timepoint + hppoint + ItemPoint);
                textTotal.text = total.ToString();
                Audioresult.PlaySE(numSE);
                hasPlayedTotalSE=true;
            }
        }
        if(cooldown.fillAmount == targetFillAmount){
        if (countscore >= rankdisplay && !hasPlayedRankSE)
        {
            
            if (clearflag == true)
            {
                Audioresult.PlaySE(kira);
                {
                    if (total >= 2500)
                    {
                        RankText.SetText("<sprite=0>");
                        Audioresult.PlaySE(SA_RankSE);
                        hasPlayedRankSE=true;
                    }
                    else if (total >= 2000)
                    {
                        RankText.SetText("<sprite=1>");
                        Audioresult.PlaySE(SA_RankSE);
                        hasPlayedRankSE=true;
                    }
                    else if (total >= 1500)
                    {
                        RankText.SetText("<sprite=2>");
                        Audioresult.PlaySE(BC_RankSE);
                        hasPlayedRankSE=true;
                    }
                    else
                    {
                        RankText.SetText("<sprite=3>");
                        Audioresult.PlaySE(BC_RankSE);
                        hasPlayedRankSE=true;
                    }
                }
            }
            if(clearflag == false)
            {
                RankText.SetText("<sprite=4>");
                Audioresult.PlaySE(D_RankSE);
                hasPlayedRankSE=true;
            }
        }
        }else{
        cooldown.fillAmount = Mathf.MoveTowards(cooldown.fillAmount, targetFillAmount, fillSpeed * Time.deltaTime);
        if(doraroll){
        Audioresult.PlaySE(Piramidroool);
        doraroll=false;
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

        score = Best * 220 + Normal * 220 + Bad * 220;

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

    public void Pramidroll(){

        /*if(!clearflag){
            StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+280, piramidbow.position.z)));
        }else*/ if(total>=3000){
            //StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+280, piramidbow.position.z)));
            //cooldown.fillAmount=1f;
            targetFillAmount = 1f;
        }else if(total>=2750){
            //StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+250, piramidbow.position.z)));
            //cooldown.fillAmount=0.93f;
            targetFillAmount = 0.93f;
        }else if(total>=2500){
            //StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+230, piramidbow.position.z)));
            //cooldown.fillAmount=0.8f;
            targetFillAmount = 0.8f;
        }else if(total>=2250){
            //StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+200, piramidbow.position.z)));
            //cooldown.fillAmount=0.7f;
            targetFillAmount = 0.7f;
        }else if(total>=2000){
           //StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+165, piramidbow.position.z)));
           //cooldown.fillAmount=0.6f;
           targetFillAmount = 0.6f;
        }else if(total>=1750){
            //StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+135, piramidbow.position.z)));
            //cooldown.fillAmount=0.5f;
            targetFillAmount = 0.5f;
        }else if(total>=1500){
            //StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+100, piramidbow.position.z)));
            //cooldown.fillAmount=0.4f;
            targetFillAmount = 0.4f;
        }else if(total>=1000){
            //StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+75, piramidbow.position.z)));
            //cooldown.fillAmount=0.3f;
            targetFillAmount = 0.3f;
        }else if(total>=0){
            //StartCoroutine(MoveToPosition(new Vector3(piramidbow.position.x, piramidbow.position.y+50, piramidbow.position.z)));
            //cooldown.fillAmount=0.2f;
            targetFillAmount = 0.2f;
        }
        /*piramidbow.position = Vector3.Lerp(piramidbow.position, targetPosition, Time.deltaTime * moveSpeed);*/
    }
    IEnumerator MoveToPosition(Vector3 targetPosition)
{
    while (Vector3.Distance(piramidbow.position, targetPosition) > 0.01f)
    {
        // 目標位置に向かって徐々に移動
        piramidbow.position = Vector3.Lerp(piramidbow.position, targetPosition, Time.deltaTime * moveSpeed * 0.5f);
        yield return null; // 次のフレームまで待つ
    }

    // 最後に目標位置に正確に設定
    piramidbow.position = targetPosition;
}

    IEnumerator RandomScore()
    {
        int i,cnt=0;
        for(i=0;i<10;i++)
        {
            cnt = Random.Range(0, 100);
            //cnt���e�L�X�g�ɓ����
            textTotal.text = cnt.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        RSFlag = true;
        coutineFlag = false;
    }
}