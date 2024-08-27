using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HelpZone : MonoBehaviour
{
    public GameObject NPC;   //NPCのGameObject
    public RescueNPC RescueNPC;


    void Start()
    {
        RescueNPC.SetText("");   //NPCに近づいていない時のテキスト

    }
    //接触判定(接触した瞬間)
    void OnTriggerStay(UnityEngine.Collider collider)
    {
        if ((collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player")) && !RescueNPC.IsItRescued() && !RescueNPC.IsItNPCrun())
        {
            RescueNPC.SetInZone(true);
            RescueNPC.SetText(RescueNPC.text);   //近づいたときにtextを表示
        }

        if (collider.gameObject.name == "RescuePoint")   //NPCが救出地点にいるとき
        {
            RescueNPC.SetInGoal(true);   //救出地点に接触
        }
    }

    //接触判定(接触後離れたとき)
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player"))   //Playerが離れたとき
        {
            RescueNPC.SetInZone(false);
            RescueNPC.SetText("");
        }
    }
}
