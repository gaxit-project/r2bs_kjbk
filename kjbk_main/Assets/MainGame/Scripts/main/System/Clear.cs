using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clear : MonoBehaviour
{
    #region 変数宣言
    // MeshRendererコンポーネント
    private MeshRenderer sr;
    #endregion

    #region 初期化処理
    void Start()
    {
        #region コンポーネントの取得
        sr = GetComponent<MeshRenderer>(); // MeshRendererコンポーネントの取得
        #endregion

        #region 色の変更
        // 色を黒に近づける処理（Color32のアルファ値255で黒に）
        sr.material.color = sr.material.color - new Color32(0, 0, 0, 255);
        #endregion
    }
    #endregion
}
