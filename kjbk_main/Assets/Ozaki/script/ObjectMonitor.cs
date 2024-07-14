using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMonitor : MonoBehaviour
{
    public string buttonName; // 対応するボタンの名前

    public delegate void ObjectDestroyed(string buttonName);
    public static event ObjectDestroyed OnObjectDestroyed;

    void OnDestroy()
    {
        // オブジェクトが破壊されたときにイベントを発生させる
        if (OnObjectDestroyed != null)
        {
            OnObjectDestroyed(buttonName);
        }
    }
}
