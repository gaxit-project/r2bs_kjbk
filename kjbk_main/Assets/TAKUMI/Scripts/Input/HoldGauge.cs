using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

internal class HoldGauge : MonoBehaviour
{
    [SerializeField] private InputActionReference _hold;
    [SerializeField] private Image _gaugeImage;

    private InputAction _holdAction;
    public static bool gaugeStatus = false;

    float progress;
    bool gaugeActivated = false;

    float lockTime = 0;


    private void Awake()
    {
        if (_hold == null) return;

        _holdAction = _hold.action;
        _holdAction.Enable();
    }

    private void OnEnable()
    {
        progress = 0.0f;
        gaugeStatus = false;
        gaugeActivated = false;
        // �i�����Q�[�W�ɔ��f
        _gaugeImage.fillAmount = progress;

        lockTime = 0;

        _holdAction.Disable();  // Action����U������
    }

    private void Update()
    {
        if (_holdAction == null) return;

        if(lockTime <= 0.5f)
        {
            lockTime += Time.deltaTime;
        }
        else
        {
            _holdAction.Enable();   // �����ɗL�������Ď��̓��͂ɔ�����

            // �������̐i�����擾
            progress = _holdAction.GetTimeoutCompletionPercentage();

            // �i�����Q�[�W�ɔ��f
            _gaugeImage.fillAmount = progress;

            // �i����1�ȏ�ɂȂ����Ƃ��̏���
            if (progress >= 1 && !gaugeActivated)
            {
                // gaugeStatus��true�ɂ���
                gaugeStatus = true;
                gaugeActivated = true;

                // �Q�[�W�����Z�b�g
                _gaugeImage.fillAmount = 0;
                _holdAction.Disable();  // Action����U������
                _holdAction.Enable();   // �����ɗL�������Ď��̓��͂ɔ�����
                progress = 0.0f;
            }
            else if (gaugeActivated)
            {
                // 1�t���[�����gaugeStatus��false�ɖ߂�
                gaugeStatus = false;
                gaugeActivated = false;
                lockTime = 0f;
            }
        }

    }
}
