using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GoalJudgement : MonoBehaviour
{
    #region �ϐ��錾
    // �o����UI�i�S�[���̃{�^���j
    [SerializeField] GameObject ExitUI;
    #endregion

    #region Start���\�b�h
    // Start�͍ŏ��̃t���[���̑O��1�x�Ăяo����܂�
    void Start()
    {
        #region UI�̏�����
        ExitUI.SetActive(false);  // �Q�[���J�n���ɏo����UI���\���ɂ���
        #endregion
    }
    #endregion

    #region �g���K�[�֘A����
    // �v���C���[���g���K�[���ɂ���Ƃ��̏���
    void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            #region UI�̕\��
            ExitUI.SetActive(true);  // �v���C���[���o���ɐG�ꂽ�Ƃ���UI��\������
            #endregion
        }
    }

    // �v���C���[���g���K�[����o���Ƃ��̏���
    void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            #region UI�̔�\��
            ExitUI.SetActive(false);  // �v���C���[���o�����痣�ꂽ�Ƃ���UI���\���ɂ���
            #endregion
        }
    }
    #endregion
}
