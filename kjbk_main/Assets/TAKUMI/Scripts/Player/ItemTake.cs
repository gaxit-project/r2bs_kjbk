using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemTake : MonoBehaviour
{
    bool[] ItemFlag = new bool[9];
    int ItemCount = 3;
    bool ItemGet = false;

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
        
    }


    void Update()
    {
        bool Take = TakeAction.triggered;
        if(ItemGet)
        {
            if (ItemFlag[0] && Take)
            {
                //
                SMI.ItemActive(0);
                MMUI.MissionUpgread("■マグカップを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[1] && Take)
            {
                //
                SMI.ItemActive(1);
                MMUI.MissionUpgread("■スマホを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[2] && Take)
            {
                //
                SMI.ItemActive(2);
                MMUI.MissionUpgread("■シャンプーを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[3] && Take)
            {
                //
                SMI.ItemActive(3);
                MMUI.MissionUpgread("■Tシャツを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[4] && Take)
            {
                //
                SMI.ItemActive(4);
                MMUI.MissionUpgread("■絵画を持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[5] && Take)
            {
                //
                SMI.ItemActive(5);
                MMUI.MissionUpgread("■ゲーム機を持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[6] && Take)
            {
                //
                SMI.ItemActive(6);
                MMUI.MissionUpgread("■写真を持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[7] && Take)
            {
                //
                SMI.ItemActive(7);
                MMUI.MissionUpgread("■くまのぬいぐるみを持って帰る", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[8] && Take)
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
        }
    }
    public void ItemSet(int num)
    {
        if(0 <= num && num <= 9)
        {
            ItemFlag[num] = true;
            SMI.ShaderOn(num);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            ItemGet = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            ItemGet = false;
        }
    }
}
