using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioText : MonoBehaviour
{
    #region 宣言
    // 表示されるテキスト
    [SerializeField] private string text;

    // 表示されるTextMeshProUGUIコンポーネント
    [SerializeField] private TextMeshProUGUI TMP;

    // 救助時に表示する画像
    public GameObject Rescued;
    public GameObject Rescued2;
    public GameObject Rescued3;

    // テキストの表示状態
    private bool ActiveText = false;

    // 救助フラグ
    public static bool RescueFlag = false;

    // ランダムな画像選択用変数
    private int Tmp = 0;
    #endregion

    #region 初期化
    private void Start()
    {
        // 救助時の画像を初期状態で非表示にする
        Rescued.SetActive(false);
        Rescued2.SetActive(false);
        Rescued3.SetActive(false);
    }
    #endregion

    #region 更新処理
    private void Update()
    {
        // 救助フラグが立っているとき
        if (RescueFlag)
        {
            // 1秒後にラジオアクティブ化、2秒後に停止する
            Invoke(nameof(ActiveRadio), 1f);
            Invoke(nameof(StopRadio), 2.5f);
            // 救助フラグをリセット
            RescueFlag = false;
        }
    }
    #endregion

    #region 救助処理
    // 救助時にランダムで画像を表示する
    public void ActiveRadio()
    {
        // 0から2の間でランダムな整数を生成
        Tmp = Random.Range(0, 3);

        // テキストを設定
        TMP.SetText(text);

        // ランダムな画像を表示
        if (Tmp == 0)
        {
            Rescued.SetActive(true);
        }
        else if (Tmp == 1)
        {
            Rescued2.SetActive(true);
        }
        else
        {
            Rescued3.SetActive(true);
        }
    }

    // 救助時の画像を非表示にする
    public void StopRadio()
    {
        // テキストをクリア
        TMP.SetText("");

        // 画像を非表示にする
        Rescued.SetActive(false);
        Rescued2.SetActive(false);
        Rescued3.SetActive(false);
    }
    #endregion

    #region テキストの状態管理
    // テキストの表示状態を設定する
    public void SetActiveText(bool b)
    {
        ActiveText = b;
    }

    // テキストの表示状態を取得する
    public bool IsItActiveText()
    {
        return ActiveText;
    }
    #endregion
}
