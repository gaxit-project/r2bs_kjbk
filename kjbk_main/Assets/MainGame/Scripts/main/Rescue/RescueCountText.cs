using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RescueCountText : MonoBehaviour
{

    //�e�L�X�g�̐錾
    [SerializeField] TextMeshProUGUI RCount;
    [SerializeField] TextMeshProUGUI RInve;
    [SerializeField] TextMeshProUGUI RText;
    [SerializeField] Text LRCount;

    public RescueCount_verMatsuno RCounter;
    RescueNPC Rcounter1 = new RescueNPC(); 
    int Cnt;   //���ۂɎg�����
    int Cnt2;  //�e�X�g�p

    private void Awake()
    {
        Cnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("���񂿂���:" + Cnt);
        //Cnt = RCounter.RescueNum;
        //RCount.SetText(Cnt + "");
        LRCount.text = Cnt.ToString();
    }
    void Start()
    {
        //�e�L�X�g�̕\��
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
        //�e�X�g�p
        // if ((Input.GetKeyDown(KeyCode.I)))
        // {
        //     Cnt2++;
        // }

        Cnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("--------------------RCT50:" + Rcounter1.getNum());
        //�����~�������l����5�����Ȃ�Ԃ��\��
        if (Cnt < 5)
        {
            RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            RCount.SetText(Cnt + "");
            LRCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            LRCount.text = Cnt.ToString();

            //text.text = Cnt.ToString();
        }

        //�~�������l����5�ȏ�Ȃ�΂ɕ\��
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
