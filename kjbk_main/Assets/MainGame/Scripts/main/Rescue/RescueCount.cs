using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RescueCount : MonoBehaviour
{
    public int RescueMaxNum;   //�ő�~����
    public static int RescueNum = 0;   //���݋~���Ґ�
    public static int ResNumBest = 0;   //Best�~���Ґ�
    public static int ResNumNormal = 0;   //Normal�~���Ґ�
    public static int ResNumBad = 0;   //Bad�~���Ґ�
    public bool RescueAll = false;   //�ő�~���Ґ��𖞂������Ƃ��̃t���O
    public RCountText countText;
    public Radio ARadio;
    public CircleUI CirUI;  //�T�[�N��UI


    void Start()
    {
        RescueNum = 0;
        PlayerPrefs.SetInt("RescueCount", RescueNum);
        PlayerPrefs.SetInt("ResCntBest", 0);
        PlayerPrefs.SetInt("ResCntNormal", 0);
        PlayerPrefs.SetInt("ResCntBad", 0);
    }

    void Update()
    {
        //�e�X�g�p
        if ((Input.GetKeyDown(KeyCode.I)))
        {
            Count();
        }
        if (RescueNum == RescueMaxNum)   //�ő�~���Ґ��𖞂����Ă��邩�̔�r
        {
            RescueAll = true;
        }
        // if(RescueAll)
        //{
        //    ARadio.AloneRadio();
        //}

        //test
        Debug.Log("Best = " + PlayerPrefs.GetInt("ResCntBest"));
        Debug.Log("Normal = " + PlayerPrefs.GetInt("ResCntNormal"));
        Debug.Log("Bad = " + PlayerPrefs.GetInt("ResCntBad"));
    }

    public void Count()   //���݋~���Ґ��̃J�E���g
    {
        Debug.Log("count");
        RescueNum++;
        Debug.Log(RescueNum);
        PlayerPrefs.SetInt("RescueCount", RescueNum); //�Z�[�u
    }

    //�d���҃J�E���g�p
    public void SevereCount()
    {
        if (CirUI.ScoreFlag == "Best")
        {
            ResNumBest++;
            PlayerPrefs.SetInt("ResCntBest", ResNumBest);
            Debug.Log("Best++");
        }
        else if (CirUI.ScoreFlag == "Normal")
        {
            ResNumNormal++;
            PlayerPrefs.SetInt("ResCntNormal", ResNumNormal);
            Debug.Log("Normal++");
        }
        else
        {
            ResNumBad++;
            PlayerPrefs.SetInt("ResCntBad", ResNumBad);
            Debug.Log("Bad++");
        }
    }


    public int getNum()   //���݋~���Ґ��̎擾
    {
        Debug.Log("callNum");
        return RescueNum;
    }

    public bool getRescueAll()   //�t���O�̎擾
    {
        return RescueAll;
    }
}