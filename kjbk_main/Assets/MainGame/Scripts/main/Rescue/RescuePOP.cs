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

            Debug.Log("重傷者を救った：" + Radio.RPeople);
        }
    }


    //軽症者を救ったときに呼び出す関数
    //軽症者を救うとヒントの表示をする
    public void LightR()
    {
        Debug.Log("aaaaaaa");
        MCnter();             //救った軽症者をカウントする関数
        Debug.Log("bbbbbbbb");
        Radio.RHintStop();    //軽症者のヒントを送る
        Debug.Log("ccccccc");
    }


    //重傷者を救ったときに呼び出す関数
    //重傷者を救うと新しい重傷者を湧かせたりする
    public void HeavyR()
    {
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

        Debug.Log("重傷者を救った：" + Radio.RPeople);
    }


    //救った軽症者の人数をカウント
    public int MCnter()
    {
        MCnt++;
        Debug.Log("軽症者の救助人数:" + MCnt);
        return MCnt;
    }


    public int Rndom() //ランダムの数を入れる関数
    {
        //bool R;
        //Rnd = 1;
        //Rnd = Random.Range(1, 6);   //1～5までの数をランダムに入れる


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
        //Debug.Log("ランダムに入れた数:" + Rnd);
        //cnt++;

        //for (int i = 0; i < RandomArray.Length; i++)
        //{
        //    Debug.Log(RandomArray[i]);
        //}
        while (RndomONOFF)             //もしフラグがオンならランダムに数値を入れる
        {
            if (!R1 && !R2 && !R3 && !R4 && !R5)  //全員救助されてたら下の処理を無視する
            {

                break;
            }
            else
            {
                Rnd = Random.Range(1, 6);      //1～5の値の中でランダムに1つ入れる
                Debug.Log(Rnd);

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
        int rndom = Pop.Rnd;
        Debug.Log("ポップする際の受け取り:" + rndom);

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
