using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkIcon : MonoBehaviour
{
    #region �ϐ��̐錾

    // �A�C�R���̃Q�[���I�u�W�F�N�g
    [SerializeField] GameObject PlayerIcon;  // �v���C���[�A�C�R��
    [SerializeField] GameObject NPCIcon;     // NPC�A�C�R��

    // RescueNPC�X�N���v�g�̎Q��
    public RescueNPC RescueNPC;

    #endregion

    void Update()
    {
        #region �A�C�R���̕\������

        // �A�C�R�����A�N�e�B�u�����b�N����Ă��Ȃ��ꍇ
        if (RescueNPC.IsItActiveIcon() && !RescueNPC.IsItLock())
        {
            RescueNPC.SetLock(true);     // ���b�N��ݒ�
            ActivePlayerIcon();          // �v���C���[�A�C�R����\��
            Invoke("ActiveNPCIcon", 1f); // 1�b���NPC�A�C�R����\��
            Invoke("FinishTalk", 2f);    // 2�b��Ƀg�[�N���I��
        }

        // �A�C�R�����A�N�e�B�u����x�ڂ̐ڐG�̏ꍇ
        if (RescueNPC.IsItActiveIcon() && RescueNPC.IsItSecondContact())
        {
            FinishTalk();                   // �g�[�N���I��
            RescueNPC.SetActiveIcon(false); // �A�C�R�����A�N�e�B�u�ɐݒ�
            PlayerIcon.SetActive(false);    // �v���C���[�A�C�R�����\��
            NPCIcon.SetActive(false);       // NPC�A�C�R�����\��
        }

        #endregion
    }

    #region �A�C�R���̕\���֘A�̏���

    // �v���C���[�A�C�R����\��
    private void ActivePlayerIcon()
    {
        if (!RescueNPC.IsItSecondContact())
        {
            PlayerIcon.SetActive(true);
        }
    }

    // NPC�A�C�R����\��
    private void ActiveNPCIcon()
    {
        if (!RescueNPC.IsItSecondContact())
        {
            PlayerIcon.SetActive(false);  // �v���C���[�A�C�R�����\��
            NPCIcon.SetActive(true);      // NPC�A�C�R����\��
        }
    }

    // �g�[�N���I��
    private void FinishTalk()
    {
        NPCIcon.SetActive(false);            // NPC�A�C�R�����\��
        RescueNPC.SetActiveIcon(false);      // �A�C�R�����A�N�e�B�u�ɐݒ�
        RescueNPC.SetLock(false);            // ���b�N������
    }

    #endregion
}
