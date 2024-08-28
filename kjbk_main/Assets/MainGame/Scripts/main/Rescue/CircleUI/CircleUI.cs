using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleUI : MonoBehaviour
{
    #region �錾��
    // �^�C�}�[�̐ݒ莞��1
    public float LimitTime1 = 5.0f;
    // �^�C�}�[�̐ݒ莞��2
    public float LimitTime2 = 8.0f;
    // �~�^�C�v�̃v���O���X�o�[
    public GameObject CircleProgress;
    // �X�R�A�p�t���O
    public string ScoreFlag = "Best";

    // �F�ύX�p�t���O
    private int ColorFlag;
    // CircleProgress��Image�擾�p
    private Image ImgCircle;
    // �o�ߎ���
    private float PassedTime;
    // Rescue�Q�Ɨp
    private GameObject Rescue;
    // RescueNPC�Q�Ɨp
    public RescueNPC resNPC;

    // �X�R�A�p�F�e��Ԃŉ���~�o������
    public static int ResNumBest;
    public static int ResNumNormal;
    public static int ResNumBad;
    public static int ResNum;
    #endregion

    #region ������
    void Start()
    {
        // CircleProgress��Image�R���|�[�l���g�擾
        ImgCircle = CircleProgress.GetComponent<Image>();

        // �^�C�}�[�X�^�[�g
        ColorFlag = 1;

        // �~���Ґ���������
        ResNum = 0;
    }
    #endregion

    #region �h��Ԃ�����
    // �h��Ԃ�����
    private void Paint(float LimitTime)
    {
        // �o�ߎ��Ԃ���h��Ԃ��ʂ��v�Z
        PassedTime += Time.deltaTime;
        float amount = PassedTime / LimitTime;

        // �h��Ԃ��ʂ�������
        ImgCircle.fillAmount = 1 - amount;
    }
    #endregion

    #region �d���҃J�E���g����
    // �d���҃J�E���g
    public void SevereCount()
    {
        ResNum++;
        if (ScoreFlag == "Best")
        {
            PlayerPrefs.SetInt("ResCntBest", ResNum);
        }
        else if (ScoreFlag == "Normal")
        {
            PlayerPrefs.SetInt("ResCntNormal", ResNum);
        }
        else if (ScoreFlag == "Bad")
        {
            PlayerPrefs.SetInt("ResCntBad", ResNum);
        }

        // �J�E���g�����Z�b�g
        ResNum = 0;
    }
    #endregion

    #region �X�V����
    void Update()
    {
        if (ColorFlag != 0)
        {
            if (ColorFlag == 1)
            {
                // �^�C�}�[1�̓h��Ԃ�����
                Paint(LimitTime1);
                if (LimitTime1 < PassedTime)
                {
                    ColorFlag = 2;
                    ImgCircle.color = new Color32(233, 6, 4, 255); // �F�ύX
                    PassedTime = 0f;
                    ScoreFlag = "Normal";
                }
            }
            else if (ColorFlag == 2)
            {
                // �^�C�}�[2�̓h��Ԃ�����
                Paint(LimitTime2);
                if (LimitTime2 < PassedTime)
                {
                    ColorFlag = 0;
                    ScoreFlag = "Bad";
                }
            }
        }

        // �d���҂��S�[���ɓ��B���Ė��~�o���d���Ȃ�J�E���g
        if (resNPC.IsItInGoal() && !resNPC.IsItRescued() && resNPC.Severe == true)
        {
            SevereCount();
        }
    }
    #endregion
}
