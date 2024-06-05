using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoWalk : MonoBehaviour
{
    //吉田スクリプト
    public Transform Target;   //ナビゲーション目的地のTransform
    private NavMeshAgent m_Agent;   //NavMeshAgent
    public NavMeshPath path;

    private GameObject surface;
    private NavMeshBuild BuildScript;

    [SerializeField] private bool Auto = true;   //救助者脱出行動のON/OFF
    [SerializeField] private bool Severe;   //重傷者判別
    [SerializeField] private float WaitSecond;

    public bool corFlag = false;   //コルーチン制御用のフラグ


    //石崎スクリプト
    //MAPの中心の位置
    public Transform central;

    private RescueNPC rescueNPC;

    //ランダムで決めるx軸の最大値
    [SerializeField] float Xradius = 10;
    //ランダムで決めるz軸の最大値
    [SerializeField] float Zradius = 10;
    //設定した待機時間
    [SerializeField] float waitTime = 5;
    //待機時間数える用
    [SerializeField] float time = 0;



    bool Encount = false;



    // Start is called before the first frame update
    void Start()
    {
        //吉田スクリプト
        m_Agent = GetComponent<NavMeshAgent>();   //NavMeshAgentの取得
        //StopAgent();   //脱出行動がOFFの場合ナビゲーションを停止

        surface = GameObject.Find("yuka");   //NavMeshSurfaceをアタッチしたオブジェクト名
        BuildScript = surface.GetComponent<NavMeshBuild>();   //NavMesh_Buildのスクリプト

        //軽症者に変化する判定
        //StartCoroutine("Distance");


        // 石崎スクリプト
        rescueNPC = GetComponent<RescueNPC>();

        //目標地点に近づいても速度を落とさない
        m_Agent.autoBraking = false;
        //目標地点を決める
        GotoNextPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //石崎スクリプト
        bool Encount = rescueNPC.IsItFirstContact();
        if (Input.GetKeyDown("y"))
        {
            Encount = true;
        }
        if (Encount)
        {
            m_Agent.ResetPath();
            Debug.Log("encount" + Encount);
            //自動脱出を開始する
            m_Agent.destination = Target.position;
            //m_Agent.isStopped = false;
            OnAuto();

            //プレイヤーの方を向く
            //var NPCpos = transform.position;
            //var Diff = (PlayerPos.potision - NPCpos);
            //transform.rotation = Quaternion.LookRotation(Diff);

        }
        //待ち時間を数える
        time += Time.deltaTime;

        //待ち時間が設定された数値を超えると発動
        if (time > waitTime && !Encount)
        {
            //目標地点を設定し直す
            GotoNextPoint();
            time = 0;
        }
    }


    //吉田スクリプト
    IEnumerator Distance()   //WaitSecondで指定している秒数
    {　　　　　　　　　　　　//同じ座標の場合ナビゲーション停止
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

    bool Compare(Vector3 pre)   //一定時間前の座標と現在座標の比較
    {
        if (Mathf.Ceil(pre.x) == Mathf.Ceil(transform.position.x) &&    //座標が同じ場合true
                Mathf.Ceil(pre.z) == Mathf.Ceil(transform.position.z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MoveAgent()   //ナビゲーションON
    {
        m_Agent.isStopped = false;
    }

    void StopAgent()   //ナビゲーションOFF
    {
        m_Agent.isStopped = true;
    }

    void OnAuto()   //脱出行動ON
    {
        Auto = true;
        if (!corFlag)
        {
            //StartCoroutine("Distance");
            corFlag = true;
        }
    }

    void OffAuto()   //脱出行動OFF
    {
        Auto = false;
    }

    /*private void OnCollisionStay(Collision collision)   //炎に触れた際重傷者化
    {
        if (collision.gameObject.name == "Blaze" && !Severe)
        {
            Debug.Log("重傷者化");
            Severe = true;
        }
    }*/

    //石崎スクリプト
    void GotoNextPoint()
    {
        //目標地点のX軸、Z軸をランダムで決める
        float posX = Random.Range(-1 * Xradius, Xradius);
        float posZ = Random.Range(-1 * Zradius, Zradius);

        //CentralPointの位置にPosXとPosZを足す
        Vector3 pos = central.position;
        pos.x += posX;
        pos.z += posZ;

        //NavMeshAgentに目標地点を設定する
        m_Agent.destination = pos;
    }
}
