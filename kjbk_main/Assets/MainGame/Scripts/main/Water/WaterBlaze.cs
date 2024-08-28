using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlaze : MonoBehaviour
{
    #region フィールド宣言
    private Inferno Inferno; // Infernoコンポーネントへの参照
    #endregion

    #region 消火器のパーティクルが当たった時
    // パーティクルがオブジェクトに衝突したときの処理
    public void OnParticleCollision(GameObject obj)
    {
        // 衝突したオブジェクトが「Blaze」タグを持つ場合
        if (obj.CompareTag("Blaze"))
        {
            // Infernoコンポーネントを取得
            Inferno = obj.GetComponent<Inferno>();

            // InfernoコンポーネントのDesBlazeプロパティがtrueの場合
            if (Inferno.DesBlaze)
            {
                // 消火音を再生
                obj.GetComponent<AudioSource>().Play();

                // オブジェクトを破壊
                Destroy(obj);
            }
        }
    }
    #endregion
}
