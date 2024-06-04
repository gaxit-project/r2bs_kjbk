using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePOP : MonoBehaviour
{
    // Start is called before the first frame update

    public CollRadio Radio;
    public RescuePOP Pop;

    [SerializeField] GameObject RBalcony;
    [SerializeField] GameObject RKitchen;
    [SerializeField] GameObject RBath;
    [SerializeField] GameObject RCloset;
    [SerializeField] GameObject RBedRoom;

    [HideInInspector] public int Rnd = 0;

    [HideInInspector] public int MCnt = -1;
    int a = 0;

    bool First = false;

    void Start()
    {
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
            Radio.RHint();
        }
        //重傷者を救ったことにする開発キー
        if (Input.GetKeyDown(KeyCode.C))
        {
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
    public int MCnter()
    {
        MCnt++;
        Debug.Log("軽症者の救助人数:" + MCnt);
        PlayerPrefs.SetInt("軽症者の救助人数", MCnt);
        return MCnt;
    }


    public int Rndom() //ランダムの数を入れる関数
    {
        Rnd = Random.Range(1, 6);   //1～5までの数をランダムに入れる
        Debug.Log("ランダムに入れた数:" + Rnd);
        PlayerPrefs.SetInt("重傷者番号", Rnd);
        return Rnd;
    }
    //重傷者を救ったらこれを起動する
    public void Rpop()
    {
        int rndom = PlayerPrefs.GetInt("重傷者番号");

        if (Rnd == 1)
        {
            RBalcony.SetActive(true);
        }
        else if (Rnd == 2)
        {
            RKitchen.SetActive(true);
        }
        else if (Rnd == 3)
        {
            RBath.SetActive(true);
        }
        else if (Rnd == 4)
        {
            RCloset.SetActive(true);
        }
        else if (Rnd == 5)
        {
            RBedRoom.SetActive(true);
        }
    }
}
