using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    #region �ϐ��錾
    // �v���C���[��NPC���߂��ɂ��邩�ǂ����̃t���O
    private bool Near;
    private bool NPCNear;

    // �h�A�̃A�j���[�^�[
    private Animator animator;

    // �h�A�̃R���C�_�[
    public BoxCollider doorCollider;

    // ���̓A�N�V����
    private InputAction TakeAction;

    // �h�A���J���Ă��邩�ǂ����̃t���O
    bool DoorOpen = false;
    #endregion

    #region Start���\�b�h
    // Start�͍ŏ��̃t���[���̑O��1�x�Ăяo����܂�
    void Start()
    {
        #region �ϐ��̏�����
        Near = false;
        NPCNear = false;
        animator = GetComponentInChildren<Animator>();  // �A�j���[�^�[�̎擾
        //doorCollider = GetComponentInChildren<BoxCollider>(); // �R���C�_�[�̎擾�i�R�����g�A�E�g�j

        var pInput = GetComponent<PlayerInput>();  // PlayerInput�̎擾

        // ���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        // �A�N�V�����}�b�v����A�N�V�������擾
        TakeAction = actionMap["Take"];
        #endregion
    }
    #endregion

    #region Update���\�b�h
    void Update()
    {
        #region ���̓A�N�V�����̎擾
        bool Take = TakeAction.triggered;
        #endregion

        #region �h�A�̏�ԍX�V
        if (Near || NPCNear)
        {
            doorCollider.enabled = false;  // �h�A�̃R���C�_�[�𖳌���
            animator.SetBool("Open", true);  // �h�A���J����A�j���[�V�������Đ�
            //Audio.Instance.PlaySound(0); // ���̍Đ��i�R�����g�A�E�g�j
        }
        else
        {
            animator.SetBool("Open", false);  // �h�A�����A�j���[�V�������Đ�
            //Audio.Instance.PlaySound(1); // ���̍Đ��i�R�����g�A�E�g�j
        }
        #endregion
    }
    #endregion

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
            animator.SetBool("Open", false);  // �h�A�����A�j���[�V�������Đ�
            //Audio.Instance.PlaySound(1); // ���̍Đ��i�R�����g�A�E�g�j
            doorCollider.enabled = true;  // �h�A�̃R���C�_�[��L����
        }

        if (col.tag == "MinorInjuries")
        {
            NPCNear = false;  // NPC�����ꂽ
            animator.SetBool("Open", false);  // �h�A�����A�j���[�V�������Đ�
            doorCollider.enabled = true;  // �h�A�̃R���C�_�[��L����
            //Audio.Instance.PlaySound(1); // ���̍Đ��i�R�����g�A�E�g�j
        }
    }
    #endregion

    #region �h�A�̊J����
    // �h�A�̊J����
    void DoorOpenONOFF()
    {
        animator.SetBool("Open", false);  // �h�A�����A�j���[�V�������Đ�
        doorCollider.enabled = true;  // �h�A�̃R���C�_�[��L����
        DoorOpen = false;  // �h�A�����Ă���t���O
    }
    #endregion
}
