using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RescueCountText : MonoBehaviour
{
    #region �ϐ��̐錾
    // �e�L�X�g�\���p�̕ϐ�
    [SerializeField] TextMeshProUGUI RCount; // �~���Ґ���\������e�L�X�g
    [SerializeField] TextMeshProUGUI RSuccessCount; // ���������~���Ґ���\������e�L�X�g

    // UI�I�u�W�F�N�g�̎Q��
    public GameObject RsuccessCount; // ���������~���Ґ��\���p�̃I�u�W�F�N�g
    public GameObject Rcount; // �~���Ґ��\���p�̃I�u�W�F�N�g

    // �~���Ґ��J�E���g�p�̃X�N���v�g�Q��
    public RescueCount RCounter; // RescueCount�X�N���v�g�ւ̎Q��
    RescueNPC Rcounter1 = new RescueNPC(); // RescueNPC�X�N���v�g�̃C���X�^���X

    // �����Ŏg�p����J�E���g�p�̕ϐ�
    int Cnt; // ���ۂɎg�p����~���Ґ�

    // �~���Ґ��̌����Ƃɕ��������l
    int number10;
    int number1;
    #endregion

    #region ����������
    private void Awake()
    {
        // PlayerPrefs����~���Ґ����擾
        Cnt = PlayerPrefs.GetInt("RescueCount");
    }

    void Start()
    {
        // ������Ԃ̐ݒ�
        RsuccessCount.SetActive(false); // ���������~���Ґ��\�����\���ɐݒ�

        // �e�L�X�g�̐F��Ԃɐݒ�
        RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        // PlayerPrefs����~���Ґ����Ď擾
        Cnt = PlayerPrefs.GetInt("RescueCount");

        // �e�L�X�g�̏����\����ݒ�
        RCount.SetText("<sprite=" + Cnt + ">");
    }
    #endregion

    #region �X�V����
    void Update()
    {
        // PlayerPrefs����ŐV�̋~���Ґ����擾
        Cnt = PlayerPrefs.GetInt("RescueCount");

        // �~���Ґ���10�ȏ�̏ꍇ�̕\������
        if (Cnt >= 10)
        {
            // �~���Ґ���10�̈ʂ�1�̈ʂɕ���
            number10 = Cnt / 10 % 10;
            number1 = Cnt % 10;

            // �e�L�X�g�\���̍X�V
            RCount.SetText("<sprite=" + number10 + ">" + "<sprite=" + number1 + ">");

            // �\���̐؂�ւ�
            Rcount.SetActive(false);
            RsuccessCount.SetActive(true);
        }
        // �~���Ґ���10�����̏ꍇ�̕\������
        else
        {
            // �e�L�X�g�̐F��Ԃɐݒ�
            RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

            // �e�L�X�g�\���̍X�V
            RCount.SetText("<sprite=" + Cnt + ">");
        }
    }
    #endregion
}
