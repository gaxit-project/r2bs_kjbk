using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlaze : MonoBehaviour
{
    #region �t�B�[���h�錾
    private Inferno Inferno; // Inferno�R���|�[�l���g�ւ̎Q��
    #endregion

    #region ���Ί�̃p�[�e�B�N��������������
    // �p�[�e�B�N�����I�u�W�F�N�g�ɏՓ˂����Ƃ��̏���
    public void OnParticleCollision(GameObject obj)
    {
        // �Փ˂����I�u�W�F�N�g���uBlaze�v�^�O�����ꍇ
        if (obj.CompareTag("Blaze"))
        {
            // Inferno�R���|�[�l���g���擾
            Inferno = obj.GetComponent<Inferno>();

            // Inferno�R���|�[�l���g��DesBlaze�v���p�e�B��true�̏ꍇ
            if (Inferno.DesBlaze)
            {
                // ���Ή����Đ�
                obj.GetComponent<AudioSource>().Play();

                // �I�u�W�F�N�g��j��
                Destroy(obj);
            }
        }
    }
    #endregion
}
