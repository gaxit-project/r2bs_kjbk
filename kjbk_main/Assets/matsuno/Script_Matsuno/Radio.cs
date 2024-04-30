using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{

    [SerializeField] GameObject EightPanel;
    [SerializeField] GameObject SixPanel;
    [SerializeField] GameObject FourPanel;
    [SerializeField] GameObject TwoPanel;
    [SerializeField] GameObject OnePanel;



    void Start()
    {
        EightPanel.SetActive(false);
        SixPanel.SetActive(false);
        FourPanel.SetActive(false);
        TwoPanel.SetActive(false);
        OnePanel.SetActive(false);

    }
/// <summary>
/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// �|��Q�[�W���w��̒l�ɂȂ����ꍇ������\������v���O����(���Ƃ���z��ɂ���)
    public void EightGauge()
    {
        Debug.Log("EightGauge");
        EightPanel.SetActive(true);          //80%�̎��̖�����\��
        Invoke(nameof(EightGauge2), 5f);     //5�b��ɏ���
    }

    public void SixGauge()
    {
        Debug.Log("SixGauge");
        SixPanel.SetActive(true);            //60%�̎��̖�����\��
        Invoke(nameof(SixGauge2), 5f);       //5�b��ɏ���
    }

    public void FourGauge()
    {
        Debug.Log("FourGauge");
        FourPanel.SetActive(true);           //40%�̎��̖�����\��
        Invoke(nameof(FourGauge2), 5f);      //5�b��ɏ���
    }

    public void TwoGauge()
    {
        Debug.Log("TwoGauge");
        TwoPanel.SetActive(true);            //20%�̎��̖�����\��
        Invoke(nameof(TwoGauge2), 5f);       //5�b��ɏ���
    }

    public void OneGauge()
    {
        Debug.Log("OneGauge");
        OnePanel.SetActive(true);            //10%�̎��̖�����\��
        Invoke(nameof(OneGauge2), 5f);       //5�b��ɏ���
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


}