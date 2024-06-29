using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RescueCountText : MonoBehaviour
{

    //�e�L�X�g�̐錾
    [SerializeField] TextMeshProUGUI RCount;
    [SerializeField] TextMeshProUGUI RSuccessCount;

    public GameObject RsuccessCount;
    public GameObject Rcount;


    public RescueCount RCounter;
    RescueNPC Rcounter1 = new RescueNPC(); 
    int Cnt;   //���ۂɎg�����
    int Cnt2;  //�e�X�g�p

    int number10;
    int number1;

    private void Awake()
    {
        Cnt = PlayerPrefs.GetInt("RescueCount");
        Debug.Log("���񂿂���:" + Cnt);
        //Cnt = RCounter.RescueNum;
        //RCount.SetText(Cnt + "");
    }
    void Start()
    {
        RsuccessCount.SetActive(false);
        //�e�L�X�g�̕\��
        RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        //Debug.Log(Cnt);
        Cnt = PlayerPrefs.GetInt("RescueCount");
        //Cnt = RCounter.RescueNum;
        RCount.SetText("<sprite="+Cnt + ">");
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
            RCount.SetText("<sprite=" + Cnt + ">");

            //text.text = Cnt.ToString();
        }

        //�~�������l����5�ȏ�Ȃ�΂ɕ\��
        else if(Cnt < 10)
        {
            Rcount.SetActive(false);
            RsuccessCount.SetActive(true);
            RCount.color = new Color(0.0f, 1.0f, 0.085f, 1.0f);
            RCount.SetText("<sprite=" + Cnt + ">");
            //LRCount.SetText("");
            //RInve.SetText("Success!!");
        }
        else
        {
            number10 = Cnt / 10 % 10;
            number1 = Cnt % 10;
            Rcount.SetActive(false);
            RsuccessCount.SetActive(true);
            RCount.color = new Color(0.0f, 1.0f, 0.085f, 1.0f);
            RCount.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">");
        }
    }

}
