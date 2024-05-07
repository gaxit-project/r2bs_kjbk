using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Help_Zone : MonoBehaviour
{
    public GameObject NPC;   //NPCのGameObject
    public Rescue_NPC Rescue_NPC;

    private GameObject RescueDiplication;
    Rescue_Diplication DiplicationScript;

    // Start is called before the first frame update
    void Start()
    {
        Rescue_NPC.SetText("");   //NPCに近づいていない時のテキスト
        RescueDiplication = GameObject.Find("RescueDiplication");
        DiplicationScript = RescueDiplication.GetComponent<Rescue_Diplication>();
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
            if (!DiplicationScript.getFlag())
            {
                Rescue_NPC.SetInZone(true);
                Rescue_NPC.SetText(Rescue_NPC.text);   //近づいたときにtextを表示
            }
            else
            {
                Rescue_NPC.SetText("");
            }
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
