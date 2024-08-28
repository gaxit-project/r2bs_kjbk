using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Alpha : MonoBehaviour
{
    #region フィールド宣言
    private MeshRenderer mesh; // MeshRendererコンポーネント
    #endregion

    #region 初期化
    void Start()
    {
        // MeshRendererコンポーネントの取得
        mesh = GetComponent<MeshRenderer>();

        // 点滅コルーチンの開始
        StartCoroutine("Blink");

        // 1.5秒後にStopメソッドを呼び出す
        Invoke("Stop", 1.5f);
    }
    #endregion

    #region コルーチン
    IEnumerator Blink()
    {
        while (true)
        {
            #region 色を暗くする処理
            // 色を100回暗くする
            for (int i = 0; i < 100; i++)
            {
                mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 1);
            }

            // 0.2秒待機
            yield return new WaitForSeconds(0.2f);
            #endregion

            #region 色を明るくする処理
            // 色を100回明るくする
            for (int j = 0; j < 100; j++)
            {
                mesh.material.color = mesh.material.color + new Color32(0, 0, 0, 1);
            }

            // 0.2秒待機
            yield return new WaitForSeconds(0.2f);
            #endregion
        }
    }
    #endregion

    #region メソッド
    private void Stop()
    {
        // 点滅コルーチンを停止
        StopCoroutine("Blink");

        // ゲームオブジェクトを破壊
        Destroy(this.gameObject);
    }
    #endregion
}
