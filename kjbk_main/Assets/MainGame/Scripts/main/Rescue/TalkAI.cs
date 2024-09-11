using UnityEngine.AI;
using UnityEngine;

public class TalkAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool hasTalked = false;
    public static bool NPCDestroy = false;
    public Camera playerCamera;  // インスペクタで設定するカメラ
    private bool wasOnScreenOnce = false;  // 一度カメラ内に表示されたかどうか

    void Start()
    {
        NPCDestroy = false;
        hasTalked = false;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // NPCが移動してカメラ外に出た場合に削除する
        if (hasTalked && IsOffScreen(playerCamera))
        {
            // 一度カメラ内に表示されたことがあり、現在はカメラ外にいる場合に消す
            if (wasOnScreenOnce)
            {
                Debug.Log("NPCがカメラ外に出ました。削除します。");
                Destroy(gameObject);
                NPCDestroy = true;
                hasTalked = false;
            }
        }
        else if (hasTalked)
        {
            Debug.Log("NPCはまだカメラ内にいます。");
            // 一度カメラ内に入ったことを記録する
            if (IsOnScreen(playerCamera))
            {
                wasOnScreenOnce = true;
            }
        }
    }

    // カメラ外かどうかを判定する関数
    private bool IsOffScreen(Camera camera)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(transform.position);
        bool isInFrontOfCamera = screenPoint.z > 0;
        bool isInsideHorizontal = screenPoint.x > 0 && screenPoint.x < 1;
        bool isInsideVertical = screenPoint.y > 0 && screenPoint.y < 1;
        bool isOffScreen = isInFrontOfCamera && !(isInsideHorizontal && isInsideVertical);

        return isOffScreen;
    }

    // カメラ内にいるかどうかを判定する関数
    private bool IsOnScreen(Camera camera)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(transform.position);
        bool isInFrontOfCamera = screenPoint.z > 0;
        bool isInsideHorizontal = screenPoint.x > 0 && screenPoint.x < 1;
        bool isInsideVertical = screenPoint.y > 0 && screenPoint.y < 1;
        bool isOnScreen = isInFrontOfCamera && isInsideHorizontal && isInsideVertical;

        return isOnScreen;
    }

    // NPCに話しかけた時に呼ばれる関数
    public void TalkToNPC()
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
