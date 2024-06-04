using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoRunNPC : MonoBehaviour
{
    public Transform Target;   //ナビゲーション目的地のTransform
    private NavMeshAgent m_Agent;   //NavMeshAgent
    public NavMeshPath path;

    private GameObject surface;
    private NavMeshBuild BuildScript;

    [SerializeField] private bool AutoRun = false;   //救助者脱出行動のON/OFF
    [SerializeField] private bool Severe;   //重傷者判別
    [SerializeField] private float WaitSecond;

    public bool corFlag = false;   //コルーチン制御用のフラグ

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();   //NavMeshAgentの取得
        m_Agent.SetDestination(Target.position);   //ナビゲーション目的地の設定
        //StopAgent();   //脱出行動がOFFの場合ナビゲーションを停止

        surface = GameObject.Find("yuka");   //NavMeshSurfaceをアタッチしたオブジェクト名
        BuildScript = surface.GetComponent<NavMeshBuild>();   //NavMesh_Buildのスクリプト

        //軽症者に変化する判定
        StartCoroutine("Distance");
    }

    // Update is called once per frame
    void Update()
    {
        if (AutoRun && !Severe && m_Agent.isStopped)
        {
            MoveAgent();
        }
        else if (AutoRun && Severe && !m_Agent.isStopped)
        {
            StopAgent();
            OffAuto();
        }
    }

    IEnumerator Distance()   //WaitSecondで指定している秒数
    {　　　　　　　　　　　　//同じ座標の場合ナビゲーション停止
        Vector3 prePosition = transform.position;
        while (true)
        {
            yield return new WaitForSeconds(WaitSecond);
            if (Compare(prePosition))
            {
                //BuildScript.Build();   //新規MeshのBake
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

    public void OnAuto()   //脱出行動ON
    {
        AutoRun = true;
        if (!corFlag)
        {
            StartCoroutine("Distance");
            corFlag = true;
        }
    }

    void OffAuto()   //脱出行動OFF
    {
        AutoRun = false;
    }

    private void OnCollisionStay(Collision collision)   //炎に触れた際重傷者化
    {
        if (collision.gameObject.name == "Blaze" && !Severe)
        {
            Debug.Log("重傷者化");
            Severe = true;
        }
    }
}
