using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManage : MonoBehaviour
{
    #region 宣言
    // 自身のRectTransformコンポーネント
    private RectTransform MyRectTfm;
    #endregion

    #region 初期化
    // Startは最初のフレーム更新時に呼ばれます
    void Start()
    {
        // 自身のRectTransformコンポーネントを取得
        MyRectTfm = GetComponent<RectTransform>();
    }
    #endregion

    #region 更新処理
    // Updateは毎フレーム呼ばれます
    void Update()
    {
        // 自身の向きをカメラに向ける
        MyRectTfm.LookAt(Camera.main.transform);
    }
    #endregion
}
