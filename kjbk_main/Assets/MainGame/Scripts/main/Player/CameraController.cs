using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region 宣言: 変数
    [SerializeField] private GameObject playerObj; // キャラクターオブジェクト
    private Vector3 offset; // カメラとの距離
    #endregion

    #region 初期化: Startメソッド
    void Start()
    {
        // タグ "Player" を持つゲームオブジェクトを取得
        playerObj = GameObject.FindWithTag("Player");

        // カメラの初期位置とプレイヤーの位置の差を計算
        if (playerObj != null)
        {
            offset = transform.position - playerObj.transform.position;
        }
    }
    #endregion

    #region 更新: LateUpdateメソッド
    void LateUpdate()
    {
        // プレイヤーの位置に基づいてカメラの位置を更新
        if (playerObj != null)
        {
            transform.position = playerObj.transform.position + offset;
        }
    }
    #endregion
}
