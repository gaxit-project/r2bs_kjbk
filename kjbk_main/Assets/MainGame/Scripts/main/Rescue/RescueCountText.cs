using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RescueCountText : MonoBehaviour
{

    //テキストの宣言
    [SerializeField] TextMeshProUGUI RCount;
    [SerializeField] TextMeshProUGUI RSuccessCount;

    public GameObject RsuccessCount;
    public GameObject Rcount;


    public RescueCount RCounter;
    RescueNPC Rcounter1 = new RescueNPC(); 
    int Cnt;   //実際に使うやつ
    int Cnt2;  //テスト用

    int number10;
    int number1;

    private void Awake()
    {
        Cnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("こんちくわ:" + Cnt);
        //Cnt = RCounter.RescueNum;
        //RCount.SetText(Cnt + "");
    }
    void Start()
    {
        RsuccessCount.SetActive(false);
        //テキストの表示
        RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        //Debug.Log(Cnt);
        Cnt = PlayerPrefs.GetInt("RescueCount");
        //Cnt = RCounter.RescueNum;
        RCount.SetText("<sprite="+Cnt + ">");
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
        if (Cnt >= 10)
        {
            number10 = Cnt / 10 % 10;
            number1 = Cnt % 10;
            RCount.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">");
            Rcount.SetActive(false);
            RsuccessCount.SetActive(true);
        }
        //もし救助した人数が5未満なら赤く表示
        else
        {
            RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            RCount.SetText("<sprite=" + Cnt + ">");

            //text.text = Cnt.ToString();
        }
        
    }

}
