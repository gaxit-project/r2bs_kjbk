using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RescueCountText : MonoBehaviour
{

    //テキストの宣言
    [SerializeField] TextMeshProUGUI RCount;
    [SerializeField] TextMeshProUGUI RInve;
    [SerializeField] TextMeshProUGUI RText;
    [SerializeField] Text LRCount;

    public RescueCount_verMatsuno RCounter;
    RescueNPC Rcounter1 = new RescueNPC(); 
    int Cnt;   //実際に使うやつ
    int Cnt2;  //テスト用

    private void Awake()
    {
        Cnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("こんちくわ:" + Cnt);
        //Cnt = RCounter.RescueNum;
        //RCount.SetText(Cnt + "");
        LRCount.text = Cnt.ToString();
    }
    void Start()
    {
        //テキストの表示
        RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        LRCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        RInve.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        //Debug.Log(Cnt);
        Cnt = PlayerPrefs.GetInt("RescueCount");
        //Cnt = RCounter.RescueNum;
        RCount.SetText(Cnt + "");
        LRCount.text = Cnt.ToString();
        RText.SetText("Objective : Help 5 people :");
        RInve.SetText("");

    }

    void Update()
    {
        //テスト用
        // if ((Input.GetKeyDown(KeyCode.I)))
        // {
        //     Cnt2++;
        // }

        Cnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("--------------------RCT50:" + Rcounter1.getNum());
        //もし救助した人数が5未満なら赤く表示
        if (Cnt < 5)
        {
            RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            RCount.SetText(Cnt + "");
            LRCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            LRCount.text = Cnt.ToString();

            //text.text = Cnt.ToString();
        }

        //救助した人数が5以上なら緑に表示
        else
        {
            RCount.color = new Color(0.0f, 1.0f, 0.085f, 1.0f);
            LRCount.color = new Color(0.0f, 1.0f, 0.085f, 1.0f);
            RInve.color = new Color(1.0f, 0.92f, 0.005f, 1.0f);
            RCount.SetText(Cnt + "");
            LRCount.text = Cnt.ToString();
            RInve.SetText("Success!!");
        }
    }

}
