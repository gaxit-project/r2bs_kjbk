using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CollRadio : MonoBehaviour
{

    public RescuePOP RPop;

    [SerializeField] GameObject EightPanel;
    [SerializeField] GameObject SixPanel;
    [SerializeField] GameObject FourPanel;
    [SerializeField] GameObject TwoPanel;
    [SerializeField] GameObject OnePanel;
    [SerializeField] GameObject RSeikou;
    [SerializeField] GameObject RShippai;
    [SerializeField] GameObject Alone;

    //�~�������Ƃ��̖���1���d���ҋ~�����C2�`3���y�ǎҎ�
    [SerializeField] GameObject FirstKitchen;
    [SerializeField] GameObject Balcony1;
    [SerializeField] GameObject Balcony2;
    [SerializeField] GameObject Balcony3;
    [SerializeField] GameObject Balcony4;
    [SerializeField] GameObject Kitchen1;
    [SerializeField] GameObject Kitchen2;
    [SerializeField] GameObject Kitchen3;
    [SerializeField] GameObject Kitchen4;
    [SerializeField] GameObject Bath1;
    [SerializeField] GameObject Bath2;
    [SerializeField] GameObject Bath3;
    [SerializeField] GameObject Bath4;
    [SerializeField] GameObject Closet1;
    [SerializeField] GameObject Closet2;
    [SerializeField] GameObject Closet3;
    [SerializeField] GameObject Closet4;
    [SerializeField] GameObject BedRoom1;
    [SerializeField] GameObject BedRoom2;
    [SerializeField] GameObject BedRoom3;
    [SerializeField] GameObject BedRoom4;


    //�����̃t���O
    [HideInInspector] public bool RPeople = true;
    [HideInInspector] public bool RPeople2 = true;

    //�������o���Ƃ��Ƃ��܂��Ƃ��̎���
    float StartTimer = 3f;   //�����t����Ƃ��̃^�C�}�[
    float EndTimer = 5f;     //����������Ƃ��̃^�C�}�[

    void Start()
    {
        EightPanel.SetActive(false);
        SixPanel.SetActive(false);
        FourPanel.SetActive(false);
        TwoPanel.SetActive(false);
        OnePanel.SetActive(false);
        RSeikou.SetActive(false);
        RShippai.SetActive(false);
        Alone.SetActive(false);
        FirstKitchen.SetActive(false);
        Balcony1.SetActive(false);
        Balcony2.SetActive(false);
        Balcony3.SetActive(false);
        Balcony4.SetActive(false);
        Kitchen1.SetActive(false);
        Kitchen2.SetActive(false);
        Kitchen3.SetActive(false);
        Kitchen4.SetActive(false);
        Bath1.SetActive(false);
        Bath2.SetActive(false);
        Bath3.SetActive(false);
        Bath4.SetActive(false);
        Closet1.SetActive(false);
        Closet2.SetActive(false);
        Closet3.SetActive(false);
        Closet4.SetActive(false);
        BedRoom1.SetActive(false);
        BedRoom2.SetActive(false);
        BedRoom3.SetActive(false);
        BedRoom4.SetActive(false);
    }
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �|��Q�[�W���w��̒l�ɂȂ����ꍇ������\������v���O����(���Ƃ���z��ɂ���)
    public void EightGauge()
    {
        Debug.Log("EightGauge");
        EightPanel.SetActive(true);          //80%�̎��̖�����\��
        Invoke(nameof(EightGauge2), EndTimer);     //5�b��ɏ���
    }

    public void SixGauge()
    {
        Debug.Log("SixGauge");
        SixPanel.SetActive(true);            //60%�̎��̖�����\��
        Invoke(nameof(SixGauge2), EndTimer);       //5�b��ɏ���
    }

    public void FourGauge()
    {
        Debug.Log("FourGauge");
        FourPanel.SetActive(true);           //40%�̎��̖�����\��
        Invoke(nameof(FourGauge2), EndTimer);      //5�b��ɏ���
    }

    public void TwoGauge()
    {
        Debug.Log("TwoGauge");
        TwoPanel.SetActive(true);            //20%�̎��̖�����\��
        Invoke(nameof(TwoGauge2), EndTimer);       //5�b��ɏ���
    }

    public void OneGauge()
    {
        Debug.Log("OneGauge");
        OnePanel.SetActive(true);            //10%�̎��̖�����\��
        Invoke(nameof(OneGauge2), EndTimer);       //5�b��ɏ���
    }

    public void RSeikouRadio()
    {
        Debug.Log("Rseikou");
        RSeikou.SetActive(true);           //�~�o�������̖�����\��
        Invoke(nameof(FourGauge2), EndTimer);      //5�b��ɏ���
    }

    public void RShippaiSRadio()
    {
        Debug.Log("RShippai");
        RShippai.SetActive(true);            //�~�o���s���̖�����\��
        Invoke(nameof(TwoGauge2), EndTimer);       //5�b��ɏ���
    }

    public void AloneRadio()
    {
        Debug.Log("Alone");
        Alone.SetActive(true);            //�S���~�����Ƃ��̖�����\��
        Invoke(nameof(OneGauge2), EndTimer);       //5�b��ɏ���
    }



    //�d���҂̃q���g�̖���


    public int RCnt(int mcnt)
    {
        return mcnt;
    }

    //�d���҂̖������Ǘ�
    public void SymbolStop()
    {
        Invoke(nameof(SymbolR), StartTimer);
    }

    //�d���҂̖���
    public void SymbolR()
    {
        int rnd = RPop.Rnd;
        if (rnd == 0)
        {
            FirstKitchen.SetActive(true);            //������ςȂ̏d���҂̖�����\��
            Invoke(nameof(RFirstKitchen), EndTimer);
        }
        else if (rnd == 1)
        {
            Balcony1.SetActive(true);            //�o���R�j�[�̏d���҂��N�����Ƃ��̖�����\��
            Invoke(nameof(RBalcony1), EndTimer);
        }
        else if (rnd == 2)
        {
            Kitchen1.SetActive(true);            //�L�b�`���̏d���҂��N�����Ƃ����̖�����\��
            Invoke(nameof(RKitchen1), EndTimer);
        }
        else if (rnd == 3)
        {
            Bath1.SetActive(true);            //���C�̏d���҂��N�����Ƃ����̖�����\��
            Invoke(nameof(RBath1), EndTimer);
        }
        else if (rnd == 4)
        {
            Closet1.SetActive(true);            //�N���[�[�b�g�̏d���҂��N�������̖�����\��
            Invoke(nameof(RCloset1), EndTimer);
        }
        else if (rnd == 5)
        {
            BedRoom1.SetActive(true);            //�Q���̏d���҂��N�������̖�����\��
            Invoke(nameof(RBedRoom1), EndTimer);
        }

    }


    //�y�ǎ҂̖������Ǘ�
    public void RHintStop()
    {
        Invoke(nameof(RHint), StartTimer);
    }

    //�y�ǎ҂̖����֘A
    public void RHint()
    {
        int Cnt = 0;
        int RCnt = RPop.MCnt;
        int rnd = RPop.Rnd;



        if (RPeople2)
        {
            if (RPeople)
            {
                if (RCnt == 0)
                {
                    if (rnd == 0)
                    {
                        FirstKitchen.SetActive(true);            //������ςȂ̏d���҂̈ʒu�m�莞�̖�����\��
                        Invoke(nameof(RFirstKitchen), EndTimer);
                        //�����\��
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (rnd == 1)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Balcony2.SetActive(true);            //�o���R�j�[�̃q���g1�̖�����\��
                            Invoke(nameof(RBalcony2), EndTimer);
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Balcony3.SetActive(true);            //�o���R�j�[�̃q���g2�̖�����\��
                            Invoke(nameof(RBalcony3), EndTimer);
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Balcony4.SetActive(true);            //�o���R�j�[�̍ŏI�q���g�̖�����\��
                            Invoke(nameof(RBalcony4), EndTimer);
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 2)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Kitchen2.SetActive(true);            //�L�b�`���̃q���g1�̖�����\��
                            Invoke(nameof(RKitchen2), EndTimer);
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Kitchen3.SetActive(true);            //�L�b�`���̃q���g2�̖�����\��
                            Invoke(nameof(RKitchen3), EndTimer);
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Kitchen4.SetActive(true);            //�L�b�`���̍ŏI�q���g�̖�����\��
                            Invoke(nameof(RKitchen4), EndTimer);
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 3)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Bath2.SetActive(true);            //���C�̃q���g1�̖�����\��
                            Invoke(nameof(RBath2), EndTimer);
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Bath3.SetActive(true);            //���C�̃q���g2�̖�����\��
                            Invoke(nameof(RBath3), EndTimer);
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Bath4.SetActive(true);            //���C�̍ŏI�q���g�̖�����\��
                            Invoke(nameof(RBath4), EndTimer);
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 4)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Closet2.SetActive(true);            //�N���[�[�b�g�̃q���g1�̖�����\��
                            Invoke(nameof(RCloset2), EndTimer);
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Closet3.SetActive(true);            //�N���[�[�b�g�̃q���g2�̖�����\��
                            Invoke(nameof(RCloset3), EndTimer);
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Closet4.SetActive(true);            //�N���[�[�b�g�̍ŏI�q���g�̖�����\��
                            Invoke(nameof(RCloset4), EndTimer);
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 5)
                    {
                        if (RCnt % 3 == 1)
                        {
                            BedRoom2.SetActive(true);            //�Q���̃q���g1�̖�����\��
                            Invoke(nameof(RBedRoom2), EndTimer);
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            BedRoom3.SetActive(true);            //�Q���̃q���g2�̖�����\��
                            Invoke(nameof(RBedRoom3), EndTimer);
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            BedRoom4.SetActive(true);            //�Q���̍ŏI�q���g�̖�����\��
                            Invoke(nameof(RBedRoom4), EndTimer);
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                }
            }
        }

        if (RCnt % 3 == 0)
        {
            RPeople = true;
        }



    }


    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �����������Ƃ��Ɏg���v���O����(���Ƃ���z��ɕς���)
    /// 
    public void EightGauge2()
    {
        EightPanel.SetActive(false);         //EightPanel������
    }

    public void SixGauge2()
    {
        SixPanel.SetActive(false);           //SixPanel������
    }

    public void FourGauge2()
    {
        FourPanel.SetActive(false);          //FourPanel������
    }

    public void TwoGauge2()
    {
        TwoPanel.SetActive(false);           //TwoPanel������
    }

    public void OneGauge2()
    {
        OnePanel.SetActive(false);           //OnePanel������
    }

    public void RSeikouRadio2()
    {
        RSeikou.SetActive(false);          //FourPanel������
    }

    public void RShippaiRadio2()
    {
        RShippai.SetActive(false);           //TwoPanel������
    }

    public void AloneRadio2()
    {
        Alone.SetActive(false);           //OnePanel������
    }

    public void RFirstKitchen()
    {
        FirstKitchen.SetActive(false);           //OnePanel������
    }
    public void RBalcony1()
    {
        Balcony1.SetActive(false);           //OnePanel������
    }

    public void RBalcony2()
    {
        Balcony2.SetActive(false);           //OnePanel������
    }

    public void RBalcony3()
    {
        Balcony3.SetActive(false);           //OnePanel������
    }

    public void RBalcony4()
    {
        Balcony4.SetActive(false);           //OnePanel������
    }

    public void RKitchen1()
    {
        Kitchen1.SetActive(false);           //OnePanel������
    }

    public void RKitchen2()
    {
        Kitchen2.SetActive(false);           //OnePanel������
    }
    public void RKitchen3()
    {
        Kitchen3.SetActive(false);           //OnePanel������
    }
    public void RKitchen4()
    {
        Kitchen4.SetActive(false);           //OnePanel������
    }
    public void RBath1()
    {
        Bath1.SetActive(false);           //OnePanel������
    }
    public void RBath2()
    {
        Bath2.SetActive(false);           //OnePanel������
    }
    public void RBath3()
    {
        Bath3.SetActive(false);           //OnePanel������
    }
    public void RBath4()
    {
        Bath4.SetActive(false);           //OnePanel������
    }
    public void RCloset1()
    {
        Closet1.SetActive(false);           //OnePanel������
    }
    public void RCloset2()
    {
        Closet2.SetActive(false);           //OnePanel������
    }
    public void RCloset3()
    {
        Closet3.SetActive(false);           //OnePanel������
    }
    public void RCloset4()
    {
        Closet4.SetActive(false);           //OnePanel������
    }
    public void RBedRoom1()
    {
        BedRoom1.SetActive(false);           //OnePanel������
    }
    public void RBedRoom2()
    {
        BedRoom2.SetActive(false);           //OnePanel������
    }
    public void RBedRoom3()
    {
        BedRoom3.SetActive(false);           //OnePanel������
    }
    public void RBedRoom4()
    {
        BedRoom4.SetActive(false);           //OnePanel������
    }

}
