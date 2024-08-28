using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger_Ray : MonoBehaviour
{
    #region 変数定義
    [SerializeField] private float reacDistance = 0.45f;  // リアクションの発生距離

    private Vector3 Xp;  // 右方向のベクトル
    private Vector3 Zp;  // 前方向のベクトル
    private Vector3 Xm;  // 左方向のベクトル
    private Vector3 Zm;  // 後方向のベクトル

    private Ray rayXp;  // 右方向のRay
    private Ray rayZp;  // 前方向のRay
    private Ray rayXm;  // 左方向のRay
    private Ray rayZm;  // 後方向のRay

    private RaycastHit XpHit;  // 右方向のヒット情報
    private RaycastHit ZpHit;  // 前方向のヒット情報
    private RaycastHit XmHit;  // 左方向のヒット情報
    private RaycastHit ZmHit;  // 後方向のヒット情報

    [System.NonSerialized] public bool Up = false;    // 前方向の危険状態
    [System.NonSerialized] public bool Under = false; // 後方向の危険状態
    [System.NonSerialized] public bool Left = false;  // 左方向の危険状態
    [System.NonSerialized] public bool Right = false; // 右方向の危険状態

    [System.NonSerialized] public float XpDistance = 100;  // 右方向の距離
    [System.NonSerialized] public float ZpDistance = 100;  // 前方向の距離
    [System.NonSerialized] public float XmDistance = 100;  // 左方向の距離
    [System.NonSerialized] public float ZmDistance = 100;  // 後方向の距離
    #endregion

    #region 初期化処理
    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理はありませんが、必要に応じてここに追加できます
    }
    #endregion

    #region Rayの設定
    // 各方向のRayの設定を行う
    private void SetRayDirections()
    {
        Xp = Vector3.right;
        Zp = Vector3.forward;
        Xm = Vector3.left;
        Zm = Vector3.back;

        Vector3 t = new Vector3(this.transform.position.x, 5f, this.transform.position.z);

        rayXp = new Ray(t, Xp);
        rayZp = new Ray(t, Zp);
        rayXm = new Ray(t, Xm);
        rayZm = new Ray(t, Zm);
    }
    #endregion

    #region Ray判定処理
    // 各方向のRayによる判定を行う
    private void CheckRaycast()
    {
        CheckRayDirection(rayXp, ref XpHit, ref XpDistance, ref Right);
        CheckRayDirection(rayZp, ref ZpHit, ref ZpDistance, ref Up);
        CheckRayDirection(rayXm, ref XmHit, ref XmDistance, ref Left);
        CheckRayDirection(rayZm, ref ZmHit, ref ZmDistance, ref Under);
    }

    // 単一の方向に対するRay判定処理
    private void CheckRayDirection(Ray ray, ref RaycastHit hit, ref float distance, ref bool directionFlag)
    {
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.CompareTag("Blaze"))
            {
                float dist = Vector3.Distance(hit.transform.position, transform.position);
                distance = dist / 100;
                directionFlag = distance <= reacDistance;
            }
            else
            {
                directionFlag = false;
            }
        }
        else
        {
            directionFlag = false;
        }
    }
    #endregion

    #region 更新処理
    // Update is called once per frame
    void Update()
    {
        SetRayDirections();  // Rayの設定
        CheckRaycast();      // Ray判定処理
    }
    #endregion
}
