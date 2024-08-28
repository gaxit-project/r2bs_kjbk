using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    #region 宣言: 変数
    [SerializeField] private GameObject pause; // ポーズ画面のUI
    public bool pause_status = false; // ポーズ状態を管理するフラグ

    private InputAction PauseAction; // ポーズアクション

    public static Presente presenter; // Presenteクラスのインスタンス
    public static bool pause1; // 設定中フラグ
    public static bool pause2; // タイトル中フラグ

    [SerializeField] private GameObject Presen; // Presenteオブジェクト
    [SerializeField] private Button ResumeIcon; // ポーズ解除ボタン
    [SerializeField] private GoalJudgement Goal; // ゴール判定スクリプト
    [SerializeField] private GameObject EscapeON; // Escape ONオブジェクト
    #endregion

    #region 初期化: Startメソッド
    void Start()
    {
        // ポーズ画面を非表示にする
        pause.SetActive(false);

        // PlayerInputコンポーネントの取得
        var pInput = GetComponent<PlayerInput>();

        // アクションマップの取得
        var actionMap = pInput.currentActionMap;

        // ポーズアクションの取得
        PauseAction = actionMap["Pause"];

        // Presenteコンポーネントの取得
        presenter = Presen.GetComponent<Presente>();
    }
    #endregion

    #region 更新: Updateメソッド
    void Update()
    {
        // 設定中フラグとタイトル中フラグの更新
        pause1 = presenter.ConfigSta;
        pause2 = presenter.TitleSta;

        // ポーズアクションがトリガーされたかをチェック
        bool pauseTriggered = PauseAction.triggered;

        // ポーズアクションがトリガーされ、設定中でもタイトル中でもない場合
        if (pauseTriggered && (!pause1 && !pause2))
        {
            // ポーズを切り替える
            PauseCon();

            // ポーズ解除ボタンを選択
            ResumeIcon.Select();
        }
    }
    #endregion

    #region ポーズ切替: PauseConメソッド
    public void PauseCon()
    {
        // ゲームが一時停止しているかをチェック
        if (Time.timeScale == 0)
        {
            // ポーズ画面を非表示にし、ゲームを再開
            pause.SetActive(false);
            Time.timeScale = 1.0f;
            pause_status = false;
        }
        else
        {
            // ポーズ画面を表示し、ゲームを一時停止
            pause.SetActive(true);
            Time.timeScale = 0.0f;
            pause_status = true;
        }
    }
    #endregion
}
