using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManage : MonoBehaviour
{
    #region フィールド宣言
    [SerializeField] private GameObject Water; // 水オブジェクトへの参照
    #endregion

    #region 初期化
    void Start()
    {
        // 水オブジェクトを非表示にする
        Water.SetActive(false);
    }
    #endregion

    #region 更新処理
    void Update()
    {
        // PlayerRayCastのHosuStatusがtrueの場合
        if (PlayerRayCast.HosuStatus == true)
        {
            // 水オブジェクトを表示する
            Water.SetActive(true);
        }
    }
    #endregion
}
