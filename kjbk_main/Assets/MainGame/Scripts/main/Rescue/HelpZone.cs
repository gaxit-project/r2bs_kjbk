using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HelpZone : MonoBehaviour
{
    public GameObject NPC;   //NPC��GameObject
    public RescueNPC RescueNPC;


    void Start()
    {
        RescueNPC.SetText("");   //NPC�ɋ߂Â��Ă��Ȃ����̃e�L�X�g

    }
    //�ڐG����(�ڐG�����u��)
    void OnTriggerStay(UnityEngine.Collider collider)
    {
        if ((collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player")) && !RescueNPC.IsItRescued() && !RescueNPC.IsItNPCrun())
        {
            RescueNPC.SetInZone(true);
            RescueNPC.SetText(RescueNPC.text);   //�߂Â����Ƃ���text��\��
        }

        if (collider.gameObject.name == "RescuePoint")   //NPC���~�o�n�_�ɂ���Ƃ�
        {
            RescueNPC.SetInGoal(true);   //�~�o�n�_�ɐڐG
        }
    }

    //�ڐG����(�ڐG�㗣�ꂽ�Ƃ�)
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player"))   //Player�����ꂽ�Ƃ�
        {
            RescueNPC.SetInZone(false);
            RescueNPC.SetText("");
        }
    }
}
