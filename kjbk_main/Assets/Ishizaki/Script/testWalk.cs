using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NavMeshAgent�g���Ƃ��ɕK�v
using UnityEngine.AI;

public class testWalk : MonoBehaviour
{
    //MAP�̒��S�̈ʒu
    public Transform central;

    private NavMeshAgent agent;
    private RescueNPC rescueNPC;

    //�����_���Ō��߂�x���̍ő�l
    [SerializeField] float Xradius = 10;
    //�����_���Ō��߂�z���̍ő�l
    [SerializeField] float Zradius = 10;
    //�ݒ肵���ҋ@����
    [SerializeField] float waitTime = 5;
    //�ҋ@���Ԑ�����p
    [SerializeField] float time = 0;

    //Vector3 pos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rescueNPC = GetComponent<RescueNPC>();

        //�ڕW�n�_�ɋ߂Â��Ă����x�𗎂Ƃ��Ȃ�
        agent.autoBraking = false;
        //�ڕW�n�_�����߂�
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        //�ڕW�n�_��X���AZ���������_���Ō��߂�
        float posX = Random.Range(-1 * Xradius, Xradius);
        float posZ = Random.Range(-1 * Zradius, Zradius);

        //CentralPoint�̈ʒu��PosX��PosZ�𑫂�
        Vector3 pos = central.position;
        pos.x += posX;
        pos.z += posZ;

        //NavMeshAgent�ɖڕW�n�_��ݒ肷��
        agent.destination = pos;
    }
    
    void Update()
    {
        //�҂����Ԃ𐔂���
        time += Time.deltaTime;

        //�҂����Ԃ��ݒ肳�ꂽ���l�𒴂���Ɣ���
        if (time > waitTime)
        {
            //�ڕW�n�_��ݒ肵����
            GotoNextPoint();
            time = 0;
        }
    }
}
