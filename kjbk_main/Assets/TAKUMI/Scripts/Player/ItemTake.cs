using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemTake : MonoBehaviour
{
    bool[] ItemFlag = new bool[9];
    bool[] GetItem = new bool[9];
    public static int ItemCount = 3;
    bool ItemGetFlag = false;

    public SubMissionItem SMI;

    public MissionMapUI MMUI;


    private InputAction TakeAction;
    void Start()
    {
        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        TakeAction = actionMap["Take"];


        FlagReset();

        ItemCount = 3;
        
    }


    void Update()
    {

        //デバッグ
        if (Input.GetKeyDown("k"))
        {
            int rnd = Random.Range(0, 9);
            ItemSet(rnd);
        }





        bool Take = TakeAction.triggered;
        if(ItemGetFlag)
        {
            if (ItemFlag[0] && Take && GetItem[0])
            {
                //
                SMI.ItemActive(0);
                MMUI.MissionUpgread("■マグカップを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[1] && Take && GetItem[1])
            {
                //
                SMI.ItemActive(1);
                MMUI.MissionUpgread("■スマホを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[2] && Take && GetItem[2])
            {
                //
                SMI.ItemActive(2);
                MMUI.MissionUpgread("■シャンプーを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[3] && Take && GetItem[3])
            {
                //
                SMI.ItemActive(3);
                MMUI.MissionUpgread("■Tシャツを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[4] && Take && GetItem[4])
            {
                //
                SMI.ItemActive(4);
                MMUI.MissionUpgread("■絵画を持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[5] && Take && GetItem[5])
            {
                //
                SMI.ItemActive(5);
                MMUI.MissionUpgread("■ゲーム機を持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[6] && Take && GetItem[6])
            {
                //
                SMI.ItemActive(6);
                MMUI.MissionUpgread("■写真を持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[7] && Take && GetItem[7])
            {
                //
                SMI.ItemActive(7);
                MMUI.MissionUpgread("■くまのぬいぐるみを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[8] && Take && GetItem[8])
            {
                //
                SMI.ItemActive(8);
                MMUI.MissionUpgread("■花を持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }

        }
    }

    void FlagReset()
    {
        for (int i = 0; i < ItemFlag.Length; i++)
        {
            ItemFlag[i] = false;
            GetItem[i] = false;
        }
    }
    public void ItemSet(int num)
    {
        if(0 <= num && num <= 9)
        {
            ItemFlag[num] = true;
            SMI.ShaderOn(num);
            SMI.ItemButtonActive(num);
            Debug.Log("アイテムセット　" + num);
        }
    }

    public void ItemFlagSet(bool Flag, int num)
    {
        ItemGetFlag = Flag;
        GetItem[num] = true;
    }


}
