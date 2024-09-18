using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenTheDoor : MonoBehaviour
{

    #region �ϐ��錾
    // �v���C���[��NPC���߂��ɂ��邩�ǂ����̃t���O
    private bool Near;
    private bool NPCNear;

    //�h�A�̉�]
    public OpenDoorRote DoorRote;

    // �h�A�̉�]����
    public FrontCol FrontCol;
    public BackCol BackCol;


    // �h�A���J���Ă��邩�ǂ����̃t���O
    public bool DoorOpen = false;

    // 2���h�A��
    public GameObject ConectDoor;
    public bool Conect = false;


    #endregion

    #region Start���\�b�h
    // Start�͍ŏ��̃t���[���̑O��1�x�Ăяo����܂�
    void Start()
    {
        #region �ϐ��̏�����
        Near = false;
        NPCNear = false;

        if ( ConectDoor != null )
        {
            Conect = true;
        }
        #endregion
    }
    #endregion

    void Update()
    {
        #region �h�A�̏�ԍX�V
        if (Near || NPCNear)
        {

            DoorOpen = true;
            // �h�A���v���C���[���D�悵�ĊJ����悤�ɂ���

            if (Near || (!Near && NPCNear) && DoorRote.Close()) // �v���C���[���D�悳���
            {
                DoorRote.OpenDoorTowards(FrontCol.Front, BackCol.Back, Conect);
            }
            else if (NPCNear && DoorRote.Close())
            {
                DoorRote.OpenDoorTowards(FrontCol.Front, BackCol.Back, Conect);
            }

        }
        else
        {
            DoorRote.CloseDoor(); // �h�A�����
            FrontCol.Front = false;
            BackCol.Back = false;
        }




        #endregion
    }

    #region �R���C�_�[�̃g���K�[����
    // �g���K�[�ɓ������Ƃ��̏���
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = true;  // �v���C���[���߂��ɂ���
        }

        if (col.tag == "MinorInjuries")
        {
            NPCNear = true;  // NPC���߂��ɂ���
        }

    }

    // �g���K�[����o���Ƃ��̏���
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = false;  // �v���C���[�����ꂽ
        }

        if (col.tag == "MinorInjuries")
        {
            NPCNear = false;  // NPC�����ꂽ
        }
    }
    #endregion
}
