using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueCount : MonoBehaviour
{
    #region 宣言
    // 救助者数の設定とカウントに関する変数
    public int RescueMaxNum; // 最大救助者数
    public static int RescueNum = 0; // 現在の救助者数
    public static int ResNumBest = 0; // ベスト救助者数
    public static int ResNumNormal = 0; // ノーマル救助者数
    public static int ResNumBad = 0; // バッド救助者数
    public bool RescueAll = false; // 最大救助者数に達したかのフラグ

    // 他のコンポーネントへの参照
    public RCountText countText; // 救助者数表示用
    public Radio ARadio; // ラジオ機能の参照
    public CircleUI CirUI; // サークルUI

    // Missionマップに変数を送るための参照
    public MissionMapUI MMUI;
    #endregion

    #region 初期化
    // 初期設定を行うStartメソッド
    void Start()
    {
        // 救助者数の初期化
        RescueNum = 0;

        // PlayerPrefsに初期値を設定
        PlayerPrefs.SetInt("RescueCount", RescueNum);
        PlayerPrefs.SetInt("ResCntBest", 0);
        PlayerPrefs.SetInt("ResCntNormal", 0);
        PlayerPrefs.SetInt("ResCntBad", 0);
    }
    #endregion

    #region 更新
    void Update()
    {
        // 最大救助者数に達しているか確認し、フラグを設定
        if (RescueNum == RescueMaxNum)
        {
            RescueAll = true;
        }
    }
    #endregion

    #region 関数
    // 現在の救助者数をカウントするメソッド
    public void Count()
    {
        RescueNum++;
        PlayerPrefs.SetInt("RescueCount", RescueNum); // 現在の救助者数を保存

        // 救助者数が10以上の場合、ミッションをアップグレード
        if (RescueNum >= 10)
        {
            MMUI.MissionUpgread("a", 10);
        }
    }

    // 現在の救助者数を取得するメソッド
    public int getNum()
    {
        return RescueNum;
    }

    // 最大救助者数に達しているかのフラグを取得するメソッド
    public bool getRescueAll()
    {
        return RescueAll;
    }
    #endregion
}
