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
    //�G���J�E���g�p
    private bool Encount = false;

    // 5�b�O�̈ʒu�ۑ��p
    private float time5;
    private Vector3 vector5;

    //NPC�ړ�����p�t���O
    private bool NPCflag = false;
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

        // 5�b�O�̈ʒu��ێ�
        time5 += Time.deltaTime;
        if (time5 > 5f)
        {
            vector5 = transform.position;
            time5 = 0;
        }

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
        // ���Ԃ𐔂���
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
            #region Hit�������̂����ŁANPC�������_���ړ����̎�
            if (raycastHit.collider.gameObject.CompareTag("Blaze") && NPCflag == false)
            {
                //NPC�t���O�I�� & �ړI�n�ύX
                NPCflag = true;
                m_Agent.destination = vector5;
            }
            #endregion
        }
        #endregion

        // �҂����Ԃ��ݒ肳�ꂽ���l�𒴂���Ɣ���
        if (time > waitTime && !Encount)
        {
            //NPC�t���O�I�t
            if (NPCflag)
            {
                NPCflag = false;
            }
            // �ڕW�n�_��ݒ肵����
            GotoNextPoint();
            // �L�����ړ��A�j���[�V����
            time = 0;
        }
        #endregion
    }
    #endregion

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
    #endregion
}
