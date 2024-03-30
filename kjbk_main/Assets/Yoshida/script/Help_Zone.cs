using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Help_Zone : MonoBehaviour
{
    public GameObject NPC;   //NPC��GameObject
    public Rescue_NPC Rescue_NPC;

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
        if (collision.gameObject.name == "Player" && !Rescue_NPC.IsItRescued() && !Rescue_NPC.IsItNPCrun())
        {
            Rescue_NPC.SetInZone(true);
            Rescue_NPC.SetText(Rescue_NPC.text);   //�߂Â����Ƃ���text��\��
        }

        if (collision.gameObject.name == "RescuePoint")   //NPC���~�o�n�_�ɂ���Ƃ�
        {
            Rescue_NPC.SetInGoal(true);   //�~�o�n�_�ɐڐG
        }
    }

    //�ڐG����(�ڐG�㗣�ꂽ�Ƃ�)
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")   //Player�����ꂽ�Ƃ�
        {
            Rescue_NPC.SetInZone(false);
            Rescue_NPC.SetText("");
        }
    }
}
