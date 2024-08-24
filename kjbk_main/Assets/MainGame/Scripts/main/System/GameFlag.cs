using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameFlag : MonoBehaviour
{
    [SerializeField] private InputActionReference _hold;
    [SerializeField] private Image _gaugeImage;

    private InputAction _holdAction;

    private bool ExitStatus = false;

    private int Rcnt = 0;

    public int RescueBorder = 10;


    private void Awake()
    {
        if (_hold == null) return;

        _holdAction = _hold.action;
        _holdAction.Enable();
    }

    void Start()
    {
        Rcnt = 0;
    }

    void Update()
    {
        if (_holdAction == null) return;

        // �������̐i�����擾
        var progress = _holdAction.GetTimeoutCompletionPercentage();

        // �i�����Q�[�W�ɔ��f
        _gaugeImage.fillAmount = progress;

        Rcnt = PlayerPrefs.GetInt("RescueCount");

        if (progress >= 1)
        {
            ExitStatus = true; // ExitStatus��true�ɂ���
            _gaugeImage.fillAmount = 0;
            _holdAction.Disable();  // Action����U������
            _holdAction.Enable();   // �����ɗL�������Ď��̓��͂ɔ�����

            //�~�������l����RescueBorder�l�ȏ�Ȃ�N���A�ֈڍs
            if (Rcnt >= RescueBorder)
            {
                PlayerPrefs.SetString("Result", "CLEAR");
                Scene.Instance.GameResult();
            }

            //�Ⴄ�Ȃ�Q�[���I�[�o�[�Ɉڍs
            else
            {
                PlayerPrefs.SetString("Result", "GAMEOVER");
                Scene.Instance.GameResult();
            }
        }
        else
        {
            // ExitStatus��false�Ƀ��Z�b�g
            ExitStatus = false;
        }
    }
}
