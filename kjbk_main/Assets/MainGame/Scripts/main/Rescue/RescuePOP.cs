using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePOP : MonoBehaviour
{

    public CollRadio Radio;
    public RescuePOP Pop;

    //重傷者をこれらに入れる
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

    //ランダムの値を入れる
    [HideInInspector] public int Rnd = 0;

    //軽症者の人数を入れる
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

    [HideInInspector] public bool ArrowONFlag = false;

    public int rndom;

    //軽症者数
    public int AllRCnt = 3;

    //radio_ver4の呼び出し変数
    public Radio_ver4 Radio4;


    void Start()
    {

    }

    void Update()
    {
        //軽症者を救ったことにする開発キー
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MCnter();
            Radio.RHintStop();
        }
        //重傷者を救ったことにする開発キー
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
            //重傷者を救った
        }
    }


    //軽症者を救ったときに呼び出す関数
    //軽症者を救うとヒントの表示をする
    public void LightR()
    {
        MCnter();             //救った軽症者をカウントする関数
        Radio.RHintStop();    //軽症者のヒントを送る
    }


    //重傷者を救ったときに呼び出す関数
    //重傷者を救うと新しい重傷者を湧かせたりする
    public void HeavyR()
    {
        Radio4.BringDialogue();
        Radio.SymbolStop();   //無線を表示
        RndomONOFF = true;    //ランダムをできるようにする
        Rndom();              //ランダムに数値を入れる
        Rpop();               //上ので出た値の重傷者を湧かせる
        if (First)
        {
            Radio.RPeople = false;  //ラジオの方のフラグをオフ
        }
        First = true;

        if (!Radio.RPeople2)        //ラジオの方のフラグ
        {
            Radio.RPeople = true;   //ラジオの方のフラグをオン
        }
        Radio.RPeople2 = true;
        ArrowONFlag = false;

        //重傷者を救った
    }


    //救った軽症者の人数をカウント
    public int MCnter()
    {
        MCnt++;
        return MCnt;
    }


    public int Rndom() //ランダムの数を入れる関数
    {

        while (RndomONOFF)             //もしフラグがオンならランダムに数値を入れる
        {
            if (!R1 && !R2 && !R3 && !R4 && !R5)  //全員救助されてたら下の処理を無視する
            {

                break;
            }
            else
            {
                Rnd = Random.Range(1, 6);      //1～5の値の中でランダムに1つ入れる

                //ランダムが重複にならないような処理
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
    //重傷者を救ったらこれを起動する
    public void Rpop()
    {
        //ランダムの数値を受け取る
        rndom = Pop.Rnd;

        if (rndom == 1)
        {
            RBalcony.SetActive(true);
            Radio4.Push();
        }
        else if (rndom == 2)
        {
            RKitchen.SetActive(true);
            Radio4.Push();
        }
        else if (rndom == 3)
        {
            RBath.SetActive(true);
            Radio4.Push();
        }
        else if (rndom == 4)
        {
            RCloset.SetActive(true);
            Radio4.Push();
        }
        else if (rndom == 5)
        {
            RBedRoom.SetActive(true);
            Radio4.Push();
        }
    }

    public void PopR()
    {
        if (cnt == 1)
        {

            FirstPop.SetActive(true);
            AllRCnt = AllRCnt + 5;
        }
        else if (cnt == 2)
        {

            SecondPop.SetActive(true);
            AllRCnt = AllRCnt + 4;
        }
        else if (cnt == 3)
        {

            ThirdPop.SetActive(true);
            AllRCnt = AllRCnt + 5;
        }
        else if (cnt == 4)
        {

            ForthPop.SetActive(true);
            AllRCnt = AllRCnt + 4;
        }
        else if (cnt == 5)
        {

            FifthPop.SetActive(true);
            AllRCnt = AllRCnt + 3;
        }
        cnt++;
    }
}
