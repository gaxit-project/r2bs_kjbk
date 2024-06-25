using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollGauge : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CGauge;

    float CountTime = 0;            //時間計測
    int Collapse = 100;            //倒壊ゲージ
    float Span = 3.5f;                 //Span秒に一回倒壊ゲージを1%減らす
    public CollRadio Demoscript;        //Radio.csから関数もって来るやつ
    public CollDesign Design;  //CollapseDesign2.csから関数もって来るやつ
    public Sunaarashi_ON_OFF Suna;  //砂嵐をもってくる
    private bool STOP = false;      //無線のフラグ
    int a = 5;                      //無線の種類分け

    int number100 = 1;
    int number10 = 0;
    int number1 = 0;
    int persent = 11;


    public BlockPOP POP;
    //public SceneChange Over;        //SceneChange.csからゲームオーバーを持ってくる
    //public Sunaarashi_ON_OFF Suna;  //Sunaarashiから砂嵐を持ってくる

    public Radio_ver3 Radio3;

    [HideInInspector] public bool Radio80;
    [HideInInspector] public bool Radio60;
    [HideInInspector] public bool Radio40;
    [HideInInspector] public bool Radio20;
    [HideInInspector] public bool Radio10;
    // Use this for initialization
    void Start()
    {
        CGauge.SetText("<sprite=" + number100 + ">" + "<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");

    }

    // Update is called once per frame
    void Update()
    {
        // 倒壊ゲージカウント
        CountTime += Time.deltaTime;   //秒数カウント
        if (CountTime >= Span)          //倒壊ゲージが1%減の秒数
        {
            Collapse--;                //倒壊ゲージ-1%
            CountTime = 0;             //秒数カウントリセット
            number10 = Collapse / 10 % 10;
            number1 = Collapse % 10;


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //倒壊ゲージの無線通知＋倒壊ゲージのデザイン表示
            if (Collapse == 80)
            {
                Design.EightHouse();             //家のデザインを出す
                Suna.SunaONOFF();                //砂嵐を表示
                Invoke(nameof(STOPFlagON), 2f);  //フラグを砂嵐後にONにする
                Radio80 = true;
            }

            else if (Collapse == 60)
            {
                Design.SixHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
                Radio60 = true;
            }

            else if (Collapse == 40)
            {
                Design.FourHouse();
                Suna.SunaONOFF();
                POP.Generate40 = true;
                Invoke(nameof(STOPFlagON), 2f);
                Radio40 = true;
            }

            else if (Collapse == 20)
            {
                Design.TwoHouse();
                Suna.SunaONOFF();
                POP.Generate20 = true;
                Invoke(nameof(STOPFlagON), 2f);
                Radio20 = true;
            }

            else if (Collapse == 10)
            {
                Design.OneHouse();
                Suna.SunaONOFF();
                POP.Generate10 = true;
                Invoke(nameof(STOPFlagON), 2f);
                Radio10 = true;
            }
            else if (Collapse <= 0)
            {
                PlayerPrefs.SetString("Result", "GAMEOVER");
                Scene.Instance.GameResult();
            }

            //特定のゲージ時に無線を出すようにする
            if (STOP)
            {
                //フラグが届いたら以下の通りに無線を実行
                Radio3.CollapseRadio = true;
                Radio3.RadioStoper();
                STOP = false;  //フラグをOFFに
            }
        }

        if(Collapse < 100)
        {
            // 倒壊ゲージの表示
            CGauge.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        }

    }


    //無線のフラグ
    void STOPFlagON()
    {
        STOP = true;
    }
    /////////////////////////////////////////////////////

}