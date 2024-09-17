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
        RescueNPC.isTalkingToNPC = false;
        #endregion
    }
    #endregion

    #region �g���K�[�C�x���g
    // �ڐG����(�ڐG�����u��)
    void OnTriggerStay(Collider collider)
    {
        // �v���C���[��NPC�ɋ߂Â����Ƃ�
        if ((collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player")) &&
            !RescueNPC.IsItRescued() && !RescueNPC.IsItNPCrun() && !RescueNPC.isTalkingToNPC) // �܂�����NPC�Ƙb���Ă��Ȃ�
        {
            if (!RescueNPC.Severe) // �y���҂��ǂ������m�F
            {
                // NPC���~���]�[���ɓ��������Ƃ�ݒ�
                RescueNPC.SetInZone(true);
                // �߂Â����Ƃ��Ƀe�L�X�g��\��
                RescueNPC.SetText(RescueNPC.text);

                // �y���҂̏ꍇ�A�v���C���[���b���������ł��邱�Ƃ������t���O���I���ɂ���
                RescueNPC.isTalkingToNPC = true;
            }
            else
            {
                // �d���҂̏ꍇ�̏����i�K�v�ɉ����Ēǉ��j
                // �d���҂ɂ͓��ʂȏ���������ꍇ�ɂ�����ɒǉ��ł��܂�
                RescueNPC.SetInZone(true); // �ڐG��Ԃɂ��邾��
            }
        }

        // NPC���~�o�n�_�ɂ���Ƃ�
        if (collider.gameObject.name == "RescuePoint")
        {
            RescueNPC.SetInGoal(true);
        }
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
            // ����NPC���v���C���[�Ƙb�����ł��邱�Ƃ�����
            RescueNPC.isTalkingToNPC = false; // �t���O�𗧂Ă�
            Debug.Log("����NPC�Ƙb����悤�ɂȂ�܂���");
        }
        #endregion
    }
    #endregion
}
