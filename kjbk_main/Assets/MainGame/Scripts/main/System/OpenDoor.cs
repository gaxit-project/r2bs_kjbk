using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    #region 変数宣言
    // プレイヤーやNPCが近くにいるかどうかのフラグ
    private bool Near;
    private bool NPCNear;

    // ドアのアニメーター
    private Animator animator;

    // ドアのコライダー
    public BoxCollider doorCollider;

    // 入力アクション
    private InputAction TakeAction;

    // ドアが開いているかどうかのフラグ
    bool DoorOpen = false;
    #endregion

    #region Startメソッド
    // Startは最初のフレームの前に1度呼び出されます
    void Start()
    {
        #region 変数の初期化
        Near = false;
        NPCNear = false;
        animator = GetComponentInChildren<Animator>();  // アニメーターの取得
        //doorCollider = GetComponentInChildren<BoxCollider>(); // コライダーの取得（コメントアウト）

        var pInput = GetComponent<PlayerInput>();  // PlayerInputの取得

        // 現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        // アクションマップからアクションを取得
        TakeAction = actionMap["Take"];
        #endregion
    }
    #endregion

    #region Updateメソッド
    void Update()
    {
        #region 入力アクションの取得
        bool Take = TakeAction.triggered;
        #endregion

        #region ドアの状態更新
        if (Near || NPCNear)
        {
            doorCollider.enabled = false;  // ドアのコライダーを無効化
            animator.SetBool("Open", true);  // ドアを開けるアニメーションを再生
            //Audio.Instance.PlaySound(0); // 音の再生（コメントアウト）
        }
        else
        {
            animator.SetBool("Open", false);  // ドアを閉じるアニメーションを再生
            //Audio.Instance.PlaySound(1); // 音の再生（コメントアウト）
        }
        #endregion
    }
    #endregion

    #region コライダーのトリガー処理
    // トリガーに入ったときの処理
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = true;  // プレイヤーが近くにいる
        }

        if (col.tag == "MinorInjuries")
        {
            NPCNear = true;  // NPCが近くにいる
        }
    }

    // トリガーから出たときの処理
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = false;  // プレイヤーが離れた
            animator.SetBool("Open", false);  // ドアを閉じるアニメーションを再生
            //Audio.Instance.PlaySound(1); // 音の再生（コメントアウト）
            doorCollider.enabled = true;  // ドアのコライダーを有効化
        }

        if (col.tag == "MinorInjuries")
        {
            NPCNear = false;  // NPCが離れた
            animator.SetBool("Open", false);  // ドアを閉じるアニメーションを再生
            doorCollider.enabled = true;  // ドアのコライダーを有効化
            //Audio.Instance.PlaySound(1); // 音の再生（コメントアウト）
        }
    }
    #endregion

    #region ドアの開閉処理
    // ドアの開閉処理
    void DoorOpenONOFF()
    {
        animator.SetBool("Open", false);  // ドアを閉じるアニメーションを再生
        doorCollider.enabled = true;  // ドアのコライダーを有効化
        DoorOpen = false;  // ドアが閉じているフラグ
    }
    #endregion
}
