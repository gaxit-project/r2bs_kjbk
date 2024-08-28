using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorInjuryFixed : MonoBehaviour
{
    // プレイヤーのゲームオブジェクトを格納する
    public GameObject player;
    // プレイヤーとのオフセット（相対位置）を格納する
    private Vector3 offset;

    void Start()
    {
        // 初期のプレイヤーとのオフセットを計算し保存
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        #region プレイヤーの追従処理
        // プレイヤーが存在する場合
        if (player != null)
        {
            // プレイヤーの位置にオフセットを加えた位置にオブジェクトを移動
            transform.position = this.player.transform.position + offset;

            // オブジェクトの回転を固定する
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
        #endregion
    }
}
