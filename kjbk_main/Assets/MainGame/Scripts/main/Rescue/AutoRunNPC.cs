using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoRunNPC : MonoBehaviour
{
    #region 変数宣言
    // ナビゲーション目的地のTransform
    public Transform Target;

    // NavMeshAgent
    private NavMeshAgent m_Agent;

    // NavMeshPath
    public NavMeshPath path;

    // NavMeshSurfaceをアタッチしたオブジェクト
    private GameObject surface;

    // NavMesh_Buildのスクリプト
    private NavMeshBuild BuildScript;

    // 救助者脱出行動のON/OFF
    [SerializeField] private bool AutoRun = false;

    // 重傷者判別
    [SerializeField] private bool Severe;

    // WaitSecondで指定している秒数
    [SerializeField] private float WaitSecond;

    // コルーチン制御用のフラグ
    public bool corFlag = false;
    #endregion

    #region Startメソッド
    // Startは最初のフレームの前に1度呼び出されます
    void Start()
    {
        #region オブジェクトの取得と初期化
        m_Agent = GetComponent<NavMeshAgent>();   // NavMeshAgentの取得
        m_Agent.SetDestination(Target.position);   // ナビゲーション目的地の設定

        surface = GameObject.Find("yuka");   // NavMeshSurfaceをアタッチしたオブジェクト名
        BuildScript = surface.GetComponent<NavMeshBuild>();   // NavMesh_Buildのスクリプト取得

        // 軽症者に変化する判定
        StartCoroutine("Distance");
        #endregion
    }
    #endregion

    #region Updateメソッド
    // Updateはフレームごとに1度呼び出されます
    void Update()
    {
        #region 自動移動と停止の制御
        if (AutoRun && !Severe && m_Agent.isStopped)
        {
            MoveAgent();
        }
        else if (AutoRun && Severe && !m_Agent.isStopped)
        {
            StopAgent();
            OffAuto();
        }
        #endregion
    }
    #endregion

    #region コルーチン
    // WaitSecondで指定している秒数、同じ座標の場合ナビゲーション停止
    IEnumerator Distance()
    {
        Vector3 prePosition = transform.position;
        while (true)
        {
            yield return new WaitForSeconds(WaitSecond);
            if (Compare(prePosition))
            {
                yield return new WaitForSeconds(WaitSecond);
                if (Compare(prePosition))
                {
                    if (!Severe)
                    {
                        Debug.Log("軽症者化");
                        OffAuto();
                    }
                    break;
                }
            }
            prePosition = transform.position;
        }
    }
    #endregion

    #region 関数
    // 一定時間前の座標と現在座標の比較
    bool Compare(Vector3 pre)
    {
        if (Mathf.Ceil(pre.x) == Mathf.Ceil(transform.position.x) &&
            Mathf.Ceil(pre.z) == Mathf.Ceil(transform.position.z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // ナビゲーションON
    void MoveAgent()
    {
        m_Agent.isStopped = false;
    }

    // ナビゲーションOFF
    void StopAgent()
    {
        m_Agent.isStopped = true;
    }

    // 脱出行動ON
    public void OnAuto()
    {
        AutoRun = true;
        if (!corFlag)
        {
            StartCoroutine("Distance");
            corFlag = true;
        }
    }

    // 脱出行動OFF
    void OffAuto()
    {
        AutoRun = false;
    }

    // 炎に触れた際重傷者化
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Blaze" && !Severe)
        {
            Debug.Log("重傷者化");
            Severe = true;
        }
    }
    #endregion
}
