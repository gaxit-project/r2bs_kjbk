using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GoalJudgement : MonoBehaviour
{
    #region 変数宣言
    // 出口のUI（ゴールのボタン）
    [SerializeField] GameObject ExitUI;
    #endregion

    #region Startメソッド
    // Startは最初のフレームの前に1度呼び出されます
    void Start()
    {
        #region UIの初期化
        ExitUI.SetActive(false);  // ゲーム開始時に出口のUIを非表示にする
        #endregion
    }
    #endregion

    #region トリガー関連処理
    // プレイヤーがトリガー内にいるときの処理
    void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            #region UIの表示
            ExitUI.SetActive(true);  // プレイヤーが出口に触れたときにUIを表示する
            #endregion
        }
    }

    // プレイヤーがトリガーから出たときの処理
    void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            #region UIの非表示
            ExitUI.SetActive(false);  // プレイヤーが出口から離れたときにUIを非表示にする
            #endregion
        }
    }
    #endregion
}
