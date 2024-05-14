using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollapseGauge2 : MonoBehaviour
{
    float CountTime = 0;            //時間計測
    float Collapse = 82;            //倒壊ゲージ
    float Span = 1;                 //Span秒に一回倒壊ゲージを1%減らす
    public Radio Demoscript;        //Radio.csから関数もって来るやつ
    public CollapseDesign2 Design;  //CollapseDesign2.csから関数もって来るやつ
    public Sunaarashi_ON_OFF Suna;  //砂嵐をもってくる
    private bool STOP = false;      //無線のフラグ
    int a = 5;                      //無線の種類分け
    //public SceneChange Over;        //SceneChange.csからゲームオーバーを持ってくる
    //public Sunaarashi_ON_OFF Suna;  //Sunaarashiから砂嵐を持ってくる

    // Use this for initialization
    void Start()
    {
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

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //倒壊ゲージの無線通知＋倒壊ゲージのデザイン表示
            if(Collapse == 80)
            {
                Design.EightHouse();             //家のデザインを出す
                Suna.SunaONOFF();                //砂嵐を表示
                Invoke(nameof(STOPFlagON), 2f);  //フラグを砂嵐後にONにする
            }

            else if (Collapse == 60)
            {
                Design.SixHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
            }

            else if (Collapse == 40)
            {
                Design.FourHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
            }

            else if (Collapse == 20)
            {
                Design.TwoHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
            }

            else if (Collapse == 10)
            {
                Design.OneHouse();
                Suna.SunaONOFF();
                Invoke(nameof(STOPFlagON), 2f);
            }
            else if (Collapse <= 0)
            {
                //Over.GameOver();   
            }
            
            //特定のゲージ時に無線を出すようにする
            if (STOP)
            {
                //フラグが届いたら以下の通りに無線を実行
                if(a == 5)
                {
                    Demoscript.EightGauge();
                }
                else if(a == 4)
                {
                    Demoscript.SixGauge();
                }
                else if (a == 3)
                {
                    Demoscript.FourGauge();
                }
                else if (a == 2)
                {
                    Demoscript.TwoGauge();
                }
                else if (a == 1)
                {
                    Demoscript.OneGauge();
                }
                STOP = false;  //フラグをOFFに
                a--;           //次の無線に変更
            }
        }


        // 倒壊ゲージの表示
        GetComponent<Text>().text = Collapse.ToString("0％");
    }


    //無線のフラグ
    void STOPFlagON()
    {
        STOP = true;
    }
    /////////////////////////////////////////////////////

}