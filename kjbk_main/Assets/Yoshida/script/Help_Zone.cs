using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Help_Zone : MonoBehaviour
{
    public GameObject NPC;   //NPCのGameObject
    public Rescue_NPC Rescue_NPC;

    // Start is called before the first frame update
    void Start()
    {
        Rescue_NPC.SetText("");   //NPCに近づいていない時のテキスト
    }

    // Update is called once per frame
    void Update()
    {

    }

    //接触判定(接触した瞬間)
    void OnCollisionStay(UnityEngine.Collision collision)
    {
        if (collision.gameObject.name == "Player" && !Rescue_NPC.IsItRescued() && !Rescue_NPC.IsItNPCrun())
        {
            Rescue_NPC.SetInZone(true);
            Rescue_NPC.SetText(Rescue_NPC.text);   //近づいたときにtextを表示
        }

        if (collision.gameObject.name == "RescuePoint")   //NPCが救出地点にいるとき
        {
            Rescue_NPC.SetInGoal(true);   //救出地点に接触
        }
    }

    //接触判定(接触後離れたとき)
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")   //Playerが離れたとき
        {
            Rescue_NPC.SetInZone(false);
            Rescue_NPC.SetText("");
        }
    }
}
