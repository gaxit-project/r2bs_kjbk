using UnityEngine;
using UnityEngine.InputSystem;

public class HoldProgressExample : MonoBehaviour
{
    // ���͂��󂯎��Ώۂ�Action
    [SerializeField] private InputActionReference _hold;

    private InputAction _holdAction;

    private void Awake()
    {
        if (_hold == null) return;

        _holdAction = _hold.action;

        // ���͂��󂯎�邽�߂ɂ͕K���L��������K�v������
        _holdAction.Enable();
    }

    private void Update()
    {
        if (_holdAction == null) return;

        // �������̐i�����擾
        var progress = _holdAction.GetTimeoutCompletionPercentage();

        // �i�������O�o��
        Debug.Log($"Progress : {progress * 100}%");
    }
}