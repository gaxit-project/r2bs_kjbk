using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    #region 宣言: 変数
    [SerializeField] public GameObject FF; // ゲームオブジェクト FF（使用されていない）
    private double _time; // 時間（使用されていない）
    private float _cycle = 1; // サイクルの時間（使用されていない）
    private SpriteRenderer Sr; // スプライトレンダラー
    private float Transparency = 0.0f; // 透明度
    #endregion

    #region 初期化: Startメソッド
    void Start()
    {
        // スプライトレンダラーを取得
        Sr = GetComponent<SpriteRenderer>();
    }
    #endregion

    #region 関数: Tenmetsuメソッド
    public void Tenmetsu()
    {
        // 透明度を設定する処理（ループが不要で直接設定することを推奨）
        for (int i = 0; i < 10; i++)
        {
            Sr.color = new Color(Sr.color.r, Sr.color.g, Sr.color.b, Transparency);
        }
    }
    #endregion
}
