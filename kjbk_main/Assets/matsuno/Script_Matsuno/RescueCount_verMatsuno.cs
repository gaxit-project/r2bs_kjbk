using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RescueCount_verMatsuno : MonoBehaviour
{
    public int RescueMaxNum;   //�ő�~����
    public int RescueNum = 0;   //���݋~���Ґ�
    public bool RescueAll = false;   //�ő�~���Ґ��𖞂������Ƃ��̃t���O
    public RCountText countText;
    public Radio ARadio;


    // Start is called before the first frame update
    void Start()
    {
        RescueNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�e�X�g�p
         if ((Input.GetKeyDown(KeyCode.I)))
         {
            //RescueNum++;
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
