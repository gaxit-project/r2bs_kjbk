using UnityEngine;
using UnityEngine.UI;

public class Presente : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Text bgmValue;
    [SerializeField] Text seValue;

    public GameObject PauseUI;
    public GameObject SoundOptionUI;
    public GameObject TitleUI;

    public GameObject BackToTheTitle;
    public GameObject SoundSetting;

    public Button a;
    public Button TitleIcon;
    public Button TitlePIcon;
    public Button SoundPIcon;

    public bool ConfigSta;
    public bool TitleSta;

    public GoalJudgement Goal;


    public Pause PauseScript;

    int Rcnt = 0;


    void Start()
    {
        OnChangedBGMVolume();
        OnChangedSEVolume();

        //Audio初期化
        //Audio初期化

        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        seSlider.value = PlayerPrefs.GetFloat("SE");

        Audio.GetInstance().BGMVolume = PlayerPrefs.GetFloat("BGM");
        //bgmValue.text = string.Format("{0:0.00}", PlayerPrefs.GetFloat("BGM"));

        Audio.GetInstance().SEVolume = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().RoopSEVolume = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().WALKVolume = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume1 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume2 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume3 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume4 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume5 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume6 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume7 = PlayerPrefs.GetFloat("SE");
        //seValue.text = string.Format("{0:0.00}", PlayerPrefs.GetFloat("SE"));

        Debug.Log("SE" + seSlider.value);
        Debug.Log("BGM" + bgmSlider.value);


        Debug.Log("SE" + Audio.GetInstance().SEVolume);
        Debug.Log("BGM" + Audio.GetInstance().BGMVolume);



        ConfigSta = false;
        TitleSta = false;
    }


    void Update()
    {
        //Debug.Log("ふらぐおーーーーーん"+Goal.PauseFlag);
    }

    public void OnChangedBGMVolume()
    {
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
        Audio.GetInstance().BGMVolume = PlayerPrefs.GetFloat("BGM");
        //bgmValue.text = string.Format("{0:0.00}", PlayerPrefs.GetFloat("BGM"));

    }
    public void OnChangedSEVolume()
    {
        PlayerPrefs.SetFloat("SE", seSlider.value);
        Audio.GetInstance().SEVolume = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().RoopSEVolume = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().WALKVolume = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume1 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume2 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume3 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume4 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume5 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume6 = PlayerPrefs.GetFloat("SE");
        Audio.GetInstance().FireVolume7 = PlayerPrefs.GetFloat("SE");
        //seValue.text = string.Format("{0:0.00}", PlayerPrefs.GetFloat("SE"));

    }

    public void Quit()
    {
        Scene.GetInstance().EndGame();
    }

    public void Option()
    {
        SoundOptionUI.SetActive(true);
        PauseUI.SetActive(false);
        a.Select();
        ConfigSta = true;
    }

    public void PauseBack()
    {
        SoundOptionUI.SetActive(false);
        TitleUI.SetActive(false);
        //PauseUI.SetActive(false);
        //Time.timeScale = 1.0f;
        Invoke(nameof(FlagONOFF), 5);
        BackToTheTitle.SetActive(true);
        SoundSetting.SetActive(true);
        PauseScript.PauseCon();
        Goal.PauseFlag = false;
        //PauseScript.pause_status = false;
    }
    public void Title()
    {
        Scene.GetInstance().Title();
    }
    public void GoTitleUI()
    {
        TitleUI.SetActive(true);
        PauseUI.SetActive(false);
        TitleIcon.Select();
        TitleSta = true;
    }
    public void TitleBack()
    {
        TitleUI.SetActive(false);
        PauseUI.SetActive(true);
        TitlePIcon.Select();
        TitleSta = false;
    }
    public void SoundBack()
    {
        SoundOptionUI.SetActive(false);
        PauseUI.SetActive(true);
        SoundPIcon.Select();
        ConfigSta = false;
    }
    public void Escape()
    {
        Rcnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("K");

        Time.timeScale = 1;
        //救助した人数が5人以上ならクリアへ移行
        if (Rcnt >= 5)
        {
            PlayerPrefs.SetString("Result", "CLEAR");
            Scene.Instance.GameResult();
            //Scene.Instance.GameClear();
        }

        //違うならゲームオーバーに移行
        else
        {
            PlayerPrefs.SetString("Result", "GAMEOVER");
            Scene.Instance.GameResult();
            //Scene.Instance.GameOver();
        }
    }
    public void NoEscape()
    {

    }
    public void FlagONOFF()
    {
        Goal.JudgeFlag = true;
    }
}
