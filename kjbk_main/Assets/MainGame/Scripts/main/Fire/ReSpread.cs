using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpread : MonoBehaviour
{
    #region �t�B�[���h�錾
    private float minSecond;       // �ŏ��Ĕ�����
    private float maxSecond;       // �ő�Ĕ�����

    private GameObject Blaze;      // Blaze_Maneger �I�u�W�F�N�g
    private Blaze_Maneger m_Blaze; // Blaze_Maneger �R���|�[�l���g

    private bool Action = true;    // �A�N�V�����t���O
    #endregion

    #region ������
    private void Start()
    {
        // Blaze_Maneger �I�u�W�F�N�g�̎擾
        Blaze = GameObject.Find("BlazeManeger");
        m_Blaze = Blaze.GetComponent<Blaze_Maneger>();

        // Blaze_Maneger ����f�[�^�̎擾
        var Data = m_Blaze.getReData();
        minSecond = Data.min;
        maxSecond = Data.max;

        // �����_���ȍĔ����Ԃ�ݒ�
        RandomReSpread();

        // maxSecond + 1�b��� Des ���\�b�h���Ăяo��
        Invoke("Des", maxSecond + 1f);
    }
    #endregion

    #region �Ĕ�����
    private void RandomReSpread()
    {
        float Second = Random.Range(minSecond, maxSecond);

        if (Action)
        {
            // �����_���Ȏ��Ԍ�� Spread ���\�b�h���Ăяo��
            Invoke("Spread", Second);
            Action = false;
        }
    }

    private void Spread()
    {
        // Blaze �𐶐�����ʒu�̐ݒ�
        Vector3 blaze = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);

        // Blaze_Maneger �� Blaze �� SpreadPlane �𐶐�
        m_Blaze.CreateBlaze(blaze);
        m_Blaze.CreateSpreadPlane(this.transform.position);

        // ���g�̃Q�[���I�u�W�F�N�g��j��
        Destroy(this.gameObject);
    }
    #endregion

    #region �R���W��������
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "SpreadPlane")
        {
            Des();
        }
    }
    #endregion

    #region ������j��
    private void Des()
    {
        // ���g�̃Q�[���I�u�W�F�N�g��j��
        Destroy(this.gameObject);
    }
    #endregion
}
