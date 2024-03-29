using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollapseGauge2 : MonoBehaviour
{
    float CountTime = 0;     //時間計測
    float Collapse = 81;     //倒壊ゲージ
    float Span = 1;          //Span秒に一回倒壊ゲージを1%減らす
    public Radio Demoscript; //Radio.csから関数もって来るやつ

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

            ////////////////////////////////////////////////////
            //倒壊ゲージの無線通知
            if (Collapse == 80)
            {
                Demoscript.EightGauge();
            }

            else if (Collapse == 60)
            {
                Demoscript.SixGauge();
            }

            else if (Collapse == 40)
            {
                Demoscript.FourGauge();
            }

            else if (Collapse == 20)
            {
                Demoscript.TwoGauge();
            }

            else if (Collapse == 10)
            {
                Demoscript.OneGauge();
            }
        }


        // 倒壊ゲージの表示
        GetComponent<Text>().text = Collapse.ToString("0％");
    }

    /////////////////////////////////////////////////////

}