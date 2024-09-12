using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoWalk : MonoBehaviour
{

    //aa

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

    // 検出可能な距離
    public float distance = 10f;
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
    //エンカウント用
    private bool Encount = false;

    // 5秒前の位置保存用
    private float time5;
    private Vector3 vector5;

    //NPC移動制御用フラグ
    private bool NPCflag = false;
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
        #region 速度でのアニメーション制御
        float speed  = m_Agent.velocity.magnitude;
        NPCanimator.SetFloat("NPCspeed", speed);
        #endregion

        // 5秒前の位置を保持
        time5 += Time.deltaTime;
        if (time5 > 5f)
        {
            vector5 = transform.position;
            time5 = 0;
        }

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
        // 時間を数える
        time += Time.deltaTime;

        #region Rayを飛ばしてオブジェクトを取得
        //transform取得
        Transform trans = this.transform;
        Vector3 NPCvec = trans.position;
        NPCvec.y += 5f;

        // RayはNPCの位置からとばす
        var rayStartPosition = NPCvec;
        // RayはNPCが向いてる方向にとばす
        var rayDirection = trans.forward;

        // Hitしたオブジェクト格納用
        RaycastHit raycastHit;

        // Rayを飛ばす（out raycastHit でHitしたオブジェクトを取得する
        var isHit = Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance);
        //Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);
        #endregion

        #region 検出したら
        // なにかを検出したら
        if (isHit)
        {
            // HitしたオブジェクトのTag何か判定
            #region Hitしたものが炎で、NPCがランダム移動中の時
            if (raycastHit.collider.gameObject.CompareTag("Blaze") && NPCflag == false)
            {
                //NPCフラグオン & 目的地変更
                NPCflag = true;
                m_Agent.destination = vector5;
            }
            #endregion
        }
        #endregion

        // 待ち時間が設定された数値を超えると発動
        if (time > waitTime && !Encount)
        {
            //NPCフラグオフ
            if (NPCflag)
            {
                NPCflag = false;
            }
            // 目標地点を設定し直す
            GotoNextPoint();
            // キャラ移動アニメーション
            time = 0;
        }
        #endregion
    }
    #endregion

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
    #endregion
}
