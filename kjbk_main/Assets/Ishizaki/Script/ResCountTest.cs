using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResCountTest : MonoBehaviour
{
    public int RescueMaxNum;   //最大救助者
    public static int RescueNum = 0;
    public static int ResNumBest = 0;   //Best救助者数
    public static int ResNumNormal = 0;   //Best救助者数
    public static int ResNumBad = 0;   //Best救助者数
    public bool RescueAll = false;   //最大救助者数を満たしたときのフラグ
    public RCountText countText;
    public Radio ARadio;
    //public CircleUI Circle;

    void Start()
    {
        PlayerPrefs.SetInt("ResCntBest", ResNumBest);
        PlayerPrefs.SetInt("ResCntNormal", ResNumNormal);
        PlayerPrefs.SetInt("ResCntBad", ResNumBad);
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
    }

    public void Count()   //現在救助者数のカウント
    {
        Debug.Log("count");
        RescueNum++;
        Debug.Log(RescueNum);
        PlayerPrefs.SetInt("ResCnt", RescueNum); //セーブ
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
