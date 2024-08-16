using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RescueCount : MonoBehaviour
{
    public int RescueMaxNum;   //最大救助者
    public static int RescueNum = 0;   //現在救助者数
    public static int ResNumBest = 0;   //Best救助者数
    public static int ResNumNormal = 0;   //Normal救助者数
    public static int ResNumBad = 0;   //Bad救助者数
    public bool RescueAll = false;   //最大救助者数を満たしたときのフラグ
    public RCountText countText;
    public Radio ARadio;
    public CircleUI CirUI;  //サークルUI

    //missionマップに変数を送る
    public MissionMapUI MMUI;

    void Start()
    {
        RescueNum = 0;
        PlayerPrefs.SetInt("RescueCount", RescueNum);
        PlayerPrefs.SetInt("ResCntBest", 0);
        PlayerPrefs.SetInt("ResCntNormal", 0);
        PlayerPrefs.SetInt("ResCntBad", 0);
    }

    void Update()
    {
        //テスト用
        if ((Input.GetKeyDown(KeyCode.I)))
        {
            Count();
        }
        if (RescueNum == RescueMaxNum)   //最大救助者数を満たしているかの比較
        {
            RescueAll = true;
        }
        // if(RescueAll)
        //{
        //    ARadio.AloneRadio();
        //}
        
        //test
        Debug.Log("Best = " + PlayerPrefs.GetInt("ResCntBest"));
        Debug.Log("Normal = " + PlayerPrefs.GetInt("ResCntNormal"));
        Debug.Log("Bad = " + PlayerPrefs.GetInt("ResCntBad"));
    }

    public void Count()   //現在救助者数のカウント
    {
        Debug.Log("count");
        RescueNum++;
        Debug.Log(RescueNum);
        PlayerPrefs.SetInt("RescueCount", RescueNum); //セーブ
        if (RescueNum >= 10)
        {
            MMUI.MissionUpgread("a", 10);
        }
    }

    public int getNum()   //現在救助者数の取得
    {
        Debug.Log("callNum");
        return RescueNum;
    }

    public bool getRescueAll()   //フラグの取得
    {
        return RescueAll;
    }
}