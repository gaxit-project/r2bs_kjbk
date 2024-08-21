using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

internal class HoldGauge : MonoBehaviour
{
    // ���͂��󂯎��Ώۂ�Action
    [SerializeField] private InputActionReference _hold;

    // �Q�[�W��UI
    [SerializeField] private Image _gaugeImage;

    //[SerializeField] private GameObject cube;

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
        //Debug.Log($"Progress : {progress * 100}%");
        if (progress >= 1)
        {
            Debug.Log("����������");
        }

        // �i�����Q�[�W�ɔ��f
        _gaugeImage.fillAmount = progress;

        /*if(progress >= 1)
        {
            cube.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 1);
        }
        else
        {
            cube.GetComponent<Renderer>().material.color = new Color32(248, 168, 133, 1);
        }*/
    }

    // ���������ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    private void OnHold(InputAction.CallbackContext context)
    {
        Debug.Log("���������ꂽ�I");
    }
}