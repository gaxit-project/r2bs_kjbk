using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadRescueNavi : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーオブジェクト")]
    private Transform player = null;

    // ターゲットのタグ
    [SerializeField, Tooltip("追いかけるターゲットのタグ")]
    private string targetTag = "SeriousInjuries";  // デフォルトでMinorInjuriesに設定

    // 現在のターゲット
    private Transform currentTarget;

    // ターゲットの目標
    [SerializeField, Tooltip("特定のゴールオブジェクト")]
    private GameObject goal;

    private RescueNPC rescueNPC;
    private RescueDiplication DiplicationScript;
    private GameObject Rescue;

    public Transform SetPlayer { get { return player; } set { player = value; } }
    public string SetTargetTag { get { return targetTag; } set { targetTag = value; } }

    void Start()
    {
        Rescue = GameObject.Find("Rescue");
        rescueNPC = FindObjectOfType<RescueNPC>();
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
    }

    void Update()
    {
        if (rescueNPC.IsItFollow() && DiplicationScript.getFlag())
        {
            //出口の方へのナビ開始
            currentTarget = goal.transform;
        }
        else
        {
            currentTarget = FindClosestTargetWithTag(targetTag);
        }

        if (currentTarget != null)
        {
            TurnAroundDirectionTarget();
        }
    }

    private void TurnAroundDirectionTarget()
    {
        // プレイヤーからターゲットまでのベクトルを計算
        Vector3 direction = (currentTarget.position - player.position).normalized;

        // Y軸の回転を計算
        float targetYRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // 現在の回転を取得
        Quaternion currentRotation = transform.rotation;

        // 新しい回転を設定（Y軸のみ変更）
        transform.rotation = Quaternion.Euler(90, targetYRotation + 90, -45);
    }

    private Transform FindClosestTargetWithTag(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = player.position;

        foreach (GameObject target in targets)
        {
            RescueNPC npc = target.GetComponent<RescueNPC>();
            if (npc != null && npc.Severe)
            {
                // ターゲットとの距離を計算
                float distance = Vector3.Distance(target.transform.position, currentPos);
                // 最も近いターゲットを更新
                if (distance < minDistance)
                {
                    closest = target;
                    minDistance = distance;
                }
            }
        }
        // 最も近いターゲットのTransformを返す
        return closest?.transform;
    }
}
