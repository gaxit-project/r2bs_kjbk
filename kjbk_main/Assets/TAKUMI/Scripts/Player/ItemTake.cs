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
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        TakeAction = actionMap["Take"];


        FlagReset();

        ItemCount = 3;
        
    }


    void Update()
    {

        //�f�o�b�O
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
                MMUI.MissionUpgread("���}�O�J�b�v�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[1] && Take && GetItem[1])
            {
                //
                SMI.ItemActive(1);
                MMUI.MissionUpgread("���X�}�z�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[2] && Take && GetItem[2])
            {
                //
                SMI.ItemActive(2);
                MMUI.MissionUpgread("���V�����v�[�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[3] && Take && GetItem[3])
            {
                //
                SMI.ItemActive(3);
                MMUI.MissionUpgread("��T�V���c�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[4] && Take && GetItem[4])
            {
                //
                SMI.ItemActive(4);
                MMUI.MissionUpgread("���G��������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[5] && Take && GetItem[5])
            {
                //
                SMI.ItemActive(5);
                MMUI.MissionUpgread("���Q�[���@�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[6] && Take && GetItem[6])
            {
                //
                SMI.ItemActive(6);
                MMUI.MissionUpgread("���ʐ^�������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[7] && Take && GetItem[7])
            {
                //
                SMI.ItemActive(7);
                MMUI.MissionUpgread("�����܂̂ʂ�����݂������ċA��", ItemCount, 0);
                ItemCount--;
                FlagReset();
            }
            else if (ItemFlag[8] && Take && GetItem[8])
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
            Debug.Log("�A�C�e���Z�b�g�@" + num);
        }
    }

    public void ItemFlagSet(bool Flag, int num)
    {
        ItemGetFlag = Flag;
        GetItem[num] = true;
    }


}
