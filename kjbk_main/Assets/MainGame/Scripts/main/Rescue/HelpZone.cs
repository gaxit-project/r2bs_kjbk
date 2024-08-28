using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HelpZone : MonoBehaviour
{

    #region アタッチされるオブジェクトとコンポーネントの参照
    // NPCのGameObject
    public GameObject NPC;
    // RescueNPCの参照
    public RescueNPC RescueNPC;
    #endregion

    #region 初期化
    void Start()
    {
        #region 初期設定
        // NPCに近づいていない時のテキストを設定
        RescueNPC.SetText("");
        #endregion
    }
    #endregion

    #region トリガーイベント
    // 接触判定(接触した瞬間)
    void OnTriggerStay(UnityEngine.Collider collider)
    {
        #region プレイヤーがNPCに近づいたとき
        // プレイヤーが接触し、NPCが救助されておらず、NPCが移動中でない場合
        if ((collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player")) &&
            !RescueNPC.IsItRescued() && !RescueNPC.IsItNPCrun())
        {
            // NPCが救助ゾーンに入ったことを設定
            RescueNPC.SetInZone(true);
            // 近づいたときにテキストを表示
            RescueNPC.SetText(RescueNPC.text);
        }
        #endregion

        #region NPCが救出地点にいるとき
        // NPCが救出地点にいるとき
        if (collider.gameObject.name == "RescuePoint")
        {
            // 救出地点に接触したことを設定
            RescueNPC.SetInGoal(true);
        }
        #endregion
    }

    // 接触判定(接触後離れたとき)
    void OnTriggerExit(Collider collider)
    {
        #region プレイヤーがNPCから離れたとき
        // プレイヤーが離れたとき
        if (collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player"))
        {
            // NPCが救助ゾーンから出たことを設定
            RescueNPC.SetInZone(false);
            // テキストを空に設定
            RescueNPC.SetText("");
        }
        #endregion
    }
    #endregion
}
