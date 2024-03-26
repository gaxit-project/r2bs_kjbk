using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Help_Zone : MonoBehaviour
{
    public GameObject NPC;   //NPCのGameObject
    public Rescue_NPC Rescue_NPC;
    [SerializeField] public Radio_Text Radio_Text;

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
        if (collision.gameObject.name == "Player" && !Rescue_NPC.IsItFollow() && !Rescue_NPC.IsItRescued() && !Rescue_NPC.IsItNPCrun())
        {
            Rescue_NPC.SetText(Rescue_NPC.text);   //近づいたときにtextを表示
            if (Input.GetKey(KeyCode.E))   //ボタン(E)を押された時の処理
            {
                if (Rescue_NPC.Severe)   ///重傷者の区別 true = 重傷者 : false = 非重傷者
                {
                    Rescue_NPC.StopMoveNPC();
                    Rescue_NPC.SetFollow(true);   //NPCをプレイヤーの頭上に誘導する
                }
                else
                {
                    Rescue_NPC.SetActiveIcon(true);
                    Rescue_NPC.StopMoveNPC();
                    Rescue_NPC.SetNPCrun(true);   //NPCを救出地点まで誘導する
                    Rescue_NPC.WaitChange(3.5f);
                    Radio_Text.SetActiveText(true);
                }
            }
        }

        if (collision.gameObject.name == "RescuePoint")   //NPCが救出地点にいるとき
        {
            Rescue_NPC.SetFollow(false);   //追従を止める
            Rescue_NPC.SetInGoal(true);   //救出地点に接触
            Rescue_NPC.CountDestroy();   //一定時間後にオブジェクト削除
        }
    }

    //接触判定(接触後離れたとき)
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")   //Playerが離れたとき
        {
            Rescue_NPC.SetText("");
        }
    }
}
