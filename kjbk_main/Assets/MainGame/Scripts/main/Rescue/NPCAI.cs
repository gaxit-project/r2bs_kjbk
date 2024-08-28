using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{

    #region 状態フラグ
    // 重傷者であるかどうかを判別するフラグ
    [SerializeField] public bool Severe = false;
    // NPCがインタラクト中かどうかを示すフラグ
    [SerializeField] bool interact = false;
    #endregion

    #region 移動関連の変数
    // NPCの移動速度
    private float MoveSpeed = 10.0f;
    #endregion


    #region NPCの動作制御

    // NPCを移動状態にする関数
    public void MoveNPC()
    {
        interact = true;
    }
    #endregion

    #region 救助地点に触れた時
    // 衝突判定処理
    void OnCollisionStay(UnityEngine.Collision collision)
    {
        // NPCが救助地点に到達した場合の処理
        if (collision.gameObject.name == "RescuePoint")
        {
            interact = false;
        }
    }
    #endregion

}
