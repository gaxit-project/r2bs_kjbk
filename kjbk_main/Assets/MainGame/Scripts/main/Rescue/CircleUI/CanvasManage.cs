using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManage : MonoBehaviour
{
    #region �錾
    // ���g��RectTransform�R���|�[�l���g
    private RectTransform MyRectTfm;
    #endregion

    #region ������
    // Start�͍ŏ��̃t���[���X�V���ɌĂ΂�܂�
    void Start()
    {
        // ���g��RectTransform�R���|�[�l���g���擾
        MyRectTfm = GetComponent<RectTransform>();
    }
    #endregion

    #region �X�V����
    // Update�͖��t���[���Ă΂�܂�
    void Update()
    {
        // ���g�̌������J�����Ɍ�����
        MyRectTfm.LookAt(Camera.main.transform);
    }
    #endregion
}
