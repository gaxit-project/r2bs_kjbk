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
    [SerializeField] GameObject RSeikou;
    [SerializeField] GameObject RShippai;
    [SerializeField] GameObject Alone;



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
/// 倒壊ゲージが指定の値になった場合無線を表示するプログラム(あとから配列にする)
    public void EightGauge()
    {
        Debug.Log("EightGauge");
        EightPanel.SetActive(true);          //80%の時の無線を表示
        Invoke(nameof(EightGauge2), 10f);     //5秒後に消す
    }

    public void SixGauge()
    {
        Debug.Log("SixGauge");
        SixPanel.SetActive(true);            //60%の時の無線を表示
        Invoke(nameof(SixGauge2), 10f);       //5秒後に消す
    }

    public void FourGauge()
    {
        Debug.Log("FourGauge");
        FourPanel.SetActive(true);           //40%の時の無線を表示
        Invoke(nameof(FourGauge2), 10f);      //5秒後に消す
    }

    public void TwoGauge()
    {
        Debug.Log("TwoGauge");
        TwoPanel.SetActive(true);            //20%の時の無線を表示
        Invoke(nameof(TwoGauge2), 10f);       //5秒後に消す
    }

    public void OneGauge()
    {
        Debug.Log("OneGauge");
        OnePanel.SetActive(true);            //10%の時の無線を表示
        Invoke(nameof(OneGauge2), 10f);       //5秒後に消す
    }

    public void RSeikouRadio()
    {
        Debug.Log("Rseikou");
        RSeikou.SetActive(true);           //40%の時の無線を表示
        Invoke(nameof(FourGauge2), 10f);      //5秒後に消す
    }

    public void RShippaiSRadio()
    {
        Debug.Log("RShippai");
        RShippai.SetActive(true);            //20%の時の無線を表示
        Invoke(nameof(TwoGauge2), 10f);       //5秒後に消す
    }

    public void AloneRadio()
    {
        Debug.Log("Alone");
        Alone.SetActive(true);            //10%の時の無線を表示
        Invoke(nameof(OneGauge2), 10f);       //5秒後に消す
    }


    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 無線を消すときに使うプログラム(あとから配列に変える)
    /// 
    public void EightGauge2()
    {
        Debug.Log("EightGauge");
        EightPanel.SetActive(false);         //EightPanelを消す
    }

    public void SixGauge2()
    {
        Debug.Log("SixGauge");
        SixPanel.SetActive(false);           //SixPanelを消す
    }

    public void FourGauge2()
    {
        Debug.Log("FourGauge");
        FourPanel.SetActive(false);          //FourPanelを消す
    }

    public void TwoGauge2()
    {
        Debug.Log("TwoGauge");
        TwoPanel.SetActive(false);           //TwoPanelを消す
    }

    public void OneGauge2()
    {
        Debug.Log("OneGauge");
        OnePanel.SetActive(false);           //OnePanelを消す
    }

    public void RSeikouRadio2()
    {
        Debug.Log("RSeikou");
        RSeikou.SetActive(false);          //FourPanelを消す
    }

    public void RShippaiRadio2()
    {
        Debug.Log("RShippai");
        RShippai.SetActive(false);           //TwoPanelを消す
    }

    public void AloneRadio2()
    {
        Debug.Log("Alone");
        Alone.SetActive(false);           //OnePanelを消す
    }


}