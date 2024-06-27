using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescue_Counter : MonoBehaviour
{
    public int RescueMaxNum;   //最大救助者
    [SerializeField] private int RescueNum = 0;   //現在救助者数
    public bool RescueAll = false;   //最大救助者数を満たしたときのフラグ

    // Start is called before the first frame update
    void Start()
    {
        RescueNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(RescueNum == RescueMaxNum)   //最大救助者数を満たしているかの比較
        {
            RescueAll = true;
        }
        //テスト用↓
        
    }

    public void Count()   //現在救助者数のカウント
    {
        RescueNum++;
    }

    public int getNum()   //現在救助者数の取得
    {
        return RescueNum;
    }

    public bool getRescueAll()   //フラグの取得
    {
        return RescueAll;
    }
}
