using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    #region �錾: �ϐ�
    [SerializeField] public GameObject FF; // �Q�[���I�u�W�F�N�g FF�i�g�p����Ă��Ȃ��j
    private double _time; // ���ԁi�g�p����Ă��Ȃ��j
    private float _cycle = 1; // �T�C�N���̎��ԁi�g�p����Ă��Ȃ��j
    private SpriteRenderer Sr; // �X�v���C�g�����_���[
    private float Transparency = 0.0f; // �����x
    #endregion

    #region ������: Start���\�b�h
    void Start()
    {
        // �X�v���C�g�����_���[���擾
        Sr = GetComponent<SpriteRenderer>();
    }
    #endregion

    #region �֐�: Tenmetsu���\�b�h
    public void Tenmetsu()
    {
        // �����x��ݒ肷�鏈���i���[�v���s�v�Œ��ڐݒ肷�邱�Ƃ𐄏��j
        for (int i = 0; i < 10; i++)
        {
            Sr.color = new Color(Sr.color.r, Sr.color.g, Sr.color.b, Transparency);
        }
    }
    #endregion
}
