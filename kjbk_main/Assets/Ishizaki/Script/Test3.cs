using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test3 : MonoBehaviour
{
    // �{�^���̉������
    private bool _isPressedFire;
    private bool _isPressedTalk;

    private InputAction TakeAction;

    // PlayerInput������Ă΂��R�[���o�b�N
    // Behaviour��Invoke Unity Events���ݒ肳��Ă��邱�Ƃ�z��
    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                // �{�^���������ꂽ���̏���
                _isPressedFire = true;
                break;

            case InputActionPhase.Canceled:
                // �{�^���������ꂽ���̏���
                _isPressedFire = false;
                break;
        }
    }

    // PlayerInput������Ă΂��R�[���o�b�N
    public void OnTalkPress(InputAction.CallbackContext context)
    {
        _isPressedTalk = context.started;
    }

    public void Start()
    {
        var pInput = GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        TakeAction = actionMap["Take"];
    }

    private void Update()
    {
        bool Take = TakeAction.triggered;

        // ���݂̃{�^���̉�����Ԃ����O�o��
        if (Take)
        {
            print($"isPressed = {Take}");
        }
    }
}

