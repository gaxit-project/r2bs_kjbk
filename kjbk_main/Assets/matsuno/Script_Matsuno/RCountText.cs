using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RCountText : MonoBehaviour
{

    //�e�L�X�g�̐錾
    [SerializeField] TextMeshProUGUI RCount;
    [SerializeField] TextMeshProUGUI RInve;
    [SerializeField] TextMeshProUGUI RText;

    public RescueCount_verMatsuno RCounter;
    int Cnt;   //���ۂɎg�����
    int Cnt2;  //�e�X�g�p

    // Start is called before the first frame update
    void Start()
    {
        //�e�L�X�g�̕\��
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
        //�e�X�g�p
        //if((Input.GetKeyDown(KeyCode.I)))
        //{
        //    Cnt2++;
        //}
        Cnt = RCounter.getNum();
        //�����~�������l����5�����Ȃ�Ԃ��\��
        if (Cnt<5)
        {
            RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            RCount.SetText(Cnt + "");
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
