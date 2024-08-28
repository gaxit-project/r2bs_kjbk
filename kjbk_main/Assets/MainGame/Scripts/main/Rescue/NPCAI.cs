using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{

    #region ��ԃt���O
    // �d���҂ł��邩�ǂ����𔻕ʂ���t���O
    [SerializeField] public bool Severe = false;
    // NPC���C���^���N�g�����ǂ����������t���O
    [SerializeField] bool interact = false;
    #endregion

    #region �ړ��֘A�̕ϐ�
    // NPC�̈ړ����x
    private float MoveSpeed = 10.0f;
    #endregion


    #region NPC�̓��쐧��

    // NPC���ړ���Ԃɂ���֐�
    public void MoveNPC()
    {
        interact = true;
    }
    #endregion

    #region �~���n�_�ɐG�ꂽ��
    // �Փ˔��菈��
    void OnCollisionStay(UnityEngine.Collision collision)
    {
        // NPC���~���n�_�ɓ��B�����ꍇ�̏���
        if (collision.gameObject.name == "RescuePoint")
        {
            interact = false;
        }
    }
    #endregion

}
