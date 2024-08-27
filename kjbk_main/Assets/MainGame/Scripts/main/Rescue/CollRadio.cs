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

    //救助したときの無線1が重傷者救助時，2～3が軽症者時
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


    //無線のフラグ
    [HideInInspector] public bool RPeople = true;
    [HideInInspector] public bool RPeople2 = true;

    //無線を出すときとしまうときの時間
    float StartTimer = 3f;   //無線付けるときのタイマー
    float EndTimer = 5f;     //無線をきるときのタイマー

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
    /// 倒壊ゲージが指定の値になった場合無線を表示するプログラム(あとから配列にする)
    public void EightGauge()
    {
        Debug.Log("EightGauge");
        EightPanel.SetActive(true);          //80%の時の無線を表示
        Invoke(nameof(EightGauge2), EndTimer);     //5秒後に消す
    }

    public void SixGauge()
    {
        Debug.Log("SixGauge");
        SixPanel.SetActive(true);            //60%の時の無線を表示
        Invoke(nameof(SixGauge2), EndTimer);       //5秒後に消す
    }

    public void FourGauge()
    {
        Debug.Log("FourGauge");
        FourPanel.SetActive(true);           //40%の時の無線を表示
        Invoke(nameof(FourGauge2), EndTimer);      //5秒後に消す
    }

    public void TwoGauge()
    {
        Debug.Log("TwoGauge");
        TwoPanel.SetActive(true);            //20%の時の無線を表示
        Invoke(nameof(TwoGauge2), EndTimer);       //5秒後に消す
    }

    public void OneGauge()
    {
        Debug.Log("OneGauge");
        OnePanel.SetActive(true);            //10%の時の無線を表示
        Invoke(nameof(OneGauge2), EndTimer);       //5秒後に消す
    }

    public void RSeikouRadio()
    {
        Debug.Log("Rseikou");
        RSeikou.SetActive(true);           //救出成功時の無線を表示
        Invoke(nameof(FourGauge2), EndTimer);      //5秒後に消す
    }

    public void RShippaiSRadio()
    {
        Debug.Log("RShippai");
        RShippai.SetActive(true);            //救出失敗時の無線を表示
        Invoke(nameof(TwoGauge2), EndTimer);       //5秒後に消す
    }

    public void AloneRadio()
    {
        Debug.Log("Alone");
        Alone.SetActive(true);            //全員救ったときの無線を表示
        Invoke(nameof(OneGauge2), EndTimer);       //5秒後に消す
    }



    //重傷者のヒントの無線


    public int RCnt(int mcnt)
    {
        return mcnt;
    }

    //重傷者の無線を管理
    public void SymbolStop()
    {
        Invoke(nameof(SymbolR), StartTimer);
    }

    //重傷者の無線
    public void SymbolR()
    {
        int rnd = RPop.Rnd;
        if (rnd == 0)
        {
            FirstKitchen.SetActive(true);            //しょっぱなの重傷者の無線を表示
            Invoke(nameof(RFirstKitchen), EndTimer);
        }
        else if (rnd == 1)
        {
            Balcony1.SetActive(true);            //バルコニーの重傷者が湧いたときの無線を表示
            Invoke(nameof(RBalcony1), EndTimer);
        }
        else if (rnd == 2)
        {
            Kitchen1.SetActive(true);            //キッチンの重傷者が湧いたとき時の無線を表示
            Invoke(nameof(RKitchen1), EndTimer);
        }
        else if (rnd == 3)
        {
            Bath1.SetActive(true);            //風呂の重傷者が湧いたとき時の無線を表示
            Invoke(nameof(RBath1), EndTimer);
        }
        else if (rnd == 4)
        {
            Closet1.SetActive(true);            //クローゼットの重傷者が湧いた時の無線を表示
            Invoke(nameof(RCloset1), EndTimer);
        }
        else if (rnd == 5)
        {
            BedRoom1.SetActive(true);            //寝室の重傷者が湧いた時の無線を表示
            Invoke(nameof(RBedRoom1), EndTimer);
        }

    }


    //軽症者の無線を管理
    public void RHintStop()
    {
        Invoke(nameof(RHint), StartTimer);
    }

    //軽症者の無線関連
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
                        FirstKitchen.SetActive(true);            //しょっぱなの重傷者の位置確定時の無線を表示
                        Invoke(nameof(RFirstKitchen), EndTimer);
                        //無線表示
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
                            Balcony2.SetActive(true);            //バルコニーのヒント1の無線を表示
                            Invoke(nameof(RBalcony2), EndTimer);
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Balcony3.SetActive(true);            //バルコニーのヒント2の無線を表示
                            Invoke(nameof(RBalcony3), EndTimer);
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Balcony4.SetActive(true);            //バルコニーの最終ヒントの無線を表示
                            Invoke(nameof(RBalcony4), EndTimer);
                            RPeople2 = false;
                            //rnd1の無線

                        }
                    }
                    else if (rnd == 2)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Kitchen2.SetActive(true);            //キッチンのヒント1の無線を表示
                            Invoke(nameof(RKitchen2), EndTimer);
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Kitchen3.SetActive(true);            //キッチンのヒント2の無線を表示
                            Invoke(nameof(RKitchen3), EndTimer);
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Kitchen4.SetActive(true);            //キッチンの最終ヒントの無線を表示
                            Invoke(nameof(RKitchen4), EndTimer);
                            RPeople2 = false;
                            //rnd1の無線

                        }
                    }
                    else if (rnd == 3)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Bath2.SetActive(true);            //風呂のヒント1の無線を表示
                            Invoke(nameof(RBath2), EndTimer);
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Bath3.SetActive(true);            //風呂のヒント2の無線を表示
                            Invoke(nameof(RBath3), EndTimer);
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Bath4.SetActive(true);            //風呂の最終ヒントの無線を表示
                            Invoke(nameof(RBath4), EndTimer);
                            RPeople2 = false;
                            //rnd1の無線

                        }
                    }
                    else if (rnd == 4)
                    {
                        if (RCnt % 3 == 1)
                        {
                            Closet2.SetActive(true);            //クローゼットのヒント1の無線を表示
                            Invoke(nameof(RCloset2), EndTimer);
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 2)
                        {
                            Closet3.SetActive(true);            //クローゼットのヒント2の無線を表示
                            Invoke(nameof(RCloset3), EndTimer);
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 0)
                        {
                            Closet4.SetActive(true);            //クローゼットの最終ヒントの無線を表示
                            Invoke(nameof(RCloset4), EndTimer);
                            RPeople2 = false;
                            //rnd1の無線

                        }
                    }
                    else if (rnd == 5)
                    {
                        if (RCnt % 3 == 1)
                        {
                            BedRoom2.SetActive(true);            //寝室のヒント1の無線を表示
                            Invoke(nameof(RBedRoom2), EndTimer);
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 2)
                        {
                            BedRoom3.SetActive(true);            //寝室のヒント2の無線を表示
                            Invoke(nameof(RBedRoom3), EndTimer);
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 0)
                        {
                            BedRoom4.SetActive(true);            //寝室の最終ヒントの無線を表示
                            Invoke(nameof(RBedRoom4), EndTimer);
                            RPeople2 = false;
                            //rnd1の無線

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
    /// 無線を消すときに使うプログラム(あとから配列に変える)
    /// 
    public void EightGauge2()
    {
        EightPanel.SetActive(false);         //EightPanelを消す
    }

    public void SixGauge2()
    {
        SixPanel.SetActive(false);           //SixPanelを消す
    }

    public void FourGauge2()
    {
        FourPanel.SetActive(false);          //FourPanelを消す
    }

    public void TwoGauge2()
    {
        TwoPanel.SetActive(false);           //TwoPanelを消す
    }

    public void OneGauge2()
    {
        OnePanel.SetActive(false);           //OnePanelを消す
    }

    public void RSeikouRadio2()
    {
        RSeikou.SetActive(false);          //FourPanelを消す
    }

    public void RShippaiRadio2()
    {
        RShippai.SetActive(false);           //TwoPanelを消す
    }

    public void AloneRadio2()
    {
        Alone.SetActive(false);           //OnePanelを消す
    }

    public void RFirstKitchen()
    {
        FirstKitchen.SetActive(false);           //OnePanelを消す
    }
    public void RBalcony1()
    {
        Balcony1.SetActive(false);           //OnePanelを消す
    }

    public void RBalcony2()
    {
        Balcony2.SetActive(false);           //OnePanelを消す
    }

    public void RBalcony3()
    {
        Balcony3.SetActive(false);           //OnePanelを消す
    }

    public void RBalcony4()
    {
        Balcony4.SetActive(false);           //OnePanelを消す
    }

    public void RKitchen1()
    {
        Kitchen1.SetActive(false);           //OnePanelを消す
    }

    public void RKitchen2()
    {
        Kitchen2.SetActive(false);           //OnePanelを消す
    }
    public void RKitchen3()
    {
        Kitchen3.SetActive(false);           //OnePanelを消す
    }
    public void RKitchen4()
    {
        Kitchen4.SetActive(false);           //OnePanelを消す
    }
    public void RBath1()
    {
        Bath1.SetActive(false);           //OnePanelを消す
    }
    public void RBath2()
    {
        Bath2.SetActive(false);           //OnePanelを消す
    }
    public void RBath3()
    {
        Bath3.SetActive(false);           //OnePanelを消す
    }
    public void RBath4()
    {
        Bath4.SetActive(false);           //OnePanelを消す
    }
    public void RCloset1()
    {
        Closet1.SetActive(false);           //OnePanelを消す
    }
    public void RCloset2()
    {
        Closet2.SetActive(false);           //OnePanelを消す
    }
    public void RCloset3()
    {
        Closet3.SetActive(false);           //OnePanelを消す
    }
    public void RCloset4()
    {
        Closet4.SetActive(false);           //OnePanelを消す
    }
    public void RBedRoom1()
    {
        BedRoom1.SetActive(false);           //OnePanelを消す
    }
    public void RBedRoom2()
    {
        BedRoom2.SetActive(false);           //OnePanelを消す
    }
    public void RBedRoom3()
    {
        BedRoom3.SetActive(false);           //OnePanelを消す
    }
    public void RBedRoom4()
    {
        BedRoom4.SetActive(false);           //OnePanelを消す
    }

}
