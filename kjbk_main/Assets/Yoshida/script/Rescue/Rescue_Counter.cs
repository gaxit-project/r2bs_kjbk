using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescue_Counter : MonoBehaviour
{
    public int RescueMaxNum;   //�ő�~����
    [SerializeField] private int RescueNum = 0;   //���݋~���Ґ�
    public bool RescueAll = false;   //�ő�~���Ґ��𖞂������Ƃ��̃t���O

    // Start is called before the first frame update
    void Start()
    {
        RescueNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(RescueNum == RescueMaxNum)   //�ő�~���Ґ��𖞂����Ă��邩�̔�r
        {
            RescueAll = true;
        }
        //�e�X�g�p��
        
    }

    public void Count()   //���݋~���Ґ��̃J�E���g
    {
        RescueNum++;
    }

    public int getNum()   //���݋~���Ґ��̎擾
    {
        return RescueNum;
    }

    public bool getRescueAll()   //�t���O�̎擾
    {
        return RescueAll;
    }
}
