using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoWalk : MonoBehaviour
{

    //aa

    #region �A�^�b�`�����I�u�W�F�N�g�ƃR���|�[�l���g�̎Q��
    // �i�r�Q�[�V�����ړI�n��Transform
    public Transform Target;
    // NavMeshAgent�R���|�[�l���g
    private NavMeshAgent m_Agent;
    // NavMeshPath�̎Q��
    public NavMeshPath path;
    // NavMeshSurface���A�^�b�`�����I�u�W�F�N�g
    private GameObject surface;
    // NavMesh_Build�̃X�N���v�g
    private NavMeshBuild BuildScript;
    // RescueNPC�X�N���v�g�̎Q��
    private RescueNPC rescueNPC;
    // NPC�̃A�j���[�^�[�R���|�[�l���g
    private Animator NPCanimator;

    // ���o�\�ȋ���
    public float distance = 10f;
    #endregion

    #region �ݒ���ԊǗ��̕ϐ�
    // �~���ҒE�o�s����ON/OFF
    [SerializeField] private bool Auto = true;
    // �d���Ҕ���
    [SerializeField] private bool Severe;
    // �R���[�`������p�̃t���O
    public bool corFlag = false;
    // �ҋ@����
    [SerializeField] private float WaitSecond;
    #endregion

    #region �����_���ڕW�n�_�ݒ�p�̕ϐ�
    // MAP�̒��S�̈ʒu
    public Transform central;
    // �����_���Ō��߂�x���̍ő�l
    [SerializeField] float Xradius = 10;
    // �����_���Ō��߂�z���̍ő�l
    [SerializeField] float Zradius = 10;
    // �ݒ肵���ҋ@����
    [SerializeField] float waitTime = 5;
    // �ҋ@���Ԑ�����p
    [SerializeField] float time = 0;
    #endregion

    #region ���̑��̕ϐ�
    // �ڕW�n�_��x���̈ʒu
    float posX;
    // �ڕW�n�_��z���̈ʒu
    float posZ;
    // �G���J�E���g�̃t���O
    bool Encount = false;
    #endregion


    #region ������
    void Start()
    {
        #region �I�u�W�F�N�g�ƃR���|�[�l���g�̎擾
        // NavMeshAgent�̎擾
        m_Agent = GetComponent<NavMeshAgent>();
        // NavMeshSurface���A�^�b�`�����I�u�W�F�N�g��T��
        surface = GameObject.Find("yuka");
        // NavMesh_Build�̃X�N���v�g���擾
        BuildScript = surface.GetComponent<NavMeshBuild>();
        // RescueNPC�X�N���v�g�̎擾
        rescueNPC = GetComponent<RescueNPC>();
        // NPC�̃A�j���[�^�[�R���|�[�l���g�̎擾
        NPCanimator = this.GetComponent<Animator>();
        #endregion

        #region �����ݒ�
        // �ڕW�n�_�ɋ߂Â��Ă����x�𗎂Ƃ��Ȃ�
        m_Agent.autoBraking = false;
        // �ڕW�n�_�����߂�
        GotoNextPoint();
        #endregion
    }
    #endregion

    #region �X�V����
    void FixedUpdate()
    {
        #region ���x�ł̃A�j���[�V��������
        float speed  = m_Agent.velocity.magnitude;
        NPCanimator.SetFloat("NPCspeed", speed);
        #endregion



        #region �G���J�E���g����
        // ���ڐG����
        Encount = rescueNPC.IsItFirstContact();
        if (Input.GetKeyDown("y"))
        {
            Encount = true;
        }
        if (Encount)
        {
            m_Agent.ResetPath();
            Debug.Log("encount" + Encount);
            // �����E�o���J�n����
            m_Agent.destination = Target.position;
            OnAuto();
        }
        #endregion

        #region �ڕW�n�_�ݒ�ƈړ�
        // �҂����Ԃ𐔂���
        time += Time.deltaTime;

        #region Ray���΂��ăI�u�W�F�N�g���擾
        //transform�擾
        Transform trans = this.transform;
        Vector3 NPCvec = trans.position;
        NPCvec.y += 5f;

        // Ray��NPC�̈ʒu����Ƃ΂�
        var rayStartPosition = NPCvec;
        // Ray��NPC�������Ă�����ɂƂ΂�
        var rayDirection = trans.forward;

        // Hit�����I�u�W�F�N�g�i�[�p
        RaycastHit raycastHit;

        // Ray���΂��iout raycastHit ��Hit�����I�u�W�F�N�g���擾����
        var isHit = Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance);
        //Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);
        #endregion

        #region ���o������
        // �Ȃɂ������o������
        if (isHit)
        {
            // Hit�����I�u�W�F�N�g��Tag��������
            #region Hit�������̂����̎�
            if (raycastHit.collider.gameObject.CompareTag("Blaze"))
            {
                m_Agent.isStopped = true;
            }
            #endregion
        }
        #endregion

        // �҂����Ԃ��ݒ肳�ꂽ���l�𒴂���Ɣ���
        if (time > waitTime && !Encount)
        {
            //navmeshAgent�̒�~����
            if (m_Agent.isStopped)
            {
                m_Agent.isStopped = false;
            }
            // �ڕW�n�_��ݒ肵����
            GotoNextPoint();
            // �L�����ړ��A�j���[�V����
            time = 0;
        }
        if (GotoNextPointGoal())
        {
        }
        #endregion
    }
    #endregion

    #region �i�r�Q�[�V�������䃁�\�b�h
    // WaitSecond�Ŏw�肵�Ă���b����A�������W�̏ꍇ�i�r�Q�[�V������~
    IEnumerator Distance()
    {
        Vector3 prePosition = transform.position;
        while (true)
        {
            yield return new WaitForSeconds(WaitSecond);
            if (Compare(prePosition))
            {
                BuildScript.Build();   //�V�KMesh��Bake
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

    // ��莞�ԑO�̍��W�ƌ��ݍ��W�̔�r
    bool Compare(Vector3 pre)
    {
        if (Mathf.Ceil(pre.x) == Mathf.Ceil(transform.position.x) && Mathf.Ceil(pre.z) == Mathf.Ceil(transform.position.z))
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
    void OnAuto()
    {
        Auto = true;
        if (!corFlag)
        {
            corFlag = true;
        }
    }

    // �E�o�s��OFF
    void OffAuto()
    {
        Auto = false;
    }
    #endregion

    #region �ڕW�n�_�ݒ胁�\�b�h
    // �ڕW�n�_��ݒ肷��
    void GotoNextPoint()
    {
        // �ڕW�n�_��X���AZ���������_���Ō��߂�
        posX = Random.Range(-1 * Xradius, Xradius);
        posZ = Random.Range(-1 * Zradius, Zradius);

        // CentralPoint�̈ʒu��PosX��PosZ�𑫂�
        Vector3 pos = central.position;
        pos.x += posX;
        pos.z += posZ;

        // NavMeshAgent�ɖڕW�n�_��ݒ肷��
        m_Agent.destination = pos;
    }

    // �ڕW�n�_�ɓ��B�������̔���
    bool GotoNextPointGoal()
    {
        Vector3 NPCpos = this.transform.position;
        Vector3 pos = central.position;

        pos.x += posX;
        pos.z += posZ;

        if (NPCpos.x == pos.x && NPCpos.z == pos.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
