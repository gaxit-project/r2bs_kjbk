using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixed : MonoBehaviour
{
    #region 宣言: 変数
    private GameObject player; // プレイヤーのゲームオブジェクト
    private Vector3 offset; // プレイヤーとの相対位置
    #endregion

    #region 初期化: Startメソッド
    void Start()
    {
        // プレイヤーのオブジェクト情報を格納
        this.player = GameObject.Find("FF");

        // 初期のポジション保存
        offset = transform.position - player.transform.position;
    }
    #endregion

    #region 更新: Updateメソッド
    void Update()
    {
        // プレイヤーの位置に追従
        transform.position = this.player.transform.position + offset;
    }
    #endregion
}
