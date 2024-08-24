using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NavMeshAgent使うときに必要
using UnityEngine.AI;

public class testWalk : MonoBehaviour
{
    //MAPの中心の位置
    public Transform central;

    private NavMeshAgent agent;
    private RescueNPC rescueNPC;

    //ランダムで決めるx軸の最大値
    [SerializeField] float Xradius = 10;
    //ランダムで決めるz軸の最大値
    [SerializeField] float Zradius = 10;
    //設定した待機時間
    [SerializeField] float waitTime = 5;
    //待機時間数える用
    [SerializeField] float time = 0;

    //Vector3 pos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rescueNPC = GetComponent<RescueNPC>();

        //目標地点に近づいても速度を落とさない
        agent.autoBraking = false;
        //目標地点を決める
        GotoNextPoint();
    }

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
        agent.destination = pos;
    }
    
    void Update()
    {
        //待ち時間を数える
        time += Time.deltaTime;

        //待ち時間が設定された数値を超えると発動
        if (time > waitTime)
        {
            //目標地点を設定し直す
            GotoNextPoint();
            time = 0;
        }
    }
}
