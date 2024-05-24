using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Radio_verMatsuno : MonoBehaviour
{

    public RescuePop RPop;
    public RescueNPC_verMatsuno RNPC; 

    [SerializeField] GameObject EightPanel;
    [SerializeField] GameObject SixPanel;
    [SerializeField] GameObject FourPanel;
    [SerializeField] GameObject TwoPanel;
    [SerializeField] GameObject OnePanel;
    [SerializeField] GameObject RSeikou;
    [SerializeField] GameObject RShippai;
    [SerializeField] GameObject Alone;


    [HideInInspector] public bool RPeople = true;
    [HideInInspector] public bool RPeople2 = true;

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
    }
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �|��Q�[�W���w��̒l�ɂȂ����ꍇ������\������v���O����(���Ƃ���z��ɂ���)
    public void EightGauge()
    {
        Debug.Log("EightGauge");
        EightPanel.SetActive(true);          //80%�̎��̖�����\��
        Invoke(nameof(EightGauge2), 10f);     //5�b��ɏ���
    }

    public void SixGauge()
    {
        Debug.Log("SixGauge");
        SixPanel.SetActive(true);            //60%�̎��̖�����\��
        Invoke(nameof(SixGauge2), 10f);       //5�b��ɏ���
    }

    public void FourGauge()
    {
        Debug.Log("FourGauge");
        FourPanel.SetActive(true);           //40%�̎��̖�����\��
        Invoke(nameof(FourGauge2), 10f);      //5�b��ɏ���
    }

    public void TwoGauge()
    {
        Debug.Log("TwoGauge");
        TwoPanel.SetActive(true);            //20%�̎��̖�����\��
        Invoke(nameof(TwoGauge2), 10f);       //5�b��ɏ���
    }

    public void OneGauge()
    {
        Debug.Log("OneGauge");
        OnePanel.SetActive(true);            //10%�̎��̖�����\��
        Invoke(nameof(OneGauge2), 10f);       //5�b��ɏ���
    }

    public void RSeikouRadio()
    {
        Debug.Log("Rseikou");
        RSeikou.SetActive(true);           //�~�o�������̖�����\��
        Invoke(nameof(FourGauge2), 10f);      //5�b��ɏ���
    }

    public void RShippaiSRadio()
    {
        Debug.Log("RShippai");
        RShippai.SetActive(true);            //�~�o���s���̖�����\��
        Invoke(nameof(TwoGauge2), 10f);       //5�b��ɏ���
    }

    public void AloneRadio()
    {
        Debug.Log("Alone");
        Alone.SetActive(true);            //�S���~�����Ƃ��̖�����\��
        Invoke(nameof(OneGauge2), 10f);       //5�b��ɏ���
    }



    //�d���҂̃q���g�̖���


    public int RCnt(int mcnt)
    {
        return mcnt;
    }
    public void RHint()
    {
        int Cnt = 0;
        int RCnt = RPop.MCnt;
        int rnd = RPop.Rnd; 
        Debug.Log("�󂯎�����d���Ҕԍ�:" + rnd);
        Debug.Log("�󂯎�����y�ǎ�:" + RCnt);


        if(RPeople2)
        {
            if (RPeople)
            {
                if (RCnt == 0)
                {
                    Debug.Log("��l�ڂ̈ʒu�m��");
                    //�����\��
                }
                else
                {
                    if (rnd == 1)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Debug.Log("1-1");
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Debug.Log("1-2");
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Debug.Log("1-3");
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 2)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Debug.Log("2-1");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Debug.Log("2-2");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Debug.Log("2-3");
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 3)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Debug.Log("3-1");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Debug.Log("3-2");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Debug.Log("3-3");
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 4)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Debug.Log("4-1");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Debug.Log("4-2");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Debug.Log("4-3");
                            RPeople2 = false;
                            //rnd1�̖���

                        }
                    }
                    else if (rnd == 5)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Debug.Log("5-1");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Debug.Log("5-2");
                            //rnd1�̖���
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Debug.Log("5-3");
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
            Debug.Log("�~���҃t���O�F" + RPeople);
        }



    }


    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// �����������Ƃ��Ɏg���v���O����(���Ƃ���z��ɕς���)
    /// 
    public void EightGauge2()
    {
        Debug.Log("EightGauge");
        EightPanel.SetActive(false);         //EightPanel������
    }

    public void SixGauge2()
    {
        Debug.Log("SixGauge");
        SixPanel.SetActive(false);           //SixPanel������
    }

    public void FourGauge2()
    {
        Debug.Log("FourGauge");
        FourPanel.SetActive(false);          //FourPanel������
    }

    public void TwoGauge2()
    {
        Debug.Log("TwoGauge");
        TwoPanel.SetActive(false);           //TwoPanel������
    }

    public void OneGauge2()
    {
        Debug.Log("OneGauge");
        OnePanel.SetActive(false);           //OnePanel������
    }

    public void RSeikouRadio2()
    {
        Debug.Log("RSeikou");
        RSeikou.SetActive(false);          //FourPanel������
    }

    public void RShippaiRadio2()
    {
        Debug.Log("RShippai");
        RShippai.SetActive(false);           //TwoPanel������
    }

    public void AloneRadio2()
    {
        Debug.Log("Alone");
        Alone.SetActive(false);           //OnePanel������
    }


}
