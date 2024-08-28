using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoRunNPC : MonoBehaviour
{
    #region �ϐ��錾
    // �i�r�Q�[�V�����ړI�n��Transform
    public Transform Target;

    // NavMeshAgent
    private NavMeshAgent m_Agent;

    // NavMeshPath
    public NavMeshPath path;

    // NavMeshSurface���A�^�b�`�����I�u�W�F�N�g
    private GameObject surface;

    // NavMesh_Build�̃X�N���v�g
    private NavMeshBuild BuildScript;

    // �~���ҒE�o�s����ON/OFF
    [SerializeField] private bool AutoRun = false;

    // �d���Ҕ���
    [SerializeField] private bool Severe;

    // WaitSecond�Ŏw�肵�Ă���b��
    [SerializeField] private float WaitSecond;

    // �R���[�`������p�̃t���O
    public bool corFlag = false;
    #endregion

    #region Start���\�b�h
    // Start�͍ŏ��̃t���[���̑O��1�x�Ăяo����܂�
    void Start()
    {
        #region �I�u�W�F�N�g�̎擾�Ə�����
        m_Agent = GetComponent<NavMeshAgent>();   // NavMeshAgent�̎擾
        m_Agent.SetDestination(Target.position);   // �i�r�Q�[�V�����ړI�n�̐ݒ�

        surface = GameObject.Find("yuka");   // NavMeshSurface���A�^�b�`�����I�u�W�F�N�g��
        BuildScript = surface.GetComponent<NavMeshBuild>();   // NavMesh_Build�̃X�N���v�g�擾

        // �y�ǎ҂ɕω����锻��
        StartCoroutine("Distance");
        #endregion
    }
    #endregion

    #region Update���\�b�h
    // Update�̓t���[�����Ƃ�1�x�Ăяo����܂�
    void Update()
    {
        #region �����ړ��ƒ�~�̐���
        if (AutoRun && !Severe && m_Agent.isStopped)
        {
            MoveAgent();
        }
        else if (AutoRun && Severe && !m_Agent.isStopped)
        {
            StopAgent();
            OffAuto();
        }
        #endregion
    }
    #endregion

    #region �R���[�`��
    // WaitSecond�Ŏw�肵�Ă���b���A�������W�̏ꍇ�i�r�Q�[�V������~
    IEnumerator Distance()
    {
        Vector3 prePosition = transform.position;
        while (true)
        {
            yield return new WaitForSeconds(WaitSecond);
            if (Compare(prePosition))
            {
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
    #endregion

    #region �֐�
    // ��莞�ԑO�̍��W�ƌ��ݍ��W�̔�r
    bool Compare(Vector3 pre)
    {
        if (Mathf.Ceil(pre.x) == Mathf.Ceil(transform.position.x) &&
            Mathf.Ceil(pre.z) == Mathf.Ceil(transform.position.z))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // �i�r�Q�[�V����ON
    void MoveAgent()
    {
        m_Agent.isStopped = false;
    }

    // �i�r�Q�[�V����OFF
    void StopAgent()
    {
        m_Agent.isStopped = true;
    }

    // �E�o�s��ON
    public void OnAuto()
    {
        AutoRun = true;
        if (!corFlag)
        {
            StartCoroutine("Distance");
            corFlag = true;
        }
    }

    // �E�o�s��OFF
    void OffAuto()
    {
        AutoRun = false;
    }

    // ���ɐG�ꂽ�ۏd���҉�
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Blaze" && !Severe)
        {
            Debug.Log("�d���҉�");
            Severe = true;
        }
    }
    #endregion
}
