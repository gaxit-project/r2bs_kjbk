using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clear : MonoBehaviour
{
    #region �ϐ��錾
    // MeshRenderer�R���|�[�l���g
    private MeshRenderer sr;
    #endregion

    #region ����������
    void Start()
    {
        #region �R���|�[�l���g�̎擾
        sr = GetComponent<MeshRenderer>(); // MeshRenderer�R���|�[�l���g�̎擾
        #endregion

        #region �F�̕ύX
        // �F�����ɋ߂Â��鏈���iColor32�̃A���t�@�l255�ō��Ɂj
        sr.material.color = sr.material.color - new Color32(0, 0, 0, 255);
        #endregion
    }
    #endregion
}
