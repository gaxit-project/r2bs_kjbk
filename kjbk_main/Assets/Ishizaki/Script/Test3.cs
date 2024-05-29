using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test3 : MonoBehaviour
{
    // ボタンの押下状態
    private bool _isPressedFire;
    private bool _isPressedTalk;

    private InputAction TakeAction;

    // PlayerInput側から呼ばれるコールバック
    // BehaviourにInvoke Unity Eventsが設定されていることを想定
    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                // ボタンが押された時の処理
                _isPressedFire = true;
                break;

            case InputActionPhase.Canceled:
                // ボタンが離された時の処理
                _isPressedFire = false;
                break;
        }
    }

    // PlayerInput側から呼ばれるコールバック
    public void OnTalkPress(InputAction.CallbackContext context)
    {
        _isPressedTalk = context.started;
    }

    public void Start()
    {
        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        TakeAction = actionMap["Take"];
    }

    private void Update()
    {
        bool Take = TakeAction.triggered;

        // 現在のボタンの押下状態をログ出力
        if (Take)
        {
            print($"isPressed = {Take}");
        }
    }
}

