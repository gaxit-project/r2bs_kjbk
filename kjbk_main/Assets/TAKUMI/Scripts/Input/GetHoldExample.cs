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

        // performed�R�[���o�b�N�݂̂��󂯎��
        // ����������ɂȂ����炱�̃R�[���o�b�N���Ă΂��
        _hold.action.performed += OnHold;

        // ���͂��󂯎�邽�߂ɂ͕K���L��������K�v������
        _hold.action.Enable();
    }

    // ���������ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    private void OnHold(InputAction.CallbackContext context)
    {
        Debug.Log("���������ꂽ�I");
    }
}
