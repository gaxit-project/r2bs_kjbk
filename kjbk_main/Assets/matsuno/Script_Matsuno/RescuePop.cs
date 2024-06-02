using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePop : MonoBehaviour
{
    // Start is called before the first frame update

    public Radio_verMatsuno Radio;
    public RescuePop Pop;

    [SerializeField] GameObject RBalcony;
    [SerializeField] GameObject RKitchen;
    [SerializeField] GameObject RBath;
    [SerializeField] GameObject RCloset;
    [SerializeField] GameObject RBedRoom;

    [HideInInspector] public int Rnd = 0;

    [HideInInspector] public int MCnt = -1;
    int a = 0;

    bool First = false;
    bool RndomONOFF = true;

    bool R1 = true;
    bool R2 = true;
    bool R3 = true;
    bool R4 = true;
    bool R5 = true;


    //ランダム関連の物
    int RStart = 1;
    int REnd = 6;
    int[] RandomArray = {0,0,0,0,0};
    int cnt = 0;

    void Start()
    {
        Debug.Log("こんばんわ");
        //RBalcony.enabled = false;
        RBalcony.SetActive(false);
        RKitchen.SetActive(false);
        RBath.SetActive(false);
        RCloset.SetActive(false);
        RBedRoom.SetActive(false);
        
    }


    // Update is called once per frame
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
            
            Debug.Log("重傷者を救った："+Radio.RPeople);
        }
    }

    public void LightR()
    {
        MCnter();
        Radio.RHintStop();
    }

    public void HeavyR()
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
        while(RndomONOFF)
        {
            if (!R1 && !R2 && !R3 && !R4 && !R5)
            {
                
                break;
            }
            else
            {
                Rnd = Random.Range(1, 6);
                Debug.Log(Rnd);


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
        int rndom = Pop.Rnd;
        Debug.Log("ポップする際の受け取り:" + rndom);

        if(rndom == 1)
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
