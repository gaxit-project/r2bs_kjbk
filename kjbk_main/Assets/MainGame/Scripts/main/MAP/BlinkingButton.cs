using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingButton : MonoBehaviour
{
    #region 宣言: 変数
    // public
    public float speed = 1.0f; // 点滅速度

    // private
    private RescueCount CounterScript; // RescueCountスクリプトのインスタンス
    private Text text; // Textコンポーネント
    private Image image; // Imageコンポーネント
    private float time; // 点滅時間のカウント
    private SwitchCamera blink; // SwitchCameraスクリプトのインスタンス
    private bool blinking = true; // 点滅の状態
    private bool blinking2 = false; // 点滅状態のフラグ

    private enum ObjType
    {
        TEXT, // オブジェクトタイプ: テキスト
        IMAGE // オブジェクトタイプ: 画像
    };
    private ObjType thisObjType = ObjType.TEXT; // 現在のオブジェクトタイプ
    #endregion

    #region 初期化: Startメソッド
    void Start()
    {
        blink = FindObjectOfType<SwitchCamera>(); // SwitchCameraスクリプトの取得
        CounterScript = FindObjectOfType<RescueCount>(); // RescueCountスクリプトの取得

        // アタッチしているオブジェクトのタイプを判別
        if (this.gameObject.GetComponent<Image>())
        {
            thisObjType = ObjType.IMAGE;
            image = this.gameObject.GetComponent<Image>(); // Imageコンポーネントの取得
        }
        else if (this.gameObject.GetComponent<Text>())
        {
            thisObjType = ObjType.TEXT;
            text = this.gameObject.GetComponent<Text>(); // Textコンポーネントの取得
        }
    }
    #endregion

    #region 更新: Updateメソッド
    void Update()
    {
        if (thisObjType == ObjType.IMAGE && image != null)
        {
            // 画像のアルファ値を更新
            image.color = GetAlphaColor(image.color);
        }
        else if (thisObjType == ObjType.TEXT && text != null)
        {
            // テキストのアルファ値を更新
            text.color = GetAlphaColor(text.color);
        }
    }
    #endregion

    #region 無効化処理: OnDisableメソッド
    void OnDisable()
    {
        // ゲームオブジェクトが無効化されたときに色をリセット
        if (thisObjType == ObjType.IMAGE && image != null)
        {
            if (blinking2)
            {
                blinking = false;
            }
            Color color = image.color;
            color.a = 1.0f; // アルファ値を最大に設定
            image.color = color;
        }
        else if (thisObjType == ObjType.TEXT && text != null)
        {
            Color color = text.color;
            color.a = 1.0f; // アルファ値を最大に設定
            text.color = color;
        }
    }
    #endregion

    #region 色取得: GetAlphaColorメソッド
    // Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        if (CounterScript.getNum() == 1 && !blink.initialMapStatusActivated)
        {
            if (blink.Ui_status)
            {
                if (blinking)
                {
                    blinking2 = true;
                    time += Time.deltaTime * 5.0f * speed;
                    color.a = Mathf.Sin(time) * 0.5f + 0.5f; // サイン波でアルファ値を更新
                }
            }
        }
        return color;
    }
    #endregion
}
