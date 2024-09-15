using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class RescueNPC : MonoBehaviour
{
    #region �ϐ�
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
    [SerializeField] HoldGauge HoldGauge;
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

    public Radio_ver4 Radio4;

    public R_Number number;

    GameObject gameManagerObj;
    GameManager gameManager;

    public int NumberR;

    public RadioText RText;
    public bool ArrowON = false;

    public static bool FirstFlag = false;

    bool RescueStopButtom = true;

    public static bool FirstResFlag = true;

    private NavMeshAgent navAgent;

    public TalkAI TalkAI;

    public static bool isTalkingToNPC = false; // �v���C���[��NPC�Ƙb���Ă��邩�ǂ�����ǐՂ���t���O
    public float interactionRange = 5f; // �Θb�\�ȋ����̐ݒ�i�K�X�����j
    #endregion

    void Start()
    {
        #region �擾�E�ǂݍ���
        TalkAI = GetComponent<TalkAI>();
        navAgent = GetComponent<NavMeshAgent>();  // NavMeshAgent�̃R���|�[�l���g���擾
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

        gameManagerObj = GameObject.Find("Manager");
        gameManager = gameManagerObj.GetComponent<GameManager>(); // �X�N���v�g���擾

        RescueStopButtom = true;
        FirstResFlag = true;


        #endregion

        #region ������

        //������
        r_num = 0;

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
        #endregion
    }

    void Update()
    {
        bool Talk = TalkAction.triggered;
        bool ResTalk = ResTalkAction.triggered;

        Transform target = Player.transform;   //Player��Transform
        Vector3 TargetPosition = target.position;

        #region �~���͈͂ɓ����Ă���
        if (IsItInZone())
        {
            
            #region �d����
            if ( Severe && !IsItInGoal())   //�d���҂ɋ߂Â����Ƃ�
            {
                if (HoldGauge != null && HoldGauge.gaugeStatus)//�~������
                {
                    Debug.Log("Follow ; " + IsItFollow() + "\nDiplication ; " + DiplicationScript.getFlag());
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
                        
                    //    DiplicationScript.OffFlag();
                    //    SetFollow(false);
                    //    PlayerPrefs.SetInt("Lock", 1);
                    //    FFanimator.SetBool("Walk", false);
                    //    FFanimator.SetBool("Carry", false);
                    //    NPCanimator.SetBool("NPCCarry", false);
                    //    NPCCol.enabled = true;
                    //    PutVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);
                    //    Invoke(nameof(MoveLock), 2f);
                    }
                    
                }

            }
            #endregion

            #region �y����
            //�y���ҏ��ŗp
            if (Talk && Severe == false && RescueStopButtom)   //�y�ǎ҂ɋ߂Â����Ƃ�
            {
                if (!IsItFirstContact())
                {
                    // �v���C���[���~������
                    TalkAI.FFStop = true;  // �v���C���[�L�������~
                    Debug.Log("�v���C���[����~���܂����B");
                    RescueStopButtom = false;
                    ComentON();// �I�u�W�F�N�g�폜
                    SetActiveIcon(true);
                    StopNPC();
                    RotateToPlayer();  // �y���҂��v���C���[�̕����Ɍ�����
                    RadioText.SetActiveText(true);
                    AudioManager.GetComponent<Audio>().PlaySound(2);    //��n�ύX�_

                    // AutoWalk�X�N���v�g�𖳌���
                    AutoWalk autoWalkScript = GetComponent<AutoWalk>();
                    if (autoWalkScript != null)
                    {
                        autoWalkScript.enabled = false;  // AutoWalk�X�N���v�g�𖳌���
                    }
                    // �i�r���b�V���G�[�W�F���g�𖳌���
                    if (navAgent != null)
                    {
                        navAgent.enabled = false;  // NavMeshAgent�𖳌���
                    }


                    StartCoroutine(StopAutoWalk());

                    RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPC���~�o�����Ƃ���Vector
                    SetRescued(true);
                    NPCanimator.SetBool("Walk", false);
                    CountDestroy();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
                }
            }
            #endregion

            #region �d���҂�S���ł���Ƃ�
            if (IsItFollow() && !IsItInGoal() && Severe == true)   //�Ǐ]��
            {
                FollowVectorNPC(TargetPosition.x, TargetPosition.y + NpcUp, TargetPosition.z);   //NPC���^�����鎞��Vector
                SetText("[E]Put");
            }
            #endregion

            #region �d���҂��~���n�_�ɂӂꂽ��
            if (IsItInGoal() && !IsItRescued() && Severe == true)   //�~�o�n�_�ɐڐG�����~�o���d����
            {
                DiplicationScript.OffFlag();
                SetText("");
                SetFollow(false);
                PlayerPrefs.SetInt("Lock", 0);//1�Ȃ烍�b�N
                FFanimator.SetBool("Carry", false);
                NPCanimator.SetBool("NPCCarry", false);
                NPCCol.enabled = true;
                Invoke(nameof(MoveLock), 2f);
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPC���~�o�����Ƃ���Vector
                SetRescued(true);
                CountDestroy2();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
                r_num = CounterScript.getNum();
                POP.PopR();
                ArrowON = false;
            }
            #endregion

            #region �y���҂��~���n�_�ɂӂꂽ��
            if (IsItInGoal() && !IsItRescued() && Severe == false)   //�~�o�n�_�ɐڐG�����~�o���y�ǎ�
            {
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPC���~�o�����Ƃ���Vector
                SetRescued(true);
                NPCanimator.SetBool("Walk", false);
                CountDestroy();   //��莞�Ԍ�ɃI�u�W�F�N�g�폜
                CounterScript.Count();
            }
            #endregion
        }
        #endregion
    }

    #region �֐�
    //�֐�
    public bool ArrowONOFF()
    {
        return ArrowON;
    }
    public void ArrowFlag()
    {
        ArrowON = true;
    }
    public int getNum()
    {
        return r_num;
    }

    public void ComentON()
    {
        number.RNumber();
        Radio4.Dialogue();
        POP.LightR();
        RText.RescueFlag = true;
    }
    public void CountDestroy()//�I�u�W�F�N�g�̔j��
    {
        if (Severe)//�d���҂̎�
        {
            POP.HeavyR();
        }
        //Invoke("Destroy", 5f);
    }
    public void CountDestroy2()//�I�u�W�F�N�g�̔j��
    {
        if (Severe)//�d���҂̎�
        {
            POP.HeavyR();
        }
        Invoke("Destroy", 0f);
        CounterScript.Count();   //�~���҃J�E���g
        r_num = CounterScript.getNum();
    }


    private void Destroy()
    {
        Destroy(this.gameObject);
        //CounterScript.Count();   //�~���҃J�E���g
        //r_num = CounterScript.getNum();

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
        Vector3 NowPosition = transform.position;  // ���݂̈ʒu
        Vector3 TargetPosition = new Vector3(x, y, z);  // �ڕW�ʒu

        // ���݈ʒu�ƖڕW�ʒu���傫������Ă���ꍇ�ɂ݈̂ړ�
        if (Vector3.Distance(NowPosition, TargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(NowPosition, TargetPosition, 0f);  // ���x��2f�ɕύX���Ĉړ����x���ɂ₩��
        }
    }

    void PutVectorNPC(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
        int ran = UnityEngine.Random.Range(0, 2); // �����_������
        if (ran == 0)
        {
            this.transform.Rotate(-90f, 0f, 0f); // �����_���Ȋp�x�ŉ�]
        }
        else
        {
            this.transform.Rotate(90f, 0f, 0f); // �����_���Ȋp�x�ŉ�]
        }
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
        //Destroy();
        RescueStopButtom = true;
    }

    IEnumerator StopAutoWalk()
    {
        // �t���O�����܂Ń��[�v
        while (!Radio_ver4.NPCStop)
        {
            yield return null; // �t���[�����Ƃɑҋ@�i1�t���[�����Ɋm�F�j
        }

        // �t���O���������炱�̏��������s�����
        
        if (navAgent != null)
        {
            navAgent.enabled = true;  // NavMeshAgent��L��
        }
        TalkAI TalkAIScript = GetComponent<TalkAI>();
        if (TalkAIScript != null)
        {
            TalkAIScript.enabled = true;  // talkAI�X�N���v�g��L��
            TalkAI.TalkToNPC();
        }

        CounterScript.Count();   //�~���҃J�E���g
        r_num = CounterScript.getNum();
        Radio_ver4.NPCStop = false;

        while (!TalkAI.NPCDestroy)
        {
            Debug.Log("FirstResFlag���̂P:" + FirstResFlag);
            if (FirstResFlag == true)
            {
                // NPC���폜���ꂽ��Ƀv���C���[�𓮂���悤�ɂ���
                TalkAI.FFStop = false;  // �v���C���[�L�������ēx������
                Debug.Log("NPC���폜����܂����B�v���C���[���ēx�����܂��B");
                Debug.Log("���߂Ă̋~���B���I");
                FirstResFlag = false;
                Debug.Log("FirstResFlag:" + FirstResFlag);
                Invoke("Destroy", 0.1f);
                break;
            }
            yield return null; // �t���[�����Ƃɑҋ@�i1�t���[�����Ɋm�F�j
        }
        RescueStopButtom = true;
        TalkAI.NPCDestroy = false;
    }

    void RotateToPlayer()
    {
        // �v���C���[�̈ʒu���擾
        Vector3 directionToPlayer = Player.transform.position - transform.position;

        // �����iy���W�j�𖳎����Đ��������ɂ̂݌����悤�ɂ���
        directionToPlayer.y = 0;

        // �����x�N�g�����[���łȂ��ꍇ�ɂ̂݉�]
        if (directionToPlayer != Vector3.zero)
        {
            // NPC���v���C���[�̕������������߂̉�]���v�Z
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // �����ɉ�]������
            transform.rotation = targetRotation;
        }
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
    #endregion
}