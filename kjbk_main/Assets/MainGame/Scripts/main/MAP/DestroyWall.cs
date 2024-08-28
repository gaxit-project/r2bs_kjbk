using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    #region 変数宣言
    [SerializeField, Tooltip("Camera")]
    private Camera targetCamera;
    [SerializeField, Tooltip("Renderer")]
    private Renderer objectRenderer;

    private bool hasBeenOnScreen = false;
    #endregion

    void Start()
    {
        #region オブジェクトの取得
        objectRenderer = GetComponent<Renderer>();
        #endregion
    }

    void Update()
    {
        #region カメラとレンダラーの確認
        if (targetCamera != null && objectRenderer != null)
        {
            #region 画面上のオブジェクト判定
            Vector3 screenPoint = targetCamera.WorldToViewportPoint(transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            if (onScreen)
            {
                hasBeenOnScreen = true;
            }
            #endregion

            #region オブジェクトの表示/非表示
            if (hasBeenOnScreen)
            {
                objectRenderer.enabled = true;
            }
            else
            {
                objectRenderer.enabled = false;
            }
            #endregion
        }
        #endregion
    }
}
