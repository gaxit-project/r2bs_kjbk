using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoWalk : MonoBehaviour
{
    public Transform Target;   //�i�r�Q�[�V�����ړI�n��Transform
    private NavMeshAgent m_Agent;   //NavMeshAgent
    public NavMeshPath path;

    private GameObject surface;
    private NavMeshBuild BuildScript;

    [SerializeField] private bool Auto = true;   //�~���ҒE�o�s����ON/OFF
    [SerializeField] private bool Severe;   //�d���Ҕ���
    [SerializeField] private float WaitSecond;

    public bool corFlag = false;   //�R���[�`������p�̃t���O

    private Animator NPCanimator;

    float posX;
    float posZ;

    //MAP�̒��S�̈ʒu
    public Transform central;

    private RescueNPC rescueNPC;

    //�����_���Ō��߂�x���̍ő�l
    [SerializeField] float Xradius = 10;
    //�����_���Ō��߂�z���̍ő�l
    [SerializeField] float Zradius = 10;
    //�ݒ肵���ҋ@����
    [SerializeField] float waitTime = 5;
    //�ҋ@���Ԑ�����p
    [SerializeField] float time = 0;

    bool Encount = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();   //NavMeshAgent�̎擾

        surface = GameObject.Find("yuka");   //NavMeshSurface���A�^�b�`�����I�u�W�F�N�g��
        BuildScript = surface.GetComponent<NavMeshBuild>();   //NavMesh_Build�̃X�N���v�g

        rescueNPC = GetComponent<RescueNPC>();

        //�ڕW�n�_�ɋ߂Â��Ă����x�𗎂Ƃ��Ȃ�
        m_Agent.autoBraking = false;
        //�ڕW�n�_�����߂�
        GotoNextPoint();

        NPCanimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool Encount = rescueNPC.IsItFirstContact();
        if (Input.GetKeyDown("y"))
        {
            Encount = true;
        }
        if (Encount)
        {
            m_Agent.ResetPath();
            Debug.Log("encount" + Encount);
            //�����E�o���J�n����
            m_Agent.destination = Target.position;
            //m_Agent.isStopped = false;
            OnAuto();
        }
        //�҂����Ԃ𐔂���
        time += Time.deltaTime;

        //�҂����Ԃ��ݒ肳�ꂽ���l�𒴂���Ɣ���
        if (time > waitTime && !Encount)
        {
            //�ڕW�n�_��ݒ肵����
            GotoNextPoint();
            //�����ɃL�����ړ��A�j���[�V����
            NPCanimator.SetBool("Walk", true);
            time = 0;
        }
        if (GotoNextPointGoal())
        {
            NPCanimator.SetBool("Walk", false);
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

    void OnAuto()   //�E�o�s��ON
    {
        Auto = true;
        if (!corFlag)
        {
            corFlag = true;
        }
    }

    void OffAuto()   //�E�o�s��OFF
    {
        Auto = false;
    }

    void GotoNextPoint()
    {
        //�ڕW�n�_��X���AZ���������_���Ō��߂�
        posX = Random.Range(-1 * Xradius, Xradius);
        posZ = Random.Range(-1 * Zradius, Zradius);

        //CentralPoint�̈ʒu��PosX��PosZ�𑫂�
        Vector3 pos = central.position;
        pos.x += posX;
        pos.z += posZ;

        //NavMeshAgent�ɖڕW�n�_��ݒ肷��
        m_Agent.destination = pos;
    }

    bool GotoNextPointGoal()
    {
        Vector3 NPCpos = this.transform.position;
        Vector3 pos = central.position;

        pos.x += posX;
        pos.z += posZ;

        if(NPCpos.x == pos.x && NPCpos.z == pos.z)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
