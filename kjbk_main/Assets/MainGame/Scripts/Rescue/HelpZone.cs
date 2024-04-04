using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HelpZone : MonoBehaviour
{
    public GameObject NPC;   //NPC‚ÌGameObject
    public RescueNPC RescueNPC;

    void Start()
    {
        RescueNPC.SetText("");   //NPC‚É‹ß‚Ã‚¢‚Ä‚¢‚È‚¢‚ÌƒeƒLƒXƒg
    }

    //ÚG”»’è(ÚG‚µ‚½uŠÔ)
    void OnCollisionStay(UnityEngine.Collision collision)
    {
        if ((collision.gameObject.name == "Player" || collision.gameObject.CompareTag("Player")) && !RescueNPC.IsItRescued() && !RescueNPC.IsItNPCrun())
        {
            RescueNPC.SetInZone(true);
            RescueNPC.SetText(RescueNPC.text);   //‹ß‚Ã‚¢‚½‚Æ‚«‚Étext‚ğ•\¦
        }

        if (collision.gameObject.name == "RescuePoint")   //NPC‚ª‹~o’n“_‚É‚¢‚é‚Æ‚«
        {
            RescueNPC.SetInGoal(true);   //‹~o’n“_‚ÉÚG
        }
    }

    //ÚG”»’è(ÚGŒã—£‚ê‚½‚Æ‚«)
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.CompareTag("Player"))   //Player‚ª—£‚ê‚½‚Æ‚«
        {
            RescueNPC.SetInZone(false);
            RescueNPC.SetText("");
        }
    }

    //ÚG”»’è(ÚG‚µ‚½uŠÔ)
    void OnTriggerStay(UnityEngine.Collider collider)
    {
        if ((collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player")) && !RescueNPC.IsItRescued() && !RescueNPC.IsItNPCrun())
        {
            RescueNPC.SetInZone(true);
            RescueNPC.SetText(RescueNPC.text);   //‹ß‚Ã‚¢‚½‚Æ‚«‚Étext‚ğ•\¦
        }

        if (collider.gameObject.name == "RescuePoint")   //NPC‚ª‹~o’n“_‚É‚¢‚é‚Æ‚«
        {
            RescueNPC.SetInGoal(true);   //‹~o’n“_‚ÉÚG
        }
    }

    //ÚG”»’è(ÚGŒã—£‚ê‚½‚Æ‚«)
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player"))   //Player‚ª—£‚ê‚½‚Æ‚«
        {
            RescueNPC.SetInZone(false);
            RescueNPC.SetText("");
        }
    }
}
