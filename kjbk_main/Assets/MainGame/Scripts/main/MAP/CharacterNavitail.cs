using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavitail : MonoBehaviour
{
    #region 宣言
    // Rendererコンポーネントを格納する変数
    public Renderer objRenderer;

    // SwitchCameraスクリプトを格納する変数
    private SwitchCamera switchCamera;
    #endregion

    void Start()
    {
        #region オブジェクトの取得
        // Rendererコンポーネントを取得
        objRenderer = GetComponent<Renderer>();

        // SwitchCameraスクリプトをシーンから取得
        switchCamera = FindObjectOfType<SwitchCamera>();
        #endregion

        #region 初期化
        // オブジェクトを非表示にする
        objRenderer.enabled = false;

        // 子オブジェクトのRendererコンポーネントも非表示にする
        SetChildrenRenderersEnabled(false);
        #endregion
    }

    void Update()
    {
        #region レンダラーの状態を切り替える
        // map_statusがfalseならオブジェクトとその子オブジェクトを非表示にする
        if (!switchCamera.map_status)
        {
            objRenderer.enabled = false;
            SetChildrenRenderersEnabled(false);
        }
        #endregion
    }

    void OnTriggerEnter(Collider other)
    {
        #region NaviSystemと接触したときの処理
        // "NaviSystem"タグのオブジェクトに触れた場合、子オブジェクトを表示し、このオブジェクトを非表示にする
        if (other.CompareTag("NaviSystem"))
        {
            SetChildrenRenderersEnabled(true);
            objRenderer.enabled = false;
        }
        #endregion

        #region Playerと接触したときの処理
        // "Player"タグのオブジェクトに触れた場合、オブジェクトとその子オブジェクトを非表示にする
        if (other.CompareTag("Player"))
        {
            objRenderer.enabled = false;
            SetChildrenRenderersEnabled(false);
        }
        #endregion
    }

    #region レンダラーを無効化するメソッド
    public void DisableRenderers()
    {
        objRenderer.enabled = false;
        SetChildrenRenderersEnabled(false);
    }
    #endregion

    #region 子オブジェクトのレンダラーを切り替えるメソッド
    // 子オブジェクトのRendererコンポーネントの有効/無効を切り替える
    private void SetChildrenRenderersEnabled(bool isEnabled)
    {
        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = isEnabled;
        }
    }
    #endregion
}
