using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RCountText : MonoBehaviour
{

    //テキストの宣言
    [SerializeField] TextMeshProUGUI RCount;
    [SerializeField] TextMeshProUGUI RInve;
    [SerializeField] TextMeshProUGUI RText;

    public RescueCount_verMatsuno RCounter;
    int Cnt;   //実際に使うやつ
    int Cnt2;  //テスト用

    // Start is called before the first frame update
    void Start()
    {
        //テキストの表示
        RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        RInve.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        Cnt = RCounter.getNum();
        RCount.SetText(Cnt2 +"");
        RText.SetText("Objective : Help 5 people :");
        RInve.SetText("");
    }

    // Update is called once per frame
    public void Update()
    {
        //テスト用
        //if((Input.GetKeyDown(KeyCode.I)))
        //{
        //    Cnt2++;
        //}
        Cnt = RCounter.getNum();
        //もし救助した人数が5未満なら赤く表示
        if (Cnt<5)
        {
            RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            RCount.SetText(Cnt + "");
        }

        //救助した人数が5以上なら緑に表示
        else
        {
            RCount.color = new Color(0.0f, 1.0f, 0.085f, 1.0f);
            RInve.color = new Color(1.0f, 0.92f, 0.005f, 1.0f);
            RCount.SetText(Cnt + "");
            RInve.SetText("Success!!");
        }
    }


}
