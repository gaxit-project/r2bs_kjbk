using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUp : MonoBehaviour
{
    #region 変数定義
    [SerializeField, Tooltip("矢印オブジェクト")]
    private GameObject Arrow;  // 矢印のGameObject

    [SerializeField, Tooltip("軽症者助けた数")]
    private int num = 1;  // 助けた軽症者の数

    [SerializeField, Tooltip("人数最大値")]
    private int maxnum;  // 最大人数

    private RescueCount CounterScript;  // RescueCountスクリプトへの参照
    private CharacterNavigation CharacterNavigation;  // CharacterNavigationスクリプトへの参照
    public RescuePOP RPOP;  // RescuePOPスクリプトへの参照
    #endregion

    #region 初期化処理
    void Start()
    {
        // RescueCountスクリプトとCharacterNavigationスクリプトの取得
        CounterScript = FindObjectOfType<RescueCount>();
        CharacterNavigation = FindObjectOfType<CharacterNavigation>();
    }
    #endregion

    #region 更新処理
    void Update()
    {
        #region 矢印の表示状態更新
        // ナビゲーションが開始中またはRPOPの矢印フラグがオンの場合、矢印を表示
        if (RPOP.ArrowONFlag || CharacterNavigation.NaviUp)
        {
            Arrow.SetActive(true);
        }
        // ナビゲーションが停止中またはRPOPの矢印フラグがオフの場合、矢印を非表示
        else if (!RPOP.ArrowONFlag || !CharacterNavigation.NaviUp)
        {
            Arrow.SetActive(false);
        }

        // 助けた軽症者の数が30に達した場合、矢印を非表示
        if (CounterScript.getNum() == 30)
        {
            Arrow.SetActive(false);
        }
        #endregion
    }
    #endregion
}
