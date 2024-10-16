using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetHoldExample : MonoBehaviour
{
    [SerializeField] private InputActionReference _hold;

    private void Awake()
    {
        if (_hold == null) return;

        // performedコールバックのみを受け取る
        // 長押し判定になったらこのコールバックが呼ばれる
        _hold.action.performed += OnHold;

        // 入力を受け取るためには必ず有効化する必要がある
        _hold.action.Enable();
    }

    // 長押しされたときに呼ばれるメソッド
    private void OnHold(InputAction.CallbackContext context)
    {
        Debug.Log("長押しされた！");
    }
}
