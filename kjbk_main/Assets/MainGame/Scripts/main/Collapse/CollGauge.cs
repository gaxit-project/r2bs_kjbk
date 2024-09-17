using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollGauge : MonoBehaviour
{
    #region フィールドの定義
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 倒壊ゲージの設定と他のスクリプトの参照
    ///
    [SerializeField] TextMeshProUGUI CGauge;

    float CountTime = 0;            // 時間計測用の変数
    public static int Collapse = 100; // 倒壊ゲージの初期値
    float Span = 4.5f;               // Span秒ごとに倒壊ゲージを1%減少させる
    public CollDesign Design;        // CollDesignスクリプトの参照
    private bool STOP = false;       // 無線のフラグ
    int a = 5;                       // 無線の種類分け用変数

    int number100 = 1;
    int number10 = 0;
    int number1 = 0;
    int persent = 10;

    public BlockPOP POP;             // 障害物を設置するコードから変数を持ってくる

    public Radio_ver4 Radio4;        // 無線スクリプトから変数を持ってくる

    public static bool TimeStop = false;
    #endregion

    #region 初期化メソッド
    // Use this for initialization
    void Start()
    {
        // 倒壊ゲージの初期表示を設定
        CGauge.SetText("<sprite=" + number100 + ">" + "<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        Collapse = 100; // 倒壊ゲージを100にリセット
    }
    #endregion

    #region 更新メソッド
    // Update is called once per frame
    void Update()
    {
        if(!TimeStop)
        {
            // 時間のカウント
            CountTime += Time.deltaTime;
        }
        

        // 倒壊ゲージの更新
        if (CountTime >= Span)
        {
            Collapse--;            // 倒壊ゲージを1%減少
            CountTime = 0;          // 秒数カウントをリセット
            number10 = Collapse / 10 % 10;
            number1 = Collapse % 10;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // 倒壊ゲージが特定の値に達した際の処理
            if (Collapse == 80)
            {
                Design.EightHouse();             // 80%時の家のデザインを表示
                CollapseRadioON();               // 無線をオンにする
            }
            else if (Collapse == 60)
            {
                Design.SixHouse();               // 60%時の家のデザインを表示
                CollapseRadioON();               // 無線をオンにする
            }
            else if (Collapse == 40)
            {
                Design.FourHouse();              // 40%時の家のデザインを表示
                POP.Generate40 = true;           // 障害物生成フラグをオン
                CollapseRadioON();               // 無線をオンにする
            }
            else if (Collapse == 20)
            {
                Design.TwoHouse();               // 20%時の家のデザインを表示
                POP.Generate20 = true;           // 障害物生成フラグをオン
                CollapseRadioON();               // 無線をオンにする
            }
            else if (Collapse == 10)
            {
                Design.OneHouse();               // 10%時の家のデザインを表示
                POP.Generate10 = true;           // 障害物生成フラグをオン
                CollapseRadioON();               // 無線をオンにする
            }
            else if (Collapse <= 0)
            {
                PlayerPrefs.SetString("Result", "GAMEOVER"); // ゲームオーバー時の処理
                Scene.Instance.GameResult();                 // ゲーム結果画面へ遷移
            }

            // 無線フラグが立っている場合の処理
            if (STOP)
            {
                STOP = false;  // フラグをリセット
            }
        }

        // 倒壊ゲージが100未満の時の表示更新
        if (Collapse < 100)
        {
            CGauge.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        }
    }
    #endregion

    #region 無線関連のメソッド
    // 無線のフラグをオンにする
    void STOPFlagON()
    {
        STOP = true;
    }

    // 倒壊無線をオンにする
    void CollapseRadioON()
    {
        Radio4.CollapseDialogue();
    }
    #endregion
}
