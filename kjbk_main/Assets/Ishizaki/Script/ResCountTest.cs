using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResCountTest : MonoBehaviour
{
    public int RescueMaxNum;   //�ő�~����
    public static int RescueNum = 0;
    public static int ResNumBest = 0;   //Best�~���Ґ�
    public static int ResNumNormal = 0;   //Best�~���Ґ�
    public static int ResNumBad = 0;   //Best�~���Ґ�
    public bool RescueAll = false;   //�ő�~���Ґ��𖞂������Ƃ��̃t���O
    public RCountText countText;
    public Radio ARadio;
    //public CircleUI Circle;

    void Start()
    {
        PlayerPrefs.SetInt("ResCntBest", ResNumBest);
        PlayerPrefs.SetInt("ResCntNormal", ResNumNormal);
        PlayerPrefs.SetInt("ResCntBad", ResNumBad);
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
    }

    public void Count()   //���݋~���Ґ��̃J�E���g
    {
        Debug.Log("count");
        RescueNum++;
        Debug.Log(RescueNum);
        PlayerPrefs.SetInt("ResCnt", RescueNum); //�Z�[�u
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
