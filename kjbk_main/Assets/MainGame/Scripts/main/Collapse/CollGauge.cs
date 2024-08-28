using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollGauge : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CGauge;

    float CountTime = 0;            //時間計測
    public static int Collapse = 100;            //倒壊ゲージ
    float Span = 4.5f;                 //Span秒に一回倒壊ゲージを1%減らす
    public CollDesign Design;  //CollapseDesign2.csから関数もって来るやつ
    private bool STOP = false;      //無線のフラグ
    int a = 5;                      //無線の種類分け

    int number100 = 1;
    int number10 = 0;
    int number1 = 0;
    int persent = 10;

    
    public BlockPOP POP;  //障害物を設置するコードから変数を持ってくる

    public Radio_ver4 Radio4;  //無線から変数を持ってくる

    // Use this for initialization
    void Start()
    {
        CGauge.SetText("<sprite=" + number100 + ">" + "<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        Collapse = 100;
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
                CollapseRadioON();
            }

            else if (Collapse == 60)
            {
                Design.SixHouse();
                CollapseRadioON();
            }

            else if (Collapse == 40)
            {
                Design.FourHouse();
                POP.Generate40 = true;
                CollapseRadioON();
            }

            else if (Collapse == 20)
            {
                Design.TwoHouse();
                POP.Generate20 = true;
                CollapseRadioON();
            }

            else if (Collapse == 10)
            {
                Design.OneHouse();
                POP.Generate10 = true;
                CollapseRadioON();
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
    void CollapseRadioON()
    {
        Radio4.CollapseDialogue();
    }
    /////////////////////////////////////////////////////

}