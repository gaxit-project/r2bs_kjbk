using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

internal class HoldGauge : MonoBehaviour
{
    [SerializeField] private InputActionReference _hold;
    [SerializeField] private Image _gaugeImage;
    [SerializeField] private GameObject cube;

    private InputAction _holdAction;
    public static bool gaugeStatus = false;

    private void Awake()
    {
        if (_hold == null) return;

        _holdAction = _hold.action;
        _holdAction.Enable();
    }

    private void Update()
    {
        if (_holdAction == null) return;

        gaugeStatus = false;

        // �������̐i�����擾
        var progress = _holdAction.GetTimeoutCompletionPercentage();

        // �i�����Q�[�W�ɔ��f
        _gaugeImage.fillAmount = progress;

        // �i����1�ȏ�ɂȂ����Ƃ��̏���
        if (progress >= 0.97)
        {
            gaugeStatus = true; // aa��true�ɂ���

            

            // �L���[�u�̐F��ύX
            //cube.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 255);

            // �Q�[�W�����Z�b�g
            _gaugeImage.fillAmount = 0;
            _holdAction.Disable();  // Action����U������
            _holdAction.Enable();   // �����ɗL�������Ď��̓��͂ɔ�����
        }
        else
        {
            // aa��false�Ƀ��Z�b�g
            gaugeStatus = false;
            //cube.GetComponent<Renderer>().material.color = new Color32(248, 168, 133, 255);
        }
    }
}
