using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioText : MonoBehaviour
{
    #region �錾
    // �\�������e�L�X�g
    [SerializeField] private string text;

    // �\�������TextMeshProUGUI�R���|�[�l���g
    [SerializeField] private TextMeshProUGUI TMP;

    // �~�����ɕ\������摜
    public GameObject Rescued;
    public GameObject Rescued2;
    public GameObject Rescued3;

    // �e�L�X�g�̕\�����
    private bool ActiveText = false;

    // �~���t���O
    public static bool RescueFlag = false;

    // �����_���ȉ摜�I��p�ϐ�
    private int Tmp = 0;
    #endregion

    #region ������
    private void Start()
    {
        // �~�����̉摜��������ԂŔ�\���ɂ���
        Rescued.SetActive(false);
        Rescued2.SetActive(false);
        Rescued3.SetActive(false);
    }
    #endregion

    #region �X�V����
    private void Update()
    {
        // �~���t���O�������Ă���Ƃ�
        if (RescueFlag)
        {
            // 1�b��Ƀ��W�I�A�N�e�B�u���A2�b��ɒ�~����
            Invoke(nameof(ActiveRadio), 1f);
            Invoke(nameof(StopRadio), 2.5f);
            // �~���t���O�����Z�b�g
            RescueFlag = false;
        }
    }
    #endregion

    #region �~������
    // �~�����Ƀ����_���ŉ摜��\������
    public void ActiveRadio()
    {
        // 0����2�̊ԂŃ����_���Ȑ����𐶐�
        Tmp = Random.Range(0, 3);

        // �e�L�X�g��ݒ�
        TMP.SetText(text);

        // �����_���ȉ摜��\��
        if (Tmp == 0)
        {
            Rescued.SetActive(true);
        }
        else if (Tmp == 1)
        {
            Rescued2.SetActive(true);
        }
        else
        {
            Rescued3.SetActive(true);
        }
    }

    // �~�����̉摜���\���ɂ���
    public void StopRadio()
    {
        // �e�L�X�g���N���A
        TMP.SetText("");

        // �摜���\���ɂ���
        Rescued.SetActive(false);
        Rescued2.SetActive(false);
        Rescued3.SetActive(false);
    }
    #endregion

    #region �e�L�X�g�̏�ԊǗ�
    // �e�L�X�g�̕\����Ԃ�ݒ肷��
    public void SetActiveText(bool b)
    {
        ActiveText = b;
    }

    // �e�L�X�g�̕\����Ԃ��擾����
    public bool IsItActiveText()
    {
        return ActiveText;
    }
    #endregion
}
