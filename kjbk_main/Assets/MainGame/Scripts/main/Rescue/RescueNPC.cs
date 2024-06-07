using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class RescueNPC : MonoBehaviour
{
    //����
    [SerializeField] public bool Severe;
    [SerializeField] int NpcUp;   //NPC���^������Ƃ��Ƀv���C���[�̓���̂ǂꂾ����ɒǏ]���邩�̐��l
    [SerializeField] public string text;   //NPC�ɋ߂Â����Ƃ��ɕ\�������text

    //�A�^�b�`
    public GameObject Player;   //Player��GameObject
    public GameObject Zone;   //�~�o�����GameObject
    [SerializeField] TextMeshPro TMP;   //NPC�ɋ߂Â����Ƃ��ɕ\�������textMesh
    [SerializeField] public NPCAI NPCAI;   //NPC��AI�X�N���v�g
    [SerializeField] public RadioText RadioText;   //��������
    [SerializeField] public RescuePOP POP;
    private GameObject Rescue;
    RescueCount_verMatsuno CounterScript;   //�~���҃J�E���g
    RescueDiplication DiplicationScript;

    MeshRenderer mesh;   //MeshRendere

    public static bool Follow = false;   //NPC�̒Ǐ] true = �Ǐ] : false = �ҋ@
    bool InGoal = false;   //�~�o�n�_�ɐڐG true =�@�ڐG : false = ��ڐG
    bool InZone = false;   //�~�o�͈͂ɐڐG true = �ڐG : false = ��ڐG
    bool NPCrun = false;   //NPC�̎������� true = �������� : false = NPC_AI�ɂ�鑀��
    bool Rescued = false;   //�L�[�{�[�h����񂾂����͂��邽�߂̃t���O
    bool ActiveIcon = false;   //��b�A�C�R���̐���
    bool FirstContact = false;   //��b�񐔂̔���
    bool SecondContact = false;   //��b�񐔂̔���
    bool Lock = false;   //Player�̓����̌Œ�

    private InputAction TalkAction;

    private Animator FFanimator;

    public static int r_num = 0;
    [HideInInspector] public int MCnt = 0;  //�y���҂̃J�E���g�CMCnt��3�ɂȂ�����0�ɖ߂��ăJ�E���g��������
    public CollRadio RadioM;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        Rescue = GameObject.Find("Rescue");
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        CounterScript = Rescue.GetComponent<RescueCount_verMatsuno>();

        var pInput = this.GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        TalkAction = actionMap["Talk"];

        //�A�j���[�V�����ǂݍ���
        FFanimator = Player.GetComponent<Animator>();

        //������
        r_num = 0;
    }

    void FixedUpdate()
    {
        bool Talk = TalkAction.triggered;

        Transform target = Player.transform;   //Player��Transform
        Vector3 TargetPosition = target.position;
        if (IsItInZone())
        {
            if (Talk && Severe == true && !IsItInGoal())   //�d���҂ɋ߂Â����Ƃ�
            {
                if (!IsItFollow() && !DiplicationScript.getFlag())   //��Ǐ]��
                {
                    DiplicationScript.OnFlag();
                    StopNPC();
                    SetFollow(true);
                    FFanimator.SetBool("Carry", true);
                }
                else   //�Ǐ]��
                {
                    DiplicationScript.OffFlag();
                    SetFollow(false);
                    FFanimator.SetBool("Carry", false);
                    PutVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);
                }
            }
            if (Talk && Severe == false)   //�y�ǎ҂ɋ߂Â����Ƃ�
            {
                if (!IsItFirstContact())
                {
                    Debug.Log(SecondContact);
                    SetFirstContact(true);
                    SetActiveIcon(true);
                    StopNPC();
                    //WaitChange(3.5f);
                        Invoke("Count", 5f);
                    RadioText.SetActiveText(true);


                }
                else if (IsItFirstContact())
                {
                    SetSecondContact(true);
                    Debug.Log("Second");
                    StopNPC();
                    //WaitChange(3.5f);
                        Invoke("Count", 5f);
                    RadioText.SetActiveText(true);
                }
            }


            if (IsItFollow() && !IsItInGoal() && Severe == true)   //�Ǐ]��
            {
                FollowVectorNPC(TargetPosition.x, TargetPosition.y + NpcUp, TargetPosition.z);   //NPC���^�����鎞��Vector
                SetText("[E]Put");
            }

            if (IsItInGoal() && !IsItRescued() && Severe == true)   //�~�o�n�_�ɐڐG�����~�o���d����
            {
                DiplicationScript.OffFlag();
                SetText("");
                SetFollow(false);
                FFanimator.SetBool("Carry", false);
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPC���~�o�����Ƃ���Vector
                SetRescued(true);
                CountDestroy();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
                CounterScript.Count();   //�~���҃J�E���g
                r_num = CounterScript.getNum();
            }

            if (IsItInGoal() && !IsItRescued() && Severe == false)   //�~�o�n�_�ɐڐG�����~�o���y�ǎ�
            {
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPC���~�o�����Ƃ���Vector
                SetRescued(true);
                CountDestroy();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
                Invoke("Count", 5f);
            }
        }
    }

    //�֐�

    public int getNum()
    {
        return r_num;
    }
    public void CountDestroy()//�I�u�W�F�N�g�̔j��
    {
        if (Severe)//�d���҂̎�
        {
            POP.HeavyR();
        }
        else
        {
            POP.LightR();
        }
        Invoke("Destroy", 5f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void Count()
    {
        CounterScript.Count();   //�~���҃J�E���g
        r_num = CounterScript.getNum();
    }

    void FollowVectorNPC(float x, float y, float z)//NPC�̒Ǐ]
    {
        transform.position = new Vector3(x, y, z);
        transform.forward = PlayController.CurrentForward;
    }

    void RescuedVectorNPC(float x, float y, float z)//NPC�~�o���̓���
    {
        Vector3 NowPosition = new Vector3(x, y, z);
        Vector3 HelpPosition = new Vector3(x, y, z - 30);
        transform.position = Vector3.MoveTowards(NowPosition, HelpPosition, 12f);
    }

    void PutVectorNPC(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }

    public void StopNPC()   //NPC_AI�̒�~
    {
        NPCAI.MoveNPC();
        SetText("");
    }

    public void SetText(string text)   //text�̐ݒ�
    {
        TMP.text = text;
    }

    public void WaitChange(float f)
    {
        Invoke("ChangeAlpha", f);
    }

    private void ChangeAlpha()
    {
        StartCoroutine("TransParent");
    }

    IEnumerator TransParent()
    {
        for (int i = 0; i < 51; i++)
        {
            mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(0.07f);
        }
        Destroy();
    }

    //bool����
    public bool IsItFollow()
    {
        return Follow;
    }
    public bool IsItInGoal()
    {
        return InGoal;
    }
    public bool IsItNPCrun()
    {
        return NPCrun;
    }
    public bool IsItRescued()
    {
        return Rescued;
    }
    public bool IsItActiveIcon()
    {
        return ActiveIcon;
    }
    public bool IsItInZone()
    {
        return InZone;
    }
    public bool IsItFirstContact()
    {
        return FirstContact;
    }
    public bool IsItSecondContact()
    {
        return SecondContact;
    }
    public bool IsItLock()
    {
        return Lock;
    }

    //boolSet
    public void SetFollow(bool b)
    {
        Follow = b;
    }
    public void SetInGoal(bool b)
    {
        InGoal = b;
    }
    public void SetNPCrun(bool b)
    {
        NPCrun = b;
    }
    public void SetRescued(bool b)
    {
        Rescued = b;
    }
    public void SetActiveIcon(bool b)
    {
        ActiveIcon = b;
    }
    public void SetInZone(bool b)
    {
        InZone = b;
    }
    public void SetFirstContact(bool b)
    {
        FirstContact = b;
    }
    public void SetSecondContact(bool b)
    {
        SecondContact = b;
    }
    public void SetLock(bool b)
    {
        Lock = b;
    }
}