using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleUI : MonoBehaviour
{
    public float LimitTime1 = 5.0f; //タイマーの設定時間1
    public float LimitTime2 = 8.0f; //タイマーの設定時間2
    public GameObject CircleProgress; //円タイプのプログレスバー
    public string ScoreFlag = "Best"; // スコア用フラグ
    private int ColorFlag; //色変更用フラグ
    private Image ImgCircle; //CircleProgressのImage取得用
    private float PassedTime; //経過時間
    private GameObject Rescue; //Rescue参照用
    public RescueNPC resNPC; //RescueNPC参照用

    //スコア用：各状態で何回救出したか
    public static int ResNumBest;
    public static int ResNumNormal;
    public static int ResNumBad;
    public static int ResNum;

    // Start is called before the first frame update
    void Start()
    {
        //CircleProgressのImageコンポーネント取得
        ImgCircle = CircleProgress.GetComponent<Image>();

        //タイマースタート
        ColorFlag = 1;

        ResNum = 0;   //救助者数
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

    //重傷者カウント
    public void SevereCount()
    {
        ResNum++;
        if (ScoreFlag == "Best")
        {
            PlayerPrefs.SetInt("ResCntBest", ResNum);
        }
        else if (ScoreFlag == "Normal")
        {
            PlayerPrefs.SetInt("ResCntNormal", ResNum);
        }
        else if (ScoreFlag == "Bad")
        {
            PlayerPrefs.SetInt("ResCntBad", ResNum);
        }

        ResNum = 0;
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

        if (resNPC.IsItInGoal() && !resNPC.IsItRescued() && resNPC.Severe == true)
        {
            SevereCount();
        }
    }
}
