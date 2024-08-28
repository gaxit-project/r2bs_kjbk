using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameFlag : MonoBehaviour
{
    #region 変数宣言
    // 長押し入力のアクション参照
    [SerializeField] private InputActionReference _hold;
    // ゲージを表示するImageコンポーネント
    [SerializeField] private Image _gaugeImage;

    // 長押しアクション
    private InputAction _holdAction;
    // ゲームの終了状態
    private bool ExitStatus = false;
    // 救助カウント
    private int Rcnt = 0;
    // 救助人数の閾値
    public int RescueBorder = 10;
    #endregion

    #region 初期化処理
    private void Awake()
    {
        #region アクションの設定
        if (_hold == null) return;

        _holdAction = _hold.action;
        _holdAction.Enable(); // アクションを有効化
        #endregion
    }

    void Start()
    {
        #region 変数の初期化
        Rcnt = 0; // 救助カウントの初期化
        #endregion
    }
    #endregion

    #region 更新処理
    void Update()
    {
        #region アクションの確認
        if (_holdAction == null) return;

        // 長押しの進捗を取得
        var progress = _holdAction.GetTimeoutCompletionPercentage();

        // 進捗をゲージに反映
        _gaugeImage.fillAmount = progress;
        #endregion

        #region 救助カウントの取得
        Rcnt = PlayerPrefs.GetInt("RescueCount");
        #endregion

        #region 終了条件のチェック
        if (progress >= 1)
        {
            ExitStatus = true; // ExitStatusをtrueにする
            _gaugeImage.fillAmount = 0; // ゲージをリセット
            _holdAction.Disable();  // Actionを一旦無効化
            _holdAction.Enable();   // すぐに有効化して次の入力に備える

            // 救助した人数がRescueBorder人以上ならクリアへ移行
            if (Rcnt >= RescueBorder)
            {
                PlayerPrefs.SetString("Result", "CLEAR");
                Scene.Instance.GameResult();
            }
            // 違うならゲームオーバーに移行
            else
            {
                PlayerPrefs.SetString("Result", "GAMEOVER");
                Scene.Instance.GameResult();
            }
        }
        else
        {
            // ExitStatusをfalseにリセット
            ExitStatus = false;
        }
        #endregion
    }
    #endregion
}
