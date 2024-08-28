using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HelpZone : MonoBehaviour
{

    #region �A�^�b�`�����I�u�W�F�N�g�ƃR���|�[�l���g�̎Q��
    // NPC��GameObject
    public GameObject NPC;
    // RescueNPC�̎Q��
    public RescueNPC RescueNPC;
    #endregion

    #region ������
    void Start()
    {
        #region �����ݒ�
        // NPC�ɋ߂Â��Ă��Ȃ����̃e�L�X�g��ݒ�
        RescueNPC.SetText("");
        #endregion
    }
    #endregion

    #region �g���K�[�C�x���g
    // �ڐG����(�ڐG�����u��)
    void OnTriggerStay(UnityEngine.Collider collider)
    {
        #region �v���C���[��NPC�ɋ߂Â����Ƃ�
        // �v���C���[���ڐG���ANPC���~������Ă��炸�ANPC���ړ����łȂ��ꍇ
        if ((collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player")) &&
            !RescueNPC.IsItRescued() && !RescueNPC.IsItNPCrun())
        {
            // NPC���~���]�[���ɓ��������Ƃ�ݒ�
            RescueNPC.SetInZone(true);
            // �߂Â����Ƃ��Ƀe�L�X�g��\��
            RescueNPC.SetText(RescueNPC.text);
        }
        #endregion

        #region NPC���~�o�n�_�ɂ���Ƃ�
        // NPC���~�o�n�_�ɂ���Ƃ�
        if (collider.gameObject.name == "RescuePoint")
        {
            // �~�o�n�_�ɐڐG�������Ƃ�ݒ�
            RescueNPC.SetInGoal(true);
        }
        #endregion
    }

    // �ڐG����(�ڐG�㗣�ꂽ�Ƃ�)
    void OnTriggerExit(Collider collider)
    {
        #region �v���C���[��NPC���痣�ꂽ�Ƃ�
        // �v���C���[�����ꂽ�Ƃ�
        if (collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player"))
        {
            // NPC���~���]�[������o�����Ƃ�ݒ�
            RescueNPC.SetInZone(false);
            // �e�L�X�g����ɐݒ�
            RescueNPC.SetText("");
        }
        #endregion
    }
    #endregion
}
