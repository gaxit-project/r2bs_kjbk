using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Help_Zone : MonoBehaviour
{
    public GameObject NPC;   //NPC��GameObject
    public Rescue_NPC Rescue_NPC;
    [SerializeField] public Radio_Text Radio_Text;

    // Start is called before the first frame update
    void Start()
    {
        Rescue_NPC.SetText("");   //NPC�ɋ߂Â��Ă��Ȃ����̃e�L�X�g
    }

    // Update is called once per frame
    void Update()
    {

    }

    //�ڐG����(�ڐG�����u��)
    void OnCollisionStay(UnityEngine.Collision collision)
    {
        if (collision.gameObject.name == "Player" && !Rescue_NPC.IsItFollow() && !Rescue_NPC.IsItRescued() && !Rescue_NPC.IsItNPCrun())
        {
            Rescue_NPC.SetText(Rescue_NPC.text);   //�߂Â����Ƃ���text��\��
            if (Input.GetKey(KeyCode.E))   //�{�^��(E)�������ꂽ���̏���
            {
                if (Rescue_NPC.Severe)   ///�d���҂̋�� true = �d���� : false = ��d����
                {
                    Rescue_NPC.StopMoveNPC();
                    Rescue_NPC.SetFollow(true);   //NPC���v���C���[�̓���ɗU������
                }
                else
                {
                    Rescue_NPC.SetActiveIcon(true);
                    Rescue_NPC.StopMoveNPC();
                    Rescue_NPC.SetNPCrun(true);   //NPC���~�o�n�_�܂ŗU������
                    Rescue_NPC.WaitChange(3.5f);
                    Radio_Text.SetActiveText(true);
                }
            }
        }

        if (collision.gameObject.name == "RescuePoint")   //NPC���~�o�n�_�ɂ���Ƃ�
        {
            Rescue_NPC.SetFollow(false);   //�Ǐ]���~�߂�
            Rescue_NPC.SetInGoal(true);   //�~�o�n�_�ɐڐG
            Rescue_NPC.CountDestroy();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
        }
    }

    //�ڐG����(�ڐG�㗣�ꂽ�Ƃ�)
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")   //Player�����ꂽ�Ƃ�
        {
            Rescue_NPC.SetText("");
        }
    }
}
