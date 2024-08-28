using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueCount : MonoBehaviour
{
    #region �錾
    // �~���Ґ��̐ݒ�ƃJ�E���g�Ɋւ���ϐ�
    public int RescueMaxNum; // �ő�~���Ґ�
    public static int RescueNum = 0; // ���݂̋~���Ґ�
    public static int ResNumBest = 0; // �x�X�g�~���Ґ�
    public static int ResNumNormal = 0; // �m�[�}���~���Ґ�
    public static int ResNumBad = 0; // �o�b�h�~���Ґ�
    public bool RescueAll = false; // �ő�~���Ґ��ɒB�������̃t���O

    // ���̃R���|�[�l���g�ւ̎Q��
    public RCountText countText; // �~���Ґ��\���p
    public Radio ARadio; // ���W�I�@�\�̎Q��
    public CircleUI CirUI; // �T�[�N��UI

    // Mission�}�b�v�ɕϐ��𑗂邽�߂̎Q��
    public MissionMapUI MMUI;
    #endregion

    #region ������
    // �����ݒ���s��Start���\�b�h
    void Start()
    {
        // �~���Ґ��̏�����
        RescueNum = 0;

        // PlayerPrefs�ɏ����l��ݒ�
        PlayerPrefs.SetInt("RescueCount", RescueNum);
        PlayerPrefs.SetInt("ResCntBest", 0);
        PlayerPrefs.SetInt("ResCntNormal", 0);
        PlayerPrefs.SetInt("ResCntBad", 0);
    }
    #endregion

    #region �X�V
    void Update()
    {
        // �ő�~���Ґ��ɒB���Ă��邩�m�F���A�t���O��ݒ�
        if (RescueNum == RescueMaxNum)
        {
            RescueAll = true;
        }
    }
    #endregion

    #region �֐�
    // ���݂̋~���Ґ����J�E���g���郁�\�b�h
    public void Count()
    {
        RescueNum++;
        PlayerPrefs.SetInt("RescueCount", RescueNum); // ���݂̋~���Ґ���ۑ�

        // �~���Ґ���10�ȏ�̏ꍇ�A�~�b�V�������A�b�v�O���[�h
        if (RescueNum >= 10)
        {
            MMUI.MissionUpgread("a", 10);
        }
    }

    // ���݂̋~���Ґ����擾���郁�\�b�h
    public int getNum()
    {
        return RescueNum;
    }

    // �ő�~���Ґ��ɒB���Ă��邩�̃t���O���擾���郁�\�b�h
    public bool getRescueAll()
    {
        return RescueAll;
    }
    #endregion
}
