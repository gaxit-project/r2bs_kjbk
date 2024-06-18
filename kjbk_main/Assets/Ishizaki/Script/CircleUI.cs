using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float LimitTime1 = 5.0f; //タイマーの設定時間1
    public float LimitTime2 = 8.0f; //タイマーの設定時間2
    public GameObject CircleProgress; //円タイプのプログレスバー
    public static string ScoreFlag = "Best"; // スコア用フラグ
    private int ColorFlag; //色変更用フラグ
    private Image ImgCircle; //CircleProgressのImage取得用
    private float PassedTime; //経過時間

    // Start is called before the first frame update
    void Start()
    {
        //CircleProgressのImageコンポーネント取得
        ImgCircle = CircleProgress.GetComponent<Image>();

        //タイマースタート
        ColorFlag = 1;
    }

    //塗りつぶし
    private void Paint(float LimitTime)
    {
        //経過時間から塗りつぶし量を計算
        PassedTime += Time.deltaTime;
        float amount = PassedTime / LimitTime;

        //塗りつぶし量を代入する
        ImgCircle.fillAmount = 1 - amount;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("スコアは" + ScoreFlag);

        if (ColorFlag != 0)
        {
            if (ColorFlag == 1)
            {
                Paint(LimitTime1);
                if (LimitTime1 < PassedTime)
                {
                    ColorFlag = 2;
                    ImgCircle.color = new Color32(233, 6, 4, 255);
                    PassedTime = 0f;
                    ScoreFlag = "Normal";
                }
            }
            else if (ColorFlag == 2)
            {
                Paint(LimitTime2);
                if (LimitTime2 < PassedTime)
                {
                    ColorFlag = 0;
                    ScoreFlag = "Bad";
                }
            }
        }
    }
}
