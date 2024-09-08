using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoWalk : MonoBehaviour
{

    #region アタッチされるオブジェクトとコンポーネントの参照
    // ナビゲーション目的地のTransform
    public Transform Target;
    // NavMeshAgentコンポーネント
    private NavMeshAgent m_Agent;
    // NavMeshPathの参照
    public NavMeshPath path;
    // NavMeshSurfaceをアタッチしたオブジェクト
    private GameObject surface;
    // NavMesh_Buildのスクリプト
    private NavMeshBuild BuildScript;
    // RescueNPCスクリプトの参照
    private RescueNPC rescueNPC;
    // NPCのアニメーターコンポーネント
    private Animator NPCanimator;
    #endregion

    #region 設定や状態管理の変数
    // 救助者脱出行動のON/OFF
    [SerializeField] private bool Auto = true;
    // 重傷者判別
    [SerializeField] private bool Severe;
    // コルーチン制御用のフラグ
    public bool corFlag = false;
    // 待機時間
    [SerializeField] private float WaitSecond;
    #endregion

    #region ランダム目標地点設定用の変数
    // MAPの中心の位置
    public Transform central;
    // ランダムで決めるx軸の最大値
    [SerializeField] float Xradius = 10;
    // ランダムで決めるz軸の最大値
    [SerializeField] float Zradius = 10;
    // 設定した待機時間
    [SerializeField] float waitTime = 5;
    // 待機時間数える用
    [SerializeField] float time = 0;
    #endregion

    #region その他の変数
    // 目標地点のx軸の位置
    float posX;
    // 目標地点のz軸の位置
    float posZ;
    // エンカウントのフラグ
    bool Encount = false;
    #endregion


    #region 初期化
    void Start()
    {
        #region オブジェクトとコンポーネントの取得
        // NavMeshAgentの取得
        m_Agent = GetComponent<NavMeshAgent>();
        // NavMeshSurfaceをアタッチしたオブジェクトを探す
        surface = GameObject.Find("yuka");
        // NavMesh_Buildのスクリプトを取得
        BuildScript = surface.GetComponent<NavMeshBuild>();
        // RescueNPCスクリプトの取得
        rescueNPC = GetComponent<RescueNPC>();
        // NPCのアニメーターコンポーネントの取得
        NPCanimator = this.GetComponent<Animator>();
        #endregion

        #region 初期設定
        // 目標地点に近づいても速度を落とさない
        m_Agent.autoBraking = false;
        // 目標地点を決める
        GotoNextPoint();
        #endregion
    }
    #endregion

    #region 更新処理
    void FixedUpdate()
    {
        #region エンカウント判定
        // 初接触判定
        Encount = rescueNPC.IsItFirstContact();
        if (Input.GetKeyDown("y"))
        {
            Encount = true;
        }
        if (Encount)
        {
            m_Agent.ResetPath();
            Debug.Log("encount" + Encount);
            // 自動脱出を開始する
            m_Agent.destination = Target.position;
            OnAuto();
        }
        #endregion

        #region 目標地点設定と移動
        // 待ち時間を数える
        time += Time.deltaTime;

        // 待ち時間が設定された数値を超えると発動
        if (time > waitTime && !Encount)
        {
            if (m_Agent.isStopped)
            {
                m_Agent.isStopped = false;
            }
            // 目標地点を設定し直す
            GotoNextPoint();
            // キャラ移動アニメーション
            NPCanimator.SetBool("Walk", true);
            time = 0;
        }
        if (GotoNextPointGoal())
        {
            NPCanimator.SetBool("Walk", false);
        }
        #endregion
    }
    #endregion

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blaze"))
        {
            m_Agent.isStopped = true;
            NPCanimator.SetBool("Walk", false);
        }
    }

    #region ナビゲーション制御メソッド
    // WaitSecondで指定している秒数後、同じ座標の場合ナビゲーション停止
    IEnumerator Distance()
    {
        Vector3 prePosition = transform.position;
        while (true)
        {
            yield return new WaitForSeconds(WaitSecond);
            if (Compare(prePosition))
            {
                BuildScript.Build();   //新規MeshのBake
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

    // 一定時間前の座標と現在座標の比較
    bool Compare(Vector3 pre)
    {
        if (Mathf.Ceil(pre.x) == Mathf.Ceil(transform.position.x) && Mathf.Ceil(pre.z) == Mathf.Ceil(transform.position.z))
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
    void OnAuto()
    {
        Auto = true;
        if (!corFlag)
        {
            corFlag = true;
        }
    }

    // 脱出行動OFF
    void OffAuto()
    {
        Auto = false;
    }
    #endregion

    #region 目標地点設定メソッド
    // 目標地点を設定する
    void GotoNextPoint()
    {
        // 目標地点のX軸、Z軸をランダムで決める
        posX = Random.Range(-1 * Xradius, Xradius);
        posZ = Random.Range(-1 * Zradius, Zradius);

        // CentralPointの位置にPosXとPosZを足す
        Vector3 pos = central.position;
        pos.x += posX;
        pos.z += posZ;

        // NavMeshAgentに目標地点を設定する
        m_Agent.destination = pos;
    }

    // 目標地点に到達したかの判定
    bool GotoNextPointGoal()
    {
        Vector3 NPCpos = this.transform.position;
        Vector3 pos = central.position;

        pos.x += posX;
        pos.z += posZ;

        if (NPCpos.x == pos.x && NPCpos.z == pos.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
