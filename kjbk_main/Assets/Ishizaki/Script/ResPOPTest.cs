using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResPOPTest : MonoBehaviour
{

    public CollRadio Radio;
    public ResPOPTest Pop;
    //public CircleUI circleUI;

    //�d���҂������ɓ����
    [SerializeField] GameObject RBalcony;
    [SerializeField] GameObject RKitchen;
    [SerializeField] GameObject RBath;
    [SerializeField] GameObject RCloset;
    [SerializeField] GameObject RBedRoom;

    //�����_���̒l������
    [HideInInspector] public int Rnd = 0;

    //�y�ǎ҂̐l��������
    [HideInInspector] public int MCnt = -1;
    int a = 0;

    bool First = false;
    bool RndomONOFF = true;

    bool R1 = true;
    bool R2 = true;
    bool R3 = true;
    bool R4 = true;
    bool R5 = true;


    void Start()
    {
        RBalcony.SetActive(false);
        RKitchen.SetActive(false);
        RBath.SetActive(false);
        RCloset.SetActive(false);
        RBedRoom.SetActive(false);

    }

    void Update()
    {
        //�y�ǎ҂��~�������Ƃɂ���J���L�[
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MCnter();
            Radio.RHintStop();
        }
        //�d���҂��~�������Ƃɂ���J���L�[
        if (Input.GetKeyDown(KeyCode.C))
        {
            Radio.SymbolStop();
            RndomONOFF = true;
            Rndom();
            Rpop();
            if (First)
            {
                Radio.RPeople = false;
            }
            First = true;

            if (!Radio.RPeople2)
            {
                Radio.RPeople = true;
            }
            Radio.RPeople2 = true;

            Debug.Log("�d���҂��~�����F" + Radio.RPeople);
        }
    }


    //�y�ǎ҂��~�����Ƃ��ɌĂяo���֐�
    //�y�ǎ҂��~���ƃq���g�̕\��������
    public void LightR()
    {
        Debug.Log("aaaaaaa");
        MCnter();             //�~�����y�ǎ҂��J�E���g����֐�
        Debug.Log("bbbbbbbb");
        Radio.RHintStop();    //�y�ǎ҂̃q���g�𑗂�
        Debug.Log("ccccccc");
    }


    //�d���҂��~�����Ƃ��ɌĂяo���֐�
    //�d���҂��~���ƐV�����d���҂�N�������肷��
    public void HeavyR()
    {
        Radio.SymbolStop();   //������\��
        RndomONOFF = true;    //�����_�����ł���悤�ɂ���
        Rndom();              //�����_���ɐ��l������
        Rpop();               //��̂ŏo���l�̏d���҂�N������
        if (First)
        {
            Radio.RPeople = false;  //���W�I�̕��̃t���O���I�t
        }
        First = true;

        if (!Radio.RPeople2)        //���W�I�̕��̃t���O
        {
            Radio.RPeople = true;   //���W�I�̕��̃t���O���I��
        }
        Radio.RPeople2 = true;

        Debug.Log("�d���҂��~�����F" + Radio.RPeople);

        //bool State = circleUI.
    }


    //�~�����y�ǎ҂̐l�����J�E���g
    public int MCnter()
    {
        MCnt++;
        Debug.Log("�y�ǎ҂̋~���l��:" + MCnt);
        return MCnt;
    }


    public int Rndom() //�����_���̐�������֐�
    {
        //bool R;
        //Rnd = 1;
        //Rnd = Random.Range(1, 6);   //1�`5�܂ł̐��������_���ɓ����


        //for (int i = 0; i < RandomArray.Length; i++)
        //{
        //    if (RandomArray[i] == Rnd)
        //    {
        //        Rnd = Random.Range(1, 6);
        //        Debug.Log("nnnnnnnnnnnnnnnn");
        //        i = 0;
        //    }
        //}
        //RandomArray[cnt] = Rnd;
        //Debug.Log("�����_���ɓ��ꂽ��:" + Rnd);
        //cnt++;

        //for (int i = 0; i < RandomArray.Length; i++)
        //{
        //    Debug.Log(RandomArray[i]);
        //}
        while (RndomONOFF)             //�����t���O���I���Ȃ烉���_���ɐ��l������
        {
            if (!R1 && !R2 && !R3 && !R4 && !R5)  //�S���~������Ă��牺�̏����𖳎�����
            {

                break;
            }
            else
            {
                Rnd = Random.Range(1, 6);      //1�`5�̒l�̒��Ń����_����1�����
                Debug.Log(Rnd);

                //�����_�����d���ɂȂ�Ȃ��悤�ȏ���
                if (Rnd == 1 && R1 || Rnd == 2 && R2 || Rnd == 3 && R3 || Rnd == 4 && R4 || Rnd == 5 && R5)
                {

                    if (Rnd == 1)
                    {
                        R1 = false;
                    }
                    else if (Rnd == 2)
                    {
                        R2 = false;
                    }
                    else if (Rnd == 3)
                    {
                        R3 = false;
                    }
                    else if (Rnd == 4)
                    {
                        R4 = false;
                    }
                    else if (Rnd == 5)
                    {
                        R5 = false;
                    }
                    RndomONOFF = false;
                    break;
                }
            }


        }




        return Rnd;
    }
    //�d���҂��~�����炱����N������
    public void Rpop()
    {
        //�����_���̐��l���󂯎��
        int rndom = Pop.Rnd;
        Debug.Log("�|�b�v����ۂ̎󂯎��:" + rndom);

        if (rndom == 1)
        {
            RBalcony.SetActive(true);
        }
        else if (rndom == 2)
        {
            RKitchen.SetActive(true);
        }
        else if (rndom == 3)
        {
            RBath.SetActive(true);
        }
        else if (rndom == 4)
        {
            RCloset.SetActive(true);
        }
        else if (rndom == 5)
        {
            RBedRoom.SetActive(true);
        }
    }
}
