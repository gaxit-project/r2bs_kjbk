using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueDiplication : MonoBehaviour
{
    #region �ϐ��̐錾
    // �t���O�̏�Ԃ�ێ�����ϐ�
    public bool diplication; // �d����Ԃ̃t���O
    public static RescueDiplication instance; // �V���O���g���p�^�[���̂��߂̃C���X�^���X
    #endregion

    #region ����������
    void Start()
    {
        // ������Ԃ�ݒ�
        diplication = false; // ������ԂŃt���O���I�t�ɐݒ�
    }
    #endregion

    #region �t���O���䃁�\�b�h
    // �t���O�̏�Ԃ��擾���郁�\�b�h
    public bool getFlag()
    {
        return diplication; // �t���O�̏�Ԃ�Ԃ�
    }

    // �t���O���I���ɂ��郁�\�b�h
    public void OnFlag()
    {
        diplication = true; // �t���O���I���ɐݒ�
    }

    // �t���O���I�t�ɂ��郁�\�b�h
    public void OffFlag()
    {
        diplication = false; // �t���O���I�t�ɐݒ�
    }
    #endregion
}
