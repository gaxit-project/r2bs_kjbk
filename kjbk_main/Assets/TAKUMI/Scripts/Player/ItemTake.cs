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
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
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
                MMUI.MissionUpgread("���}�O�J�b�v�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[1] && Take)
            {
                //
                SMI.ItemActive(1);
                MMUI.MissionUpgread("���X�}�z�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[2] && Take)
            {
                //
                SMI.ItemActive(2);
                MMUI.MissionUpgread("���V�����v�[�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[3] && Take)
            {
                //
                SMI.ItemActive(3);
                MMUI.MissionUpgread("��T�V���c�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[4] && Take)
            {
                //
                SMI.ItemActive(4);
                MMUI.MissionUpgread("���G��������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[5] && Take)
            {
                //
                SMI.ItemActive(5);
                MMUI.MissionUpgread("���Q�[���@�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[6] && Take)
            {
                //
                SMI.ItemActive(6);
                MMUI.MissionUpgread("���ʐ^�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[7] && Take)
            {
                //
                SMI.ItemActive(7);
                MMUI.MissionUpgread("�����܂̂ʂ�����݂������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[8] && Take)
            {
                //
                SMI.ItemActive(8);
                MMUI.MissionUpgread("���Ԃ������ċA��", ItemCount, 0);
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
