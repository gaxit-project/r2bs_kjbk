using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Alpha : MonoBehaviour
{
    #region �t�B�[���h�錾
    private MeshRenderer mesh; // MeshRenderer�R���|�[�l���g
    #endregion

    #region ������
    void Start()
    {
        // MeshRenderer�R���|�[�l���g�̎擾
        mesh = GetComponent<MeshRenderer>();

        // �_�ŃR���[�`���̊J�n
        StartCoroutine("Blink");

        // 1.5�b���Stop���\�b�h���Ăяo��
        Invoke("Stop", 1.5f);
    }
    #endregion

    #region �R���[�`��
    IEnumerator Blink()
    {
        while (true)
        {
            #region �F���Â����鏈��
            // �F��100��Â�����
            for (int i = 0; i < 100; i++)
            {
                mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 1);
            }

            // 0.2�b�ҋ@
            yield return new WaitForSeconds(0.2f);
            #endregion

            #region �F�𖾂邭���鏈��
            // �F��100�񖾂邭����
            for (int j = 0; j < 100; j++)
            {
                mesh.material.color = mesh.material.color + new Color32(0, 0, 0, 1);
            }

            // 0.2�b�ҋ@
            yield return new WaitForSeconds(0.2f);
            #endregion
        }
    }
    #endregion

    #region ���\�b�h
    private void Stop()
    {
        // �_�ŃR���[�`�����~
        StopCoroutine("Blink");

        // �Q�[���I�u�W�F�N�g��j��
        Destroy(this.gameObject);
    }
    #endregion
}
