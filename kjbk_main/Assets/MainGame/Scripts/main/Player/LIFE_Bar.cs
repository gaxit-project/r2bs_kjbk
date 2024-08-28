using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIFE_Bar : MonoBehaviour
{
    #region �錾: �Q�[���I�u�W�F�N�g
    // HP1�̐ԐF�\��
    [SerializeField] private GameObject HP1Red;

    // HP2�̒ʏ�\��
    [SerializeField] private GameObject HP2;

    // HP2�̐ԐF�\��
    [SerializeField] private GameObject HP2Red;

    // HP3�̒ʏ�\��
    [SerializeField] private GameObject HP3;

    // HP3�̐ԐF�\��
    [SerializeField] private GameObject HP3Red;
    #endregion

    #region �錾: ��ԃt���O
    // ��ԃt���O�i���g�p�j
    private bool flag = true;
    #endregion

    #region �錾: HP�J�E���g
    // ���݂�HP
    private int i = 3;
    #endregion

    #region ������: Start���\�b�h
    void Start()
    {
        // ������Ԃ�HP�֘A�̃Q�[���I�u�W�F�N�g���\���ɂ���
        HP1Red.SetActive(false);
        HP2.SetActive(false);
        HP2Red.SetActive(false);
        HP3.SetActive(false);
        HP3Red.SetActive(false);
    }
    #endregion

    #region �֐�: HPBar���\�b�h
    public void HPBar()
    {
        // HP�̏�Ԃɉ������\���ݒ�
        if (i == 3)
        {
            // HP3�AHP2�AHP1�̕\���ݒ�
            HP1Red.SetActive(true);
            HP2Red.SetActive(false);
            HP3Red.SetActive(true);
            HP3.SetActive(true);
            HP2.SetActive(true);

            // HP3�̓_�ŊJ�n
            StartCoroutine(HPBar2());

            // ��莞�Ԍ�ɊeHP�I�u�W�F�N�g���\���ɂ���
            Invoke(nameof(HP1RedKesu), 5f);
            Invoke(nameof(HP2RedKesu), 5f);
            Invoke(nameof(HP3RedKesu), 5f);
            Invoke(nameof(HP3Kesu), 5f);
            Invoke(nameof(HP2Kesu), 5f);
        }
        else if (i == 2)
        {
            // HP2�AHP1�̕\���ݒ�
            HP2.SetActive(false);
            HP1Red.SetActive(true);
            HP2Red.SetActive(true);
            HP3.SetActive(true);

            // HP2�̓_�ŊJ�n
            StartCoroutine(HPBar1());

            // ��莞�Ԍ�ɊeHP�I�u�W�F�N�g���\���ɂ���
            Invoke(nameof(HP1RedKesu), 5f);
            Invoke(nameof(HP2RedKesu), 5f);
            Invoke(nameof(HP2Kesu), 5f);
            Invoke(nameof(HP3Kesu), 5f);
        }
        // HP�J�E���g���f�N�������g
        i--;
    }
    #endregion

    #region �֐�: HPBar2���\�b�h
    IEnumerator HPBar2()
    {
        // HP3�̐ԐF�\���̓_�ŏ���
        for (int i = 0; i < 10; i++)
        {
            // HP3Red���\���ɂ���
            HP3Red.SetActive(false);
            yield return new WaitForSeconds(0.2f);

            // HP3Red��\������
            HP3Red.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        // �ŏI�I��HP3Red���\���ɂ���
        HP3Red.SetActive(false);
    }
    #endregion

    #region �֐�: HPBar1���\�b�h
    IEnumerator HPBar1()
    {
        // HP2�̐ԐF�\���̓_�ŏ���
        for (int i = 0; i < 10; i++)
        {
            // HP2Red���\���ɂ���
            HP2Red.SetActive(false);
            yield return new WaitForSeconds(0.2f);

            // HP2Red��\������
            HP2Red.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        // �ŏI�I��HP2Red���\���ɂ���
        HP2Red.SetActive(false);
    }
    #endregion

    #region �֐�: HP�폜���\�b�h
    // HP3Red���\���ɂ��郁�\�b�h
    public void HP3RedKesu()
    {
        HP3Red.SetActive(false);
    }

    // HP2Red���\���ɂ��郁�\�b�h
    public void HP2RedKesu()
    {
        HP2Red.SetActive(false);
    }

    // HP1Red���\���ɂ��郁�\�b�h
    public void HP1RedKesu()
    {
        HP1Red.SetActive(false);
    }

    // HP3���\���ɂ��郁�\�b�h
    public void HP3Kesu()
    {
        HP3.SetActive(false);
    }

    // HP2���\���ɂ��郁�\�b�h
    public void HP2Kesu()
    {
        HP2.SetActive(false);
    }
    #endregion
}
