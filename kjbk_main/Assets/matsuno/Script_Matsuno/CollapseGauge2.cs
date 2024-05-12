using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollapseGauge2 : MonoBehaviour
{
    float CountTime = 0;            //時間計測
    float Collapse = 100;            //倒壊ゲージ
    float Span = 1;                 //Span秒に一回倒壊ゲージを1%減らす
    public Radio Demoscript;        //Radio.csから関数もって来るやつ
    public CollapseDesign2 Design;  //CollapseDesign2.csから関数もって来るやつ
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
            if (Collapse == 80)
            {
               // Suna.SunaONOFF();
                Demoscript.EightGauge();
                Design.EightHouse();
            }

            else if (Collapse == 60)
            {
                Demoscript.SixGauge();
                Design.SixHouse();
            }

            else if (Collapse == 40)
            {
                Demoscript.FourGauge();
                Design.FourHouse();
            }

            else if (Collapse == 20)
            {
                Demoscript.TwoGauge();
                Design.TwoHouse();
            }

            else if (Collapse == 10)
            {
                Demoscript.OneGauge();
                Design.OneHouse();
            }
            else if (Collapse <= 0)
            {
                //Over.GameOver();   
            }
        }


        // 倒壊ゲージの表示
        GetComponent<Text>().text = Collapse.ToString("0％");
    }

    /////////////////////////////////////////////////////

}