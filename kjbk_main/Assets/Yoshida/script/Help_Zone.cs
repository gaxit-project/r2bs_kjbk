using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Help_Zone : MonoBehaviour
{
    public GameObject NPC;   //NPCのGameObject
    public Rescue_NPC NPCscript;

    // Start is called before the first frame update
    void Start()
    {
        NPCscript.SetText("");   //NPCに近づいていない時のテキスト
    }

    // Update is called once per frame
    void Update()
    {

    }

    //接触判定(接触した瞬間)
    void OnCollisionStay(UnityEngine.Collision collision)
    {
        if (collision.gameObject.name == "Player" && !NPCscript.IsItFollow() && !NPCscript.IsItRescued() && !NPCscript.IsItNPCrun())
        {
            NPCscript.SetText(NPCscript.text);   //近づいたときにtextを表示
            if (Input.GetKey(KeyCode.E))   //ボタン(E)を押された時の処理
            {
                if (NPCscript.Severe)   ///重傷者の区別 true = 重傷者 : false = 非重傷者
                {
                    NPCscript.StopMoveNPC();
                    NPCscript.SetFollow(true);   //NPCをプレイヤーの頭上に誘導する
                }
                else
                {
                    NPCscript.SetActiveIcon(true);
                    NPCscript.StopMoveNPC();
                    NPCscript.SetNPCrun(true);   //NPCを救出地点まで誘導する
                    NPCscript.WaitChange(3.5f);
                }
            }
        }

        if (collision.gameObject.name == "RescuePoint")   //NPCが救出地点にいるとき
        {
            NPCscript.SetFollow(false);   //追従を止める
            NPCscript.SetInGoal(true);   //救出地点に接触
            NPCscript.CountDestroy();   //一定時間後にオブジェクト削除
        }
    }

    //接触判定(接触後離れたとき)
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")   //Playerが離れたとき
        {
            NPCscript.SetText("");
        }
    }
}
