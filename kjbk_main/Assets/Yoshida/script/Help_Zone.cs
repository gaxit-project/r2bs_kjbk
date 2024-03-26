using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Help_Zone : MonoBehaviour
{
    public GameObject NPC;   //NPC��GameObject
    public Rescue_NPC NPCscript;

    // Start is called before the first frame update
    void Start()
    {
        NPCscript.SetText("");   //NPC�ɋ߂Â��Ă��Ȃ����̃e�L�X�g
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�ڐG����(�ڐG�����u��)
    void OnCollisionStay(UnityEngine.Collision collision)
    {
        if (collision.gameObject.name == "Player" && !NPCscript.IsItFollow() && !NPCscript.IsItRescued() && !NPCscript.IsItNPCrun())
        {
            NPCscript.SetText(NPCscript.text);   //�߂Â����Ƃ���text��\��
            if (Input.GetKey(KeyCode.E))   //�{�^��(E)�������ꂽ���̏���
            {
                if (NPCscript.Severe)   ///�d���҂̋�� true = �d���� : false = ��d����
                {
                    NPCscript.StopMoveNPC();
                    NPCscript.SetFollow(true);   //NPC���v���C���[�̓���ɗU������
                }
                else
                {
                    NPCscript.SetActiveIcon(true);
                    NPCscript.StopMoveNPC();
                    NPCscript.SetNPCrun(true);   //NPC���~�o�n�_�܂ŗU������
                    NPCscript.WaitChange(3.5f);
                }
            }
        }

        if (collision.gameObject.name == "RescuePoint")   //NPC���~�o�n�_�ɂ���Ƃ�
        {
            NPCscript.SetFollow(false);   //�Ǐ]���~�߂�
            NPCscript.SetInGoal(true);   //�~�o�n�_�ɐڐG
            NPCscript.CountDestroy();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
        }
    }

    //�ڐG����(�ڐG�㗣�ꂽ�Ƃ�)
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")   //Player�����ꂽ�Ƃ�
        {
            NPCscript.SetText("");
        }
    }
}
