using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Radio_ver3 : MonoBehaviour
{
    [SerializeField] GameObject ChatPanel;
    [SerializeField] GameObject ChatPanel1;
    [SerializeField] GameObject ChatPanel2;
    [SerializeField] GameObject ChatPanel3;

    [HideInInspector] public bool JorE = true;
    [HideInInspector] public bool SwitchONOFF = true;

    //無線のフラグ
    [HideInInspector] public bool RPeople = true;
    [HideInInspector] public bool RPeople2 = true;

    [HideInInspector] public bool CollapseRadio = false;
    [HideInInspector] public bool RHintFlag = false;
    [HideInInspector] public bool RPopFlag = false;
    [HideInInspector] public bool FirstFlag = true;


    public CollGauge CG2;

    [SerializeField] private TMP_Text RadioText;

    public RescuePOP RPOP;


    //無線を出すときとしまうときの時間
    float StartTimer = 15f;   //無線付けるときのタイマー
    float EndTimer = 10f;     //無線をきるときのタイマー


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayCoroutine());
        ChatPanel.SetActive(false);
        ChatPanel1.SetActive(false);
        ChatPanel2.SetActive(false);
        ChatPanel3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(FirstFlag)
        {
            ChatPanel.SetActive(true);
            RadioText.SetText("今回の現場は学生寮だ！行方不明者の半数を救うのが君の任務だ");
            StartCoroutine(Simple());

            Invoke(nameof(FirstRadio1),7.5f);
            Invoke(nameof(FirstRadio2), 11f);
            Invoke(nameof(RadioOFF), StartTimer);
            FirstFlag = false;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (SwitchONOFF)
            {
                Debug.Log("英語化");
                JorE = false;
                SwitchONOFF = false;
            }
            else
            {
                Debug.Log("日本語化");
                JorE = true;
                SwitchONOFF = true;
            }

        }
    }

    public void FirstRadio1()
    {
        RadioText.SetText("重傷者が複数名いるとの情報だ");
        StartCoroutine(Simple());
    }
    public void FirstRadio2()
    {
        RadioText.SetText("軽症者を救いながら情報を集めてくれ");
        StartCoroutine(Simple());
    }
    public void RadioStoper()
    {
        Debug.Log("Radio");
        if (CollapseRadio)
        {
            CollapsePanel();
            StartCoroutine(Simple());
            RadioON();
            Invoke(nameof(RadioOFF), EndTimer);
            CollapseRadio = false;
        }
        else if (RHintFlag)
        {
            RHintStop();
            RHintFlag = false;
        }
        else if (RPopFlag)
        {
            SymbolStop();
            RPopFlag = false;
        }
    }

    public void RadioON()
    {
        ChatPanel.SetActive(true);          //無線のデザインを表示
    }
    public void RadioOFF()
    {
        ChatPanel.SetActive(false);          //無線のデザインを表示
    }
    public void Radio1OFF()
    {
        ChatPanel1.SetActive(false);          //無線のデザインを表示
    }
    public void Radio2OFF()
    {
        ChatPanel2.SetActive(false);          //無線のデザインを表示
    }
    public void Radio3OFF()
    {
        ChatPanel3.SetActive(false);          //無線のデザインを表示
    }

    public int RCnt(int mcnt)
    {
        return mcnt;
    }

    //重傷者の無線を管理
    public void SymbolStop()
    {
        SymbolR();
        StartCoroutine(Simple());
        RadioON();
        Invoke(nameof(RadioOFF), EndTimer);
    }

    //軽症者の無線を管理
    public void RHintStop()
    {
        RHint();
        StartCoroutine(Simple());
        RadioON();
        Invoke(nameof(RadioOFF), EndTimer);
    }

    //倒壊ゲージに関する無線を管理するもの
    public void CollapsePanel()
    {
        Debug.Log("文字入力");
        if (JorE)
        {
            Debug.Log("文字入力2");
            if (CG2.Radio80)
            {
                RadioText.SetText("何か建物にヒビが入っていないか？");
                CG2.Radio80 = false;
            }
            else if (CG2.Radio60)
            {
                RadioText.SetText("ヒビが拡大しているもしかしたら崩れるぞ");
                CG2.Radio60 = false;
            }
            else if (CG2.Radio40)
            {
                RadioText.SetText("壁が崩れ始めている\r\n頑張ってくれ");
                CG2.Radio40 = false;
            }
            else if (CG2.Radio20)
            {
                RadioText.SetText("天井が崩れ始めてるぞ\r\n急いでくれ");
                CG2.Radio20 = false;
            }
            else if (CG2.Radio10)
            {
                RadioText.SetText("倒壊寸前だぞ\r\n速く逃げろ");
                CG2.Radio10 = false;
            }
        }
        else
        {
            if (CG2.Radio80)
            {
                RadioText.SetText("Hello80");
                CG2.Radio80 = false;
            }
            else if (CG2.Radio60)
            {
                RadioText.SetText("Hello60");
                CG2.Radio60 = false;
            }
            else if (CG2.Radio40)
            {
                RadioText.SetText("Hello40");
                CG2.Radio40 = false;
            }
            else if (CG2.Radio20)
            {
                RadioText.SetText("Hello20");
                CG2.Radio20 = false;
            }
            else if (CG2.Radio10)
            {
                RadioText.SetText("Hello10");
                CG2.Radio10 = false;
            }
        }
    }

    

    //重傷者の無線
    public void SymbolR()
    {
        int rnd = RPOP.Rnd;
        if (JorE)
        {
            if (rnd == 0)
            {
                RadioText.SetText("他にも重傷者がいるとの情報だ！至急探してくれ！");
            }
            else if (rnd == 1)
            {
                RadioText.SetText("他にも重傷者がいるとの情報だ！至急探してくれ！");
            }
            else if (rnd == 2)
            {
                RadioText.SetText("他にも重傷者がいるとの情報だ！至急探してくれ！");
            }
            else if (rnd == 3)
            {
                RadioText.SetText("他にも重傷者がいるとの情報だ！至急探してくれ！");
            }
            else if (rnd == 4)
            {
                RadioText.SetText("他にも重傷者がいるとの情報だ！至急探してくれ！");
            }
            else if (rnd == 5)
            {
                RadioText.SetText("他にも重傷者がいるとの情報だ！至急探してくれ！");
            }
        }
        else if(!JorE)
        {
            if (rnd == 0)
            {
                RadioText.SetText("倒壊寸前だぞ\r\n速く逃げろ");
            }
            else if (rnd == 1)
            {
                RadioText.SetText("倒壊寸前だぞ\r\n速く逃げろ");
            }
            else if (rnd == 2)
            {
                RadioText.SetText("倒壊寸前だぞ\r\n速く逃げろ");
            }
            else if (rnd == 3)
            {
                RadioText.SetText("倒壊寸前だぞ\r\n速く逃げろ");
            }
            else if (rnd == 4)
            {
                RadioText.SetText("倒壊寸前だぞ\r\n速く逃げろ");
            }
            else if (rnd == 5)
            {
                RadioText.SetText("倒壊寸前だぞ\r\n速く逃げろ");
            }
        }
        

    }

    //軽症者の無線関連
    public void RHint()
    {
        int Cnt = 0;
        int RCnt = RPOP.MCnt;
        int rnd = RPOP.Rnd;
        Debug.Log("受け取った重傷者番号:" + rnd);
        Debug.Log("受け取った軽症者:" + RCnt);


        if (RPeople2)
        {
            if (RPeople)
            {
                if (RCnt == 0)
                {
                    if (rnd == 0)
                    {
                        RadioText.SetText("キッチンから声が聞こえたとの情報だ！至急向かってくれ！");
                        Debug.Log("一人目の位置確定");
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
                            RadioText.SetText("西の方に人が逃げていったとの情報だ");
                            Debug.Log("1-1");
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("西南方面に人影があったかもしれない一応向かってくれないか");
                            Debug.Log("1-2");
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("バルコニーで人が倒れているとの情報だ");
                            Debug.Log("1-3");
                            RPeople2 = false;
                            //rnd1の無線

                        }
                    }
                    else if (rnd == 2)
                    {
                        if (RCnt % 3 == 1)
                        {
                            RadioText.SetText("北側に人が向かっていったとの情報が入った");
                            Debug.Log("2-1");
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("北西側から叫び声が聞こえたから向かってくれ");
                            Debug.Log("2-2");
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("リビングで人が倒れているとの情報だ");
                            Debug.Log("2-3");
                            RPeople2 = false;
                            //rnd1の無線

                        }
                    }
                    else if (rnd == 3)
                    {
                        if (RCnt % 3 == 1)
                        {
                            RadioText.SetText("北側に人が向かっていったとの情報が入った");
                            Debug.Log("3-1");
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("俺はこの時間よくお風呂に入って...あ、無線を付けたままだった...");
                            Debug.Log("3-2");
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("お風呂場にて滑って動けない人がいるとの情報だ");
                            Debug.Log("3-3");
                            RPeople2 = false;
                            //rnd1の無線

                        }
                    }
                    else if (rnd == 4)
                    {
                        if (RCnt % 3 == 1)
                        {
                            RadioText.SetText("南側に人が向かっていったとの情報が入った");
                            Debug.Log("4-1");
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("この服も煙まみれでもうそろそろ着替えたいぜ！");
                            Debug.Log("4-2");
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("クローゼットにて物が倒れて動けない人がいるとの情報だ");
                            Debug.Log("4-3");
                            RPeople2 = false;
                            //rnd1の無線

                        }
                    }
                    else if (rnd == 5)
                    {
                        if (RCnt % 3 == 1)
                        {
                            RadioText.SetText("西側に走って逃げる人がいたとの情報だ");
                            Debug.Log("5-1");
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 2)
                        {
                            RadioText.SetText("まさかこんな状況で寝てるやつはいないよな...");
                            Debug.Log("5-2");
                            //rnd1の無線
                        }
                        else if (RCnt % 3 == 0)
                        {
                            RadioText.SetText("寝室で全然起きない人がいるみたいだ！急いで起こしに行ってくれ");
                            Debug.Log("5-3");
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
            Debug.Log("救助者フラグ：" + RPeople);
        }
    }

    public void RMessage()
    {
        RadioText.SetText("あなたは命の恩人よ！ありがとう！");
        StartCoroutine(Simple());
        ChatPanel1.SetActive(true);
        Invoke(nameof(Radio1OFF), EndTimer);
    }
    public void RMessage1()
    {
        RadioText.SetText("外の空気うめえ！！");
        StartCoroutine(Simple());
        ChatPanel2.SetActive(true);
        Invoke(nameof(Radio2OFF), EndTimer);
    }
    public void RMessage2()
    {
        RadioText.SetText("助かったよ！ありがとう！");
        StartCoroutine(Simple());
        ChatPanel3.SetActive(true);
        Invoke(nameof(Radio3OFF), EndTimer);
    }



    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 無線を消すときに使うプログラム(あとから配列に変える)
    /// 
    private IEnumerator Simple()
    {
        RadioText.maxVisibleCharacters = 0;

        for(var i = 0; i < RadioText.text.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            RadioText.maxVisibleCharacters = i + 1;
        }
    }
    private IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(7.5f);
    }
}




    