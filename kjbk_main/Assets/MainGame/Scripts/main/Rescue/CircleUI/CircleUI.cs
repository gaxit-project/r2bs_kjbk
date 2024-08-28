using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleUI : MonoBehaviour
{
    #region 宣言部
    // タイマーの設定時間1
    public float LimitTime1 = 5.0f;
    // タイマーの設定時間2
    public float LimitTime2 = 8.0f;
    // 円タイプのプログレスバー
    public GameObject CircleProgress;
    // スコア用フラグ
    public string ScoreFlag = "Best";

    // 色変更用フラグ
    private int ColorFlag;
    // CircleProgressのImage取得用
    private Image ImgCircle;
    // 経過時間
    private float PassedTime;
    // Rescue参照用
    private GameObject Rescue;
    // RescueNPC参照用
    public RescueNPC resNPC;

    // スコア用：各状態で何回救出したか
    public static int ResNumBest;
    public static int ResNumNormal;
    public static int ResNumBad;
    public static int ResNum;
    #endregion

    #region 初期化
    void Start()
    {
        // CircleProgressのImageコンポーネント取得
        ImgCircle = CircleProgress.GetComponent<Image>();

        // タイマースタート
        ColorFlag = 1;

        // 救助者数を初期化
        ResNum = 0;
    }
    #endregion

    #region 塗りつぶし処理
    // 塗りつぶし処理
    private void Paint(float LimitTime)
    {
        // 経過時間から塗りつぶし量を計算
        PassedTime += Time.deltaTime;
        float amount = PassedTime / LimitTime;

        // 塗りつぶし量を代入する
        ImgCircle.fillAmount = 1 - amount;
    }
    #endregion

    #region 重傷者カウント処理
    // 重傷者カウント
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

        // カウントをリセット
        ResNum = 0;
    }
    #endregion

    #region 更新処理
    void Update()
    {
        if (ColorFlag != 0)
        {
            if (ColorFlag == 1)
            {
                // タイマー1の塗りつぶし処理
                Paint(LimitTime1);
                if (LimitTime1 < PassedTime)
                {
                    ColorFlag = 2;
                    ImgCircle.color = new Color32(233, 6, 4, 255); // 色変更
                    PassedTime = 0f;
                    ScoreFlag = "Normal";
                }
            }
            else if (ColorFlag == 2)
            {
                // タイマー2の塗りつぶし処理
                Paint(LimitTime2);
                if (LimitTime2 < PassedTime)
                {
                    ColorFlag = 0;
                    ScoreFlag = "Bad";
                }
            }
        }

        // 重傷者がゴールに到達して未救出かつ重傷ならカウント
        if (resNPC.IsItInGoal() && !resNPC.IsItRescued() && resNPC.Severe == true)
        {
            SevereCount();
        }
    }
    #endregion
}
