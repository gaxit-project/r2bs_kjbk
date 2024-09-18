using UnityEngine;
using UnityEngine.UI;

public class Presente : MonoBehaviour
{
    #region 宣言: 変数
    // スライダーの参照
    [SerializeField] Slider bgmSlider; // BGMボリューム調整スライダー
    [SerializeField] Slider seSlider; // SEボリューム調整スライダー

    // テキストの参照
    [SerializeField] Text bgmValue; // BGMボリューム表示テキスト
    [SerializeField] Text seValue; // SEボリューム表示テキスト

    // UIオブジェクトの参照
    public GameObject PauseUI; // ポーズ画面のUI
    public GameObject SoundOptionUI; // サウンド設定のUI
    public GameObject TitleUI; // タイトル画面のUI
    public GameObject BackToTheTitle; // タイトル画面に戻るボタン
    public GameObject SoundSetting; // サウンド設定ボタン

    // ボタンの参照
    public Button a; // サウンド設定ボタン
    public Button TitleIcon; // タイトルアイコンボタン
    public Button TitlePIcon; // タイトル戻るアイコンボタン
    public Button SoundPIcon; // サウンド設定戻るアイコンボタン

    // ステータスフラグ
    public bool ConfigSta; // 設定中フラグ
    public bool TitleSta; // タイトル中フラグ

    // ゲームロジックの参照
    public GoalJudgement Goal; // ゴール判定スクリプト
    public Pause PauseScript; // ポーズスクリプト

    // ゲーム進行カウンター
    int Rcnt = 0; // 救助カウント
    #endregion

    #region 初期化: Startメソッド
    void Start()
    {
        // 音量スライダーの初期設定
        OnChangedBGMVolume();
        OnChangedSEVolume();

        // スライダーの値を保存から読み込む
        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        seSlider.value = PlayerPrefs.GetFloat("SE");

        // Audioインスタンスの音量設定
        var audio = Audio.GetInstance();
        audio.BGMVolume = PlayerPrefs.GetFloat("BGM");
        audio.SEVolume = PlayerPrefs.GetFloat("SE");
        audio.RoopSEVolume = PlayerPrefs.GetFloat("SE");
        audio.WALKVolume = PlayerPrefs.GetFloat("SE");
        audio.FireVolume1 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume2 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume3 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume4 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume5 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume6 = PlayerPrefs.GetFloat("SE");
        audio.FireVolume7 = PlayerPrefs.GetFloat("SE");

        // ステータスフラグの初期化
        ConfigSta = false;
        TitleSta = false;
    }
    #endregion

    #region 音量変更: スライダーの値変更
    public void OnChangedBGMVolume()
    {
        // BGM音量の保存と適用
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
        var audio = Audio.GetInstance();
        audio.BGMVolume = bgmSlider.value;
    }

    public void OnChangedSEVolume()
    {
        // SE音量の保存と適用
        PlayerPrefs.SetFloat("SE", seSlider.value);
        var audio = Audio.GetInstance();
        audio.SEVolume = seSlider.value;
        audio.RoopSEVolume = seSlider.value;
        audio.WALKVolume = seSlider.value;
        audio.FireVolume1 = seSlider.value;
        audio.FireVolume2 = seSlider.value;
        audio.FireVolume3 = seSlider.value;
        audio.FireVolume4 = seSlider.value;
        audio.FireVolume5 = seSlider.value;
        audio.FireVolume6 = seSlider.value;
        audio.FireVolume7 = seSlider.value;
    }
    #endregion

    #region ボタン操作: UI操作メソッド
    public void Quit()
    {
        Audio.GetInstance().PlaySound(16);
        // ゲーム終了処理
        Scene.GetInstance().EndGame();
    }

    public void Option()
    {
        Audio.GetInstance().PlaySound(16);
        // サウンド設定UIを表示し、ポーズUIを非表示にする
        SoundOptionUI.SetActive(true);
        PauseUI.SetActive(false);
        a.Select();
        ConfigSta = true;
    }

    public void PauseBack()
    {
        Audio.GetInstance().PlaySound(16);
        // ポーズ画面に戻る処理
        SoundOptionUI.SetActive(false);
        TitleUI.SetActive(false);
        BackToTheTitle.SetActive(true);
        SoundSetting.SetActive(true);
        PauseScript.PauseCon();
    }

    public void Title()
    {
        Audio.GetInstance().PlaySound(16);
        // タイトル画面に遷移する処理
        Scene.GetInstance().Title();
    }

    public void GoTitleUI()
    {
        Audio.GetInstance().PlaySound(16);
        // タイトルUIを表示し、ポーズUIを非表示にする
        TitleUI.SetActive(true);
        PauseUI.SetActive(false);
        TitleIcon.Select();
        TitleSta = true;
    }

    public void TitleBack()
    {
        Audio.GetInstance().PlaySound(16);
        // ポーズUIに戻る処理
        TitleUI.SetActive(false);
        PauseUI.SetActive(true);
        TitlePIcon.Select();
        TitleSta = false;
    }

    public void SoundBack()
    {
        Audio.GetInstance().PlaySound(16);
        // サウンド設定UIに戻る処理
        SoundOptionUI.SetActive(false);
        PauseUI.SetActive(true);
        SoundPIcon.Select();
        ConfigSta = false;
    }

    public void Escape()
    {
        Audio.GetInstance().PlaySound(16);
        // 救助人数に応じてゲーム結果を決定する処理
        Rcnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("K");

        Time.timeScale = 1;
        if (Rcnt >= 5)
        {
            PlayerPrefs.SetString("Result", "CLEAR");
            Scene.Instance.GameResult();
        }
        else
        {
            PlayerPrefs.SetString("Result", "GAMEOVER");
            Scene.Instance.GameResult();
        }
    }
    #endregion
}
