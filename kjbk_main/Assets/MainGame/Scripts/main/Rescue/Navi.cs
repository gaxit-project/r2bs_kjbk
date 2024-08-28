using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navi : MonoBehaviour
{

    #region �A�^�b�`�����I�u�W�F�N�g�ƃR���|�[�l���g�̎Q��
    // RescueNPC�X�N���v�g�̎Q��
    public RescueNPC RescueNPC;
    #endregion

    #region NavMeshAgent�֘A�̕ϐ�
    // NavMeshAgent�R���|�[�l���g�̎Q��
    [SerializeField] private NavMeshAgent _navMeshAgent = null;
    // �~���|�C���g��Transform
    [SerializeField] public Transform RescuePoint;
    #endregion


    #region �X�V����
    void Update()
    {
        #region NPC�����s���ŃA�C�R�����A�N�e�B�u�łȂ��ꍇ
        // NPC���ړ������A�C�R�����A�N�e�B�u�łȂ��ꍇ�A�ړI�n��ݒ�
        if (RescueNPC.IsItNPCrun() && !RescueNPC.IsItActiveIcon())
        {
            _navMeshAgent.destination = RescuePoint.position;
        }
        #endregion
    }
    #endregion
}
