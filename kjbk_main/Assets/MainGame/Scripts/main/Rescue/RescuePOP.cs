using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePOP : MonoBehaviour
{

    public CollRadio Radio;
    public RescuePOP Pop;

    //�d���҂������ɓ����
    [SerializeField] GameObject RBalcony;
    [SerializeField] GameObject RKitchen;
    [SerializeField] GameObject RBath;
    [SerializeField] GameObject RCloset;
    [SerializeField] GameObject RBedRoom;


    [SerializeField] GameObject hito1st;
    [SerializeField] GameObject JK1st;
    [SerializeField] GameObject kurohuku1st;
    [SerializeField] GameObject ILOVENY1st;
    [SerializeField] GameObject hito1_st;
    [SerializeField] GameObject hito2nd;
    [SerializeField] GameObject JK2nd;
    [SerializeField] GameObject kurohuku2nd;
    [SerializeField] GameObject hito3rd;
    [SerializeField] GameObject JK3rd;
    [SerializeField] GameObject hito3_2rd;
    [SerializeField] GameObject kurohuku4th;
    [SerializeField] GameObject ILOVENY4th;
    [SerializeField] GameObject hito4th;
    [SerializeField] GameObject JK5th;
    [SerializeField] GameObject kurohuku5th;
    [SerializeField] GameObject ILOVENY5th;

    [SerializeField] GameObject FirstPop;
    [SerializeField] GameObject SecondPop; 
    [SerializeField] GameObject ThirdPop;
    [SerializeField] GameObject ForthPop;
    [SerializeField] GameObject FifthPop;

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

    int cnt = 1;


    void Start()
    {

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

    public void PopR()
    {
        if (cnt == 1)
        {
            //hito1st.SetActive(true);
            //JK1st.SetActive(true);
            //kurohuku1st.SetActive(true);
            //ILOVENY1st.SetActive(true);
            //hito1_st.SetActive(true);
            FirstPop.SetActive(true);
        }
        else if (cnt == 2)
        {
            //hito2nd.SetActive(true);
            //JK2nd.SetActive(true);
            //kurohuku2nd.SetActive(true);
            SecondPop.SetActive(true);
        }
        else if (cnt == 3)
        {
            //hito3rd.SetActive(true);
            //JK3rd.SetActive(true);
            //hito3_2rd.SetActive(true);
            ThirdPop.SetActive(true);
        }
        else if (cnt == 4)
        {
            //kurohuku4th.SetActive(true);
            //ILOVENY4th.SetActive(true);
            // hito4th.SetActive(true);
            ForthPop.SetActive(true);
        }
        else if (cnt == 5)
        {
            //JK5th.SetActive(true);
            //kurohuku5th.SetActive(true);
            //ILOVENY5th.SetActive(true);
            FifthPop.SetActive(true);
        }
        cnt++;
    }
}
