using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonChangeAYX : MonoBehaviour
{
    #region 宣言: 変数
    [SerializeField] private InputActionReference _holdLong; // 長押しアクションのリファレンス

    private InputAction _holdLongAction; // 長押しアクション

    public GameObject YDbutton; // YDボタン
    public GameObject YSbutton; // YSボタン
    public GameObject Abutton; // Aボタン
    public GameObject AbuttonSub; // Aボタンサブ
    public GameObject Xbutton; // Xボタン

    private RescueNPC rescueNPC; // RescueNPCのインスタンス
    private RescueDiplication DiplicationScript; // RescueDiplicationのインスタンス
    private GameObject Rescue; // Rescueオブジェクト
    private bool Xb = false; // Xボタンの表示状態
    private bool x = false; // 他のボタンの表示状態
    public int cnt = 0; // ボタンのカウント
    #endregion

    #region 初期化: Awakeメソッド
    private void Awake()
    {
        // 長押しアクションが設定されている場合、アクションを有効化
        if (_holdLong == null) return;

        _holdLongAction = _holdLong.action;
        _holdLongAction.Enable();
    }
    #endregion

    #region 初期化: Startメソッド
    void Start()
    {
        // Rescueオブジェクトの取得
        Rescue = GameObject.Find("Rescue");
        // コンポーネントの取得
        rescueNPC = FindObjectOfType<RescueNPC>();
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        // ObjectMonitorのイベントを登録
        ObjectMonitor.OnObjectDestroyed += HandleObjectDestroyed;

        // 全てのボタンを非アクティブにする
        DeactivateAllButtons();
    }
    #endregion

    #region 更新: Updateメソッド
    void Update()
    {
        if (_holdLongAction == null) return;

        if (rescueNPC.IsItFollow())
        {
            var progress = _holdLongAction.GetTimeoutCompletionPercentage();

            if (progress > 0)
            {
                // Xボタンを表示
                if (Xbutton != null) Xbutton.SetActive(false);
            }
            else
            {
                // Xボタンを非表示
                if (Xbutton != null) Xbutton.SetActive(false);
            }
        }
    }
    #endregion

    #region 衝突処理: OnTriggerEnterメソッド
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ButtonAYX"))
        {
            cnt++;
            // 衝突したオブジェクトの名前に基づいて適切なボタンをアクティブ化
            switch (other.gameObject.name)
            {
                #region 消火器接近時の表示ボタン
                case "SyoukaButton":
                    if (!x)
                    {
                        DeactivateAllButtons();
                        if (YSbutton != null) YSbutton.SetActive(true);
                        x = true;
                    }
                    break;
                #endregion
                #region 話す時の表示ボタン
                case "AButton":
                    if (!x)
                    {
                        DeactivateAllButtons();
                        if (Abutton != null) Abutton.SetActive(true);
                        x = true;
                    }
                    break;
                #endregion
                #region サブミッション時の表示ボタン
                case "AButtonSub":
                    if (!x)
                    {
                        DeactivateAllButtons();
                        if (AbuttonSub != null) AbuttonSub.SetActive(true);
                        x = true;
                    }
                    break;
                #endregion
                #region 担ぐ時の表示ボタン
                case "XButton":
                    if (!Xb && Xbutton != null) Xbutton.SetActive(true);
                    break;
                default:
                    break;
                #endregion
            }
        }
    }
    #endregion

    #region 衝突処理: OnTriggerExitメソッド
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ButtonAYX"))
        {
            cnt = cnt - 1;
            DeactivateButton(other.gameObject.name);
        }
    }
    #endregion

    #region オブジェクト破壊処理: OnDestroyメソッド
    void OnDestroy()
    {
        ObjectMonitor.OnObjectDestroyed -= HandleObjectDestroyed;
        DeactivateAllButtons();
    }
    #endregion

    #region イベント処理: HandleObjectDestroyedメソッド
    void HandleObjectDestroyed(string buttonName)
    {
        cnt = 0;
        DeactivateButton(buttonName);
    }
    #endregion

    #region ボタンの非アクティブ化: DeactivateAllButtonsメソッド
    // 全てのボタンを非アクティブにする
    public void DeactivateAllButtons()
    {
        if (YDbutton != null) YDbutton.SetActive(false);
        if (YSbutton != null) YSbutton.SetActive(false);
        if (Abutton != null) Abutton.SetActive(false);
        if (AbuttonSub != null) AbuttonSub.SetActive(false);
        if (!Xb && Xbutton != null) Xbutton.SetActive(false);
        x = false;
    }
    #endregion

    #region ボタンの非アクティブ化: DeactivateButtonメソッド
    // 離れたオブジェクトに対応するボタンを非アクティブ
    private void DeactivateButton(string buttonName)
    {
        switch (buttonName)
        {
            case "DoorButton":
                if (YDbutton != null) YDbutton.SetActive(false);
                x = false;
                break;
            case "SyoukaButton":
                if (YSbutton != null) YSbutton.SetActive(false);
                x = false;
                break;
            case "AButton":
                if (Abutton != null) Abutton.SetActive(false);
                x = false;
                break;
            case "AButtonSub":
                if (AbuttonSub != null) AbuttonSub.SetActive(false);
                x = false;
                break;
            case "XButton":
                if (Xbutton != null && !Xb) Xbutton.SetActive(false);
                break;
            default:
                break;
        }
    }
    #endregion
}
