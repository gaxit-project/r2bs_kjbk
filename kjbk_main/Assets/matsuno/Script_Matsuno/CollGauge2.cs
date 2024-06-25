using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollGauge2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CGauge;
    [HideInInspector] public float Counter = 0;            //時間計測
    int Collapse = 100;            //倒壊ゲージ
    float Span = 3.5f;                 //Span秒に一回倒壊ゲージを1%減らす

    int number100 = 1;
    int number10 = 0;
    int number1 = 0;
    int persent = 11;

    // Start is called before the first frame update
    void Start()
    {
        CGauge.SetText("<sprite="+ number100 +">"+"<sprite=" + number10 + ">" + "<sprite=" + number1 + ">"+"<sprite="+ persent +">");
    }

    // Update is called once per frame
    void Update()
    {
        // 倒壊ゲージカウント
        Counter += Time.deltaTime;   //秒数カウント
        if (Counter >= Span)          //倒壊ゲージが1%減の秒数
        {
            Collapse--;                //倒壊ゲージ-1%
            Counter = 0;             //秒数カウントリセット
            number10 = Collapse / 10 % 10;
            number1 = Collapse % 10;
        }

        if(Collapse == 100)
        {
            
        }
        else
        {
            // 倒壊ゲージの表示
            CGauge.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">" + "<sprite=" + persent + ">");
        }

       
    }
}
