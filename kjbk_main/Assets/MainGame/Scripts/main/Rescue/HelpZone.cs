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
        RescueNPC.isTalkingToNPC = false;
        #endregion
    }
    #endregion

    #region トリガーイベント
    // 接触判定(接触した瞬間)
    void OnTriggerStay(Collider collider)
    {
        // プレイヤーがNPCに近づいたとき
        if ((collider.gameObject.name == "Player" || collider.gameObject.CompareTag("Player")) &&
            !RescueNPC.IsItRescued() && !RescueNPC.IsItNPCrun() && !RescueNPC.isTalkingToNPC) // まだ他のNPCと話していない
        {
            if (!RescueNPC.Severe) // 軽傷者かどうかを確認
            {
                // NPCが救助ゾーンに入ったことを設定
                RescueNPC.SetInZone(true);
                // 近づいたときにテキストを表示
                RescueNPC.SetText(RescueNPC.text);

                // 軽傷者の場合、プレイヤーが話しかけ中であることを示すフラグをオンにする
                RescueNPC.isTalkingToNPC = true;
            }
            else
            {
                // 重傷者の場合の処理（必要に応じて追加）
                // 重傷者には特別な処理がある場合にこちらに追加できます
                RescueNPC.SetInZone(true); // 接触状態にするだけ
            }
        }

        // NPCが救出地点にいるとき
        if (collider.gameObject.name == "RescuePoint")
        {
            RescueNPC.SetInGoal(true);
        }
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
            // このNPCがプレイヤーと話し中であることを示す
            RescueNPC.isTalkingToNPC = false; // フラグを立てる
            Debug.Log("他のNPCと話せるようになりました");
        }
        #endregion
    }
    #endregion
}
