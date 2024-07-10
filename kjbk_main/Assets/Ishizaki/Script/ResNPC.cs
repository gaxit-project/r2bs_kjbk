using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResNPC : MonoBehaviour
{
    /*
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
    private GameObject ResCounter;
    private PlayController PlayCon;

    public GameObject AudioManager;//��n�ύX�_

    RescueCount CounterScript;   //�~���҃J�E���g
    RescueDiplication DiplicationScript;

    Audio talkScript;//��n�ύX�_

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
    private InputAction ResTalkAction;

    private Animator FFanimator;
    private Animator NPCanimator;

    BoxCollider NPCCol;

    public static int r_num = 0;
    [HideInInspector] public int MCnt = 0;  //�y���҂̃J�E���g�CMCnt��3�ɂȂ�����0�ɖ߂��ăJ�E���g��������
    public CollRadio RadioM;

    public Radio_ver3 Radio3;

    public R_Number number;

    GameObject gameManagerObj;
    GameManager gameManager;

    public int NumberR;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        Rescue = GameObject.Find("Rescue");
        ResCounter = GameObject.Find("Rcounter");
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        CounterScript = ResCounter.GetComponent<RescueCount>();
        PlayCon = Player.GetComponent<PlayController>();

        var pInput = this.GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        TalkAction = actionMap["Talk"];
        ResTalkAction = actionMap["ResTalk"];

        //�A�j���[�V�����ǂݍ���
        FFanimator = Player.GetComponent<Animator>();
        NPCanimator = this.GetComponent<Animator>();

        NPCCol = this.GetComponent<BoxCollider>();

        //������
        r_num = 0;

        gameManagerObj = GameObject.Find("Manager");
        gameManager = gameManagerObj.GetComponent<GameManager>(); // �X�N���v�g���擾


        //////////////////////////////////
        Follow = false;   //NPC�̒Ǐ] true = �Ǐ] : false = �ҋ@
        InGoal = false;   //�~�o�n�_�ɐڐG true =�@�ڐG : false = ��ڐG
        InZone = false;   //�~�o�͈͂ɐڐG true = �ڐG : false = ��ڐG
        NPCrun = false;   //NPC�̎������� true = �������� : false = NPC_AI�ɂ�鑀��
        Rescued = false;   //�L�[�{�[�h����񂾂����͂��邽�߂̃t���O
        ActiveIcon = false;   //��b�A�C�R���̐���
        FirstContact = false;   //��b�񐔂̔���
        SecondContact = false;   //��b�񐔂̔���
        Lock = false;

        //FFanimator.SetBool("Carry", false);
        //NPCanimator.SetBool("NPCCarry", false);
    }

    void FixedUpdate()
    {
        bool Talk = TalkAction.triggered;
        bool ResTalk = ResTalkAction.triggered;

        Transform target = Player.transform;   //Player��Transform
        Vector3 TargetPosition = target.position;
        if (IsItInZone())
        {
            if (ResTalk && Severe == true && !IsItInGoal())   //�d���҂ɋ߂Â����Ƃ�
            {
                if (!IsItFollow() && !DiplicationScript.getFlag())   //��Ǐ]��
                {
                    DiplicationScript.OnFlag();
                    StopNPC();
                    SetFollow(true);
                    PlayerPrefs.SetInt("Lock", 1);
                    FFanimator.SetBool("Walk", false);
                    FFanimator.SetBool("Carry", true);
                    NPCanimator.SetBool("NPCCarry", true);
                    NPCCol.enabled = false;
                    Invoke(nameof(MoveLock), 2f);
                }
                else   //�Ǐ]��
                {
                    DiplicationScript.OffFlag();
                    SetFollow(false);
                    PlayerPrefs.SetInt("Lock", 1);
                    FFanimator.SetBool("Walk", false);
                    FFanimator.SetBool("Carry", false);
                    NPCanimator.SetBool("NPCCarry", false);
                    NPCCol.enabled = true;
                    PutVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);
                    Invoke(nameof(MoveLock), 2f);
                }
            }
            if (Talk && Severe == false)   //�y�ǎ҂ɋ߂Â����Ƃ�
            {
                if (!IsItFirstContact())
                {
                    ComentON();// �I�u�W�F�N�g�폜
                    Debug.Log(SecondContact);
                    SetFirstContact(true);
                    SetActiveIcon(true);
                    StopNPC();
                    RadioText.SetActiveText(true);
                    NPCanimator.SetBool("Walk", true);
                    AudioManager.GetComponent<Audio>().PlaySound(2);    //��n�ύX�_
                }
                else if (IsItFirstContact())
                {
                    SetSecondContact(true);
                    Debug.Log("Second");
                    StopNPC();
                    RadioText.SetActiveText(true);
                    NPCanimator.SetBool("Walk", true);
                }
            }


            if (IsItFollow() && !IsItInGoal() && Severe == true)   //�Ǐ]��
            {
                FollowVectorNPC(TargetPosition.x, TargetPosition.y + NpcUp, TargetPosition.z);   //NPC���^�����鎞��Vector
                SetText("[E]Put");
            }

            if (IsItInGoal() && !IsItRescued() && Severe == true)   //�~�o�n�_�ɐڐG�����~�o���d����
            {
                gameManager.CallInoperable(2.0f); // 2 �b�ԁ@���̃X�N���v�g�𖳌��ɂ���iInput�ł��Ȃ��j
                DiplicationScript.OffFlag();
                SetText("");
                SetFollow(false);
                PlayerPrefs.SetInt("Lock", 1);
                FFanimator.SetBool("Carry", false);
                NPCanimator.SetBool("NPCCarry", false);
                NPCCol.enabled = true;
                Invoke(nameof(MoveLock), 2f);
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPC���~�o�����Ƃ���Vector
                SetRescued(true);
                CountDestroy();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
                //CounterScript.SevereCount();   //�~���҃J�E���g
                CounterScript.Count();
                r_num = CounterScript.getNum();
                POP.PopR();
            }

            if (IsItInGoal() && !IsItRescued() && Severe == false)   //�~�o�n�_�ɐڐG�����~�o���y�ǎ�
            {
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPC���~�o�����Ƃ���Vector
                SetRescued(true);
                NPCanimator.SetBool("Walk", false);
                CountDestroy();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
                CounterScript.Count();
            }
        }
    }

    //�֐�

    public int getNum()
    {
        return r_num;
    }

    public void ComentON()
    {
        number.RNumber();
        POP.LightR();
        Radio3.RHintFlag = true;
        Debug.Log("�y�ǎ҃t���O���񂷂��[��"+Radio3.RHintFlag);
    }
    public void CountDestroy()//�I�u�W�F�N�g�̔j��
    {
        if (Severe)//�d���҂̎�
        {
            POP.HeavyR();
            Radio3.RPopFlag = true;
        }
        Invoke("Destroy", 2f);
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
        transform.position = new Vector3(x, y-4.5f, z);
        transform.rotation = Quaternion.Euler(0,- 90, 0);
        transform.right = PlayController.CurrentForward;
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

    public void MoveLock()
    {
        PlayerPrefs.SetInt("Lock", 0);
    }
    */
}
