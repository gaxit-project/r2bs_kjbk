using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadRescueNavi : MonoBehaviour
{
    #region 宣言: 変数
    [SerializeField, Tooltip("プレイヤーオブジェクト")]
    private Transform player = null; // プレイヤーのTransform

    [SerializeField, Tooltip("追いかけるターゲットのタグ")]
    private string targetTag = "SeriousInjuries"; // 追いかけるターゲットのタグ

    private Transform currentTarget; // 現在のターゲットのTransform

    [SerializeField, Tooltip("特定のゴールオブジェクト")]
    private GameObject goal; // ゴールオブジェクト

    private RescueNPC rescueNPC; // RescueNPCスクリプトのインスタンス
    private RescueDiplication DiplicationScript; // RescueDiplicationスクリプトのインスタンス
    private GameObject Rescue; // Rescueオブジェクト
    #endregion

    #region プロパティ
    public Transform SetPlayer
    {
        get { return player; }
        set { player = value; }
    }

    public string SetTargetTag
    {
        get { return targetTag; }
        set { targetTag = value; }
    }
    #endregion

    #region 初期化: Startメソッド
    void Start()
    {
        // Rescueオブジェクトの取得
        Rescue = GameObject.Find("Rescue");

        // RescueNPCスクリプトの取得
        rescueNPC = FindObjectOfType<RescueNPC>();

        // RescueDiplicationスクリプトの取得
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
    }
    #endregion

    #region 更新: Updateメソッド
    void Update()
    {
        // ナビゲーションターゲットの設定
        if (rescueNPC.IsItFollow() && DiplicationScript.getFlag())
        {
            // 出口の方へのナビゲーション開始
            currentTarget = goal.transform;
        }
        else
        {
            // ターゲットのタグに基づいて最も近いターゲットを探す
            currentTarget = FindClosestTargetWithTag(targetTag);
        }

        // 現在のターゲットが設定されていれば、ターゲットに向けて回転する
        if (currentTarget != null)
        {
            TurnAroundDirectionTarget();
        }
    }
    #endregion

    #region ナビゲーションターゲットに向けて回転
    private void TurnAroundDirectionTarget()
    {
        // プレイヤーからターゲットまでのベクトルを計算
        Vector3 direction = (currentTarget.position - player.position).normalized;

        // Y軸の回転角度を計算
        float targetYRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // 新しい回転を設定（Y軸のみ変更）
        transform.rotation = Quaternion.Euler(90, targetYRotation + 90, -45);
    }
    #endregion

    #region 最も近いターゲットをタグで検索
    private Transform FindClosestTargetWithTag(string tag)
    {
        // 指定したタグを持つ全てのターゲットオブジェクトを取得
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null; // 最も近いターゲットオブジェクト
        float minDistance = Mathf.Infinity; // 最小距離の初期値
        Vector3 currentPos = player.position; // プレイヤーの現在位置

        foreach (GameObject target in targets)
        {
            // RescueNPCスクリプトを取得
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
    #endregion
}
