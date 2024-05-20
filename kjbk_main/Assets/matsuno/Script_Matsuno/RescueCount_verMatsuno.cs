using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RescueCount_verMatsuno : MonoBehaviour
{
    public int RescueMaxNum;   //最大救助者
    public int RescueNum = 0;   //現在救助者数
    public bool RescueAll = false;   //最大救助者数を満たしたときのフラグ
    public RCountText countText;
    public Radio ARadio;


    // Start is called before the first frame update
    void Start()
    {
        RescueNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //テスト用
         if ((Input.GetKeyDown(KeyCode.I)))
         {
            //RescueNum++;
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
