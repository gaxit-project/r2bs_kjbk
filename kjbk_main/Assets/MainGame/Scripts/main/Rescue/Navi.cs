using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navi : MonoBehaviour
{

    #region アタッチされるオブジェクトとコンポーネントの参照
    // RescueNPCスクリプトの参照
    public RescueNPC RescueNPC;
    #endregion

    #region NavMeshAgent関連の変数
    // NavMeshAgentコンポーネントの参照
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    // 救助ポイントのTransform
    [SerializeField] public Transform RescuePoint;
    #endregion


    #region 更新処理
    void Update()
    {
        #region NPCが走行中でアイコンがアクティブでない場合
        // NPCが移動中かつアイコンがアクティブでない場合、目的地を設定
        if (RescueNPC.IsItNPCrun() && !RescueNPC.IsItActiveIcon())
        {
            _navMeshAgent.destination = RescuePoint.position;
        }
        #endregion
    }
    #endregion
}
