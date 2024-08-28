using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManage : MonoBehaviour
{
    #region �t�B�[���h�錾
    [SerializeField] private GameObject Water; // ���I�u�W�F�N�g�ւ̎Q��
    #endregion

    #region ������
    void Start()
    {
        // ���I�u�W�F�N�g���\���ɂ���
        Water.SetActive(false);
    }
    #endregion

    #region �X�V����
    void Update()
    {
        // PlayerRayCast��HosuStatus��true�̏ꍇ
        if (PlayerRayCast.HosuStatus == true)
        {
            // ���I�u�W�F�N�g��\������
            Water.SetActive(true);
        }
    }
    #endregion
}
