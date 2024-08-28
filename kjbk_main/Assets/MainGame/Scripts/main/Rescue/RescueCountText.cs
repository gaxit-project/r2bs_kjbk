using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RescueCountText : MonoBehaviour
{
    #region 変数の宣言
    // テキスト表示用の変数
    [SerializeField] TextMeshProUGUI RCount; // 救助者数を表示するテキスト
    [SerializeField] TextMeshProUGUI RSuccessCount; // 成功した救助者数を表示するテキスト

    // UIオブジェクトの参照
    public GameObject RsuccessCount; // 成功した救助者数表示用のオブジェクト
    public GameObject Rcount; // 救助者数表示用のオブジェクト

    // 救助者数カウント用のスクリプト参照
    public RescueCount RCounter; // RescueCountスクリプトへの参照
    RescueNPC Rcounter1 = new RescueNPC(); // RescueNPCスクリプトのインスタンス

    // 内部で使用するカウント用の変数
    int Cnt; // 実際に使用する救助者数

    // 救助者数の桁ごとに分けた数値
    int number10;
    int number1;
    #endregion

    #region 初期化処理
    private void Awake()
    {
        // PlayerPrefsから救助者数を取得
        Cnt = PlayerPrefs.GetInt("RescueCount");
    }

    void Start()
    {
        // 初期状態の設定
        RsuccessCount.SetActive(false); // 成功した救助者数表示を非表示に設定

        // テキストの色を赤に設定
        RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        // PlayerPrefsから救助者数を再取得
        Cnt = PlayerPrefs.GetInt("RescueCount");

        // テキストの初期表示を設定
        RCount.SetText("<sprite=" + Cnt + ">");
    }
    #endregion

    #region 更新処理
    void Update()
    {
        // PlayerPrefsから最新の救助者数を取得
        Cnt = PlayerPrefs.GetInt("RescueCount");

        // 救助者数が10以上の場合の表示処理
        if (Cnt >= 10)
        {
            // 救助者数を10の位と1の位に分割
            number10 = Cnt / 10 % 10;
            number1 = Cnt % 10;

            // テキスト表示の更新
            RCount.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">");

            // 表示の切り替え
            Rcount.SetActive(false);
            RsuccessCount.SetActive(true);
        }
        // 救助者数が10未満の場合の表示処理
        else
        {
            // テキストの色を赤に設定
            RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

            // テキスト表示の更新
            RCount.SetText("<sprite=" + Cnt + ">");
        }
    }
    #endregion
}
