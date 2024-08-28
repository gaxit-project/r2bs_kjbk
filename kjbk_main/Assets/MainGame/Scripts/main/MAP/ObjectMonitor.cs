using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMonitor : MonoBehaviour
{
    #region 変数の宣言
    public string buttonName; // 対応するボタンの名前
    #endregion

    #region デリゲートとイベントの宣言
    public delegate void ObjectDestroyed(string buttonName); // オブジェクトが破壊されたときに呼び出されるデリゲート
    public static event ObjectDestroyed OnObjectDestroyed; // オブジェクト破壊イベント
    #endregion

    #region Unityイベントメソッド
    void OnDestroy()
    {
        // オブジェクトが破壊されたときにイベントを発生させる
        if (OnObjectDestroyed != null)
        {
            OnObjectDestroyed(buttonName);
        }
    }
    #endregion
}
