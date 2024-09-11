using UnityEngine;
using UnityEngine.AI;

public class TalkAI : MonoBehaviour
{
    public static NavMeshAgent agent;
    private Renderer npcRenderer;
    public static bool hasTalked = false;
    public static bool NPCDestroy = false;

    void Start()
    {
        NPCDestroy = false;
        hasTalked = false;
        agent = GetComponent<NavMeshAgent>();
        npcRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // NPCが移動してカメラ外に出た場合に削除する
        if (hasTalked && !npcRenderer.isVisible)
        {
            // NPCがカメラ外に出たので消す
            Destroy(gameObject);
            Debug.Log("NPCを破壊！");
            NPCDestroy = true;
            hasTalked = false;
        }
    }

    // NPCに話しかけた時に呼ばれる関数
    public static void TalkToNPC()
    {
        if (!hasTalked)
        {
            // NPCに話しかけた後、ナビメッシュで目的地へ移動開始
            hasTalked = true;
            agent.SetDestination(new Vector3(0, 0, 0)); // 座標(0,0,0)へ移動させる
            Debug.Log("NPCがゴールへ移動開始！");
        }
    }
}

