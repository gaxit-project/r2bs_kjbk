using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoRunNPC : MonoBehaviour
{
    public Transform Target;   //�i�r�Q�[�V�����ړI�n��Transform
    private NavMeshAgent m_Agent;   //NavMeshAgent
    public NavMeshPath path;

    private GameObject surface;
    private NavMeshBuild BuildScript;

    [SerializeField] private bool AutoRun = false;   //�~���ҒE�o�s����ON/OFF
    [SerializeField] private bool Severe;   //�d���Ҕ���
    [SerializeField] private float WaitSecond;

    public bool corFlag = false;   //�R���[�`������p�̃t���O

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();   //NavMeshAgent�̎擾
        m_Agent.SetDestination(Target.position);   //�i�r�Q�[�V�����ړI�n�̐ݒ�
        //StopAgent();   //�E�o�s����OFF�̏ꍇ�i�r�Q�[�V�������~

        surface = GameObject.Find("yuka");   //NavMeshSurface���A�^�b�`�����I�u�W�F�N�g��
        BuildScript = surface.GetComponent<NavMeshBuild>();   //NavMesh_Build�̃X�N���v�g

        //�y�ǎ҂ɕω����锻��
        StartCoroutine("Distance");
    }

    // Update is called once per frame
    void Update()
    {
        if (AutoRun && !Severe && m_Agent.isStopped)
        {
            MoveAgent();
        }
        else if (AutoRun && Severe && !m_Agent.isStopped)
        {
            StopAgent();
            OffAuto();
        }
    }

    IEnumerator Distance()   //WaitSecond�Ŏw�肵�Ă���b��
    {�@�@�@�@�@�@�@�@�@�@�@�@//�������W�̏ꍇ�i�r�Q�[�V������~
        Vector3 prePosition = transform.position;
        while (true)
        {
            yield return new WaitForSeconds(WaitSecond);
            if (Compare(prePosition))
            {
                //BuildScript.Build();   //�V�KMesh��Bake
                yield return new WaitForSeconds(WaitSecond);
                if (Compare(prePosition))
                {
                    if (!Severe)
                    {
                        Debug.Log("�y�ǎ҉�");
                        OffAuto();
                    }
                    break;
                }
            }
            prePosition = transform.position;
        }
    }

    bool Compare(Vector3 pre)   //��莞�ԑO�̍��W�ƌ��ݍ��W�̔�r
    {
        if (Mathf.Ceil(pre.x) == Mathf.Ceil(transform.position.x) &&    //���W�������ꍇtrue
                Mathf.Ceil(pre.z) == Mathf.Ceil(transform.position.z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MoveAgent()   //�i�r�Q�[�V����ON
    {
        m_Agent.isStopped = false;
    }

    void StopAgent()   //�i�r�Q�[�V����OFF
    {
        m_Agent.isStopped = true;
    }

    public void OnAuto()   //�E�o�s��ON
    {
        AutoRun = true;
        if (!corFlag)
        {
            StartCoroutine("Distance");
            corFlag = true;
        }
    }

    void OffAuto()   //�E�o�s��OFF
    {
        AutoRun = false;
    }

    private void OnCollisionStay(Collision collision)   //���ɐG�ꂽ�ۏd���҉�
    {
        if (collision.gameObject.name == "Blaze" && !Severe)
        {
            Debug.Log("�d���҉�");
            Severe = true;
        }
    }
}
