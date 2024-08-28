using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueDiplication : MonoBehaviour
{
    #region 変数の宣言
    // フラグの状態を保持する変数
    public bool diplication; // 重複状態のフラグ
    public static RescueDiplication instance; // シングルトンパターンのためのインスタンス
    #endregion

    #region 初期化処理
    void Start()
    {
        // 初期状態を設定
        diplication = false; // 初期状態でフラグをオフに設定
    }
    #endregion

    #region フラグ制御メソッド
    // フラグの状態を取得するメソッド
    public bool getFlag()
    {
        return diplication; // フラグの状態を返す
    }

    // フラグをオンにするメソッド
    public void OnFlag()
    {
        diplication = true; // フラグをオンに設定
    }

    // フラグをオフにするメソッド
    public void OffFlag()
    {
        diplication = false; // フラグをオフに設定
    }
    #endregion
}
