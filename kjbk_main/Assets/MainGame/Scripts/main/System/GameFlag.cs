using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameFlag : MonoBehaviour
{
    #region �ϐ��錾
    // ���������͂̃A�N�V�����Q��
    [SerializeField] private InputActionReference _hold;
    // �Q�[�W��\������Image�R���|�[�l���g
    [SerializeField] private Image _gaugeImage;

    // �������A�N�V����
    private InputAction _holdAction;
    // �Q�[���̏I�����
    private bool ExitStatus = false;
    // �~���J�E���g
    private int Rcnt = 0;
    // �~���l����臒l
    public int RescueBorder = 10;
    #endregion

    #region ����������
    private void Awake()
    {
        #region �A�N�V�����̐ݒ�
        if (_hold == null) return;

        _holdAction = _hold.action;
        _holdAction.Enable(); // �A�N�V������L����
        #endregion
    }

    void Start()
    {
        #region �ϐ��̏�����
        Rcnt = 0; // �~���J�E���g�̏�����
        #endregion
    }
    #endregion

    #region �X�V����
    void Update()
    {
        #region �A�N�V�����̊m�F
        if (_holdAction == null) return;

        // �������̐i�����擾
        var progress = _holdAction.GetTimeoutCompletionPercentage();

        // �i�����Q�[�W�ɔ��f
        _gaugeImage.fillAmount = progress;
        #endregion

        #region �~���J�E���g�̎擾
        Rcnt = PlayerPrefs.GetInt("RescueCount");
        #endregion

        #region �I�������̃`�F�b�N
        if (progress >= 1)
        {
            ExitStatus = true; // ExitStatus��true�ɂ���
            _gaugeImage.fillAmount = 0; // �Q�[�W�����Z�b�g
            _holdAction.Disable();  // Action����U������
            _holdAction.Enable();   // �����ɗL�������Ď��̓��͂ɔ�����

            // �~�������l����RescueBorder�l�ȏ�Ȃ�N���A�ֈڍs
            if (Rcnt >= RescueBorder)
            {
                PlayerPrefs.SetString("Result", "CLEAR");
                Scene.Instance.GameResult();
            }
            // �Ⴄ�Ȃ�Q�[���I�[�o�[�Ɉڍs
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
        #endregion
    }
    #endregion
}
