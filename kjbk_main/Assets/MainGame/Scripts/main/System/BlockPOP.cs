using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPOP : MonoBehaviour
{
    #region ��Q���̐錾
    [SerializeField] private GameObject Corridor1;   // ��Q���R���h�[��1
    [SerializeField] private GameObject Corridor2;   // ��Q���R���h�[��2
    [SerializeField] private GameObject Corridor3;   // ��Q���R���h�[��3
    [SerializeField] private GameObject Corridor4;   // ��Q���R���h�[��4
    [SerializeField] private GameObject Wall1;       // ��Q���E�H�[��1
    [SerializeField] private GameObject Wall2;       // ��Q���E�H�[��2
    #endregion

    #region �|��Q�[�W�̃t���O
    [HideInInspector] public bool Generate40 = false; // �R���v�X�Q�[�W��40%�̂Ƃ��̃t���O
    [HideInInspector] public bool Generate20 = false; // �R���v�X�Q�[�W��20%�̂Ƃ��̃t���O
    [HideInInspector] public bool Generate10 = false; // �R���v�X�Q�[�W��10%�̂Ƃ��̃t���O
    [HideInInspector] public bool Judge = true;       // ��Q���ݒu����t���O
    #endregion

    #region ����������
    void Start()
    {
        #region ��Q���̏�����Ԑݒ�
        Corridor1.SetActive(false); // �R���h�[��1���\��
        Corridor2.SetActive(false); // �R���h�[��2���\��
        Corridor3.SetActive(false); // �R���h�[��3���\��
        Corridor4.SetActive(false); // �R���h�[��4���\��
        Wall1.SetActive(true);      // �E�H�[��1��\��
        Wall2.SetActive(true);      // �E�H�[��2��\��
        #endregion
    }
    #endregion

    #region �X�V����
    void Update()
    {
        #region ��Q���̐ݒu����
        // ��Q�������������ꏊ�ɒN�����Ȃ��Ƃ��ɏ�Q����ݒu
        if (Judge)
        {
            // �R���v�X�Q�[�W��40%�̂Ƃ��ɏ�Q���ݒu
            if (Generate40)
            {
                Corridor1.SetActive(true);  // �R���h�[��1��\��
                Generate40 = false;        // �t���O�����Z�b�g
            }
            // �R���v�X�Q�[�W��20%�̂Ƃ��ɏ�Q���ݒu
            else if (Generate20)
            {
                Corridor2.SetActive(true);  // �R���h�[��2��\��
                Corridor3.SetActive(true);  // �R���h�[��3��\��
                Wall1.SetActive(false);     // �E�H�[��1���\��
                Generate20 = false;        // �t���O�����Z�b�g
            }
            // �R���v�X�Q�[�W��10%�̂Ƃ��ɏ�Q���ݒu
            else if (Generate10)
            {
                Corridor4.SetActive(true);  // �R���h�[��4��\��
                Wall2.SetActive(false);     // �E�H�[��2���\��
                Generate10 = false;        // �t���O�����Z�b�g
            }
        }
        #endregion
    }
    #endregion

    #region �R���W��������
    // ��Q�����N���ꏊ�ɐl��������t���O���I�t�ɂ���
    public void OnCollisionEnter(Collision Hit2)
    {
        if (Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "MinorInjuries")
        {
            Judge = false; // �l������ƃt���O���I�t
        }
    }

    // ��Q�����N���ꏊ����l�����ꂽ��t���O���I���ɂ���
    public void OnCollisionExit(Collision Hit2)
    {
        if (Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "MinorInjuries")
        {
            Judge = true; // �l�����Ȃ��Ȃ�����t���O���I��
        }
    }
    #endregion
}
