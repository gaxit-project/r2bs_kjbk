using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RCountText : MonoBehaviour
{

    //�e�L�X�g�̐錾
    public Text text;
    string str;
    [SerializeField] TextMeshProUGUI RCount;
    [SerializeField] TextMeshProUGUI RInve;
    [SerializeField] TextMeshProUGUI RText;
    [SerializeField] Text LRCount;

    //public RescueCount_verMatsuno RCounter;
    public RescueCount_verMatsuno RCounter;
    RescueNPC Rcounter1 = new RescueNPC();
    int Cnt = 1;   //���ۂɎg�����
    int Cnt2;  //�e�X�g�p

    // Start is called before the first frame update

    private void Awake()
    {
        Cnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("���񂿂���:"+Cnt);
        //Cnt = RCounter.RescueNum;
        //RCount.SetText(Cnt + "");
        LRCount.text = Cnt.ToString();
    }
    void Start()
    {
        //�e�L�X�g�̕\��
        RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        RInve.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        //Debug.Log(Cnt);
        Cnt = PlayerPrefs.GetInt("RescueCount");
        //Cnt = RCounter.RescueNum;
        RCount.SetText(Cnt + "");
        LRCount.text = Cnt.ToString();
        RText.SetText("Objective : Help 5 people :");
        RInve.SetText("");

        //text.text = Cnt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //�e�X�g�p
        // if ((Input.GetKeyDown(KeyCode.I)))
        // {
        //     Cnt2++;
        // }

        Cnt = Rcounter1.getNum();
        Debug.Log("--------------------RCT50:"+Rcounter1.getNum());
        //�����~�������l����5�����Ȃ�Ԃ��\��
        if (Cnt < 5)
        {
            RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            RCount.SetText(Cnt + "");

            //text.text = Cnt.ToString();
        }

        //�~�������l����5�ȏ�Ȃ�΂ɕ\��
        else
        {
            RCount.color = new Color(0.0f, 1.0f, 0.085f, 1.0f);
            RInve.color = new Color(1.0f, 0.92f, 0.005f, 1.0f);
            RCount.SetText(Cnt + "");
            RInve.SetText("Success!!");
        }
    }


}
