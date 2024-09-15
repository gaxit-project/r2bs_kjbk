using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    #region �錾
    // ���C���J�����i�[�p
    public GameObject mainCamera;
    // �T�u�J�����i�[�p
    public GameObject subCamera;
    // �J�����̏��
    private bool CameraStatus = false;
    // �ʏ푬�x�C�_�b�V�����x
    public float Speed, RunSpeed, Debuff;
    // ���ݑ��x
    float CurrentSpeed;
    // ��]���x
    float RotateSpeed = 15f;

    //���݂̃X�^�~�i
    float Stamina = 1f;//0�`1�ōő�X�^�~�i��1
    //�X�^�~�i�����
    public float StaminaDownSpeed = 5f;//5�b�����Ė����Ȃ�
    //�X�^�~�i�񕜗�
    public float StaminaUpSpeed = 10f;//110�b�q���ĉ�
    //�X�^�~�i�؂���N��������
    bool RunOut = false;

    // �ړ����x�ቺ����
    float DebuffSpeed;

    // �v���C���[�̏��
    bool House = PlayerRayCast.HosuStatus;
    bool Follow = RescueNPC.Follow;

    // ���݂̐i�s����
    public static Vector3 CurrentForward;

    // �ړ����Ă��邩
    public static bool MoveStatus = false;

    // �A�j���[�^�[�R���|�[�l���g
    private Animator animator;

    // �{�^���̉������
    private bool IsPressedRun;

    // �I�[�f�B�I�\�[�X
    private AudioSource audiosource;

    // ���̓A�N�V����
    private InputAction MoveAction;
    // �ړ����͂̏��
    public static bool MoveInput;

    // ���͒l
    float Yvalue;
    float Xvalue;

    #endregion

    #region PlayerInput�R�[���o�b�N
    // PlayerInput������Ă΂��R�[���o�b�N
    public void OnRun(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                // �{�^���������ꂽ���̏���
                IsPressedRun = true;
                break;

            case InputActionPhase.Canceled:
                // �{�^���������ꂽ���̏���
                IsPressedRun = false;
                break;
        }
    }
    #endregion

    #region ������
    private void Awake()
    {
        // �I�[�f�B�I�\�[�X���擾
        audiosource = GetComponent<AudioSource>();
    }

    void Start()
    {
        #region �擾�E�ǂݍ���
        // ���C���J�����ƃT�u�J���������ꂼ��擾
        mainCamera = GameObject.Find("Main Camera");
        subCamera = GameObject.Find("FPSCamera");

        // �A�j���[�^�[�R���|�[�l���g���擾
        animator = GetComponent<Animator>();

        var pInput = GetComponent<PlayerInput>();
        // ���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        // �A�N�V�����}�b�v����A�N�V�������擾
        MoveAction = actionMap["Move"];


        #endregion

        #region �ϐ�������
        // ���C���J�������A�N�e�B�u�ɂ���
        mainCamera.SetActive(true);
        // �T�u�J�������A�N�e�B�u�ɂ���
        subCamera.SetActive(false);

        //�X�^�~�i������
        Stamina = 1f;
        PlayerPrefs.SetFloat("Stamina", Stamina);

        // �ړ����͎�t���
        MoveInput = true;
        // ���̓��b�N�@0 = ����: 1 = ����
        PlayerPrefs.SetInt("Lock", 0);
        #endregion
    }
    #endregion

    void Update()
    {
        Follow = RescueNPC.Follow;


        Rigidbody rb = this.transform.GetComponent<Rigidbody>();

        Vector3 posi = this.transform.position;

        float speed = rb.velocity.magnitude;
        animator.SetFloat("speed", speed);


        #region Lock��0�̎����͋���
        if (0 == PlayerPrefs.GetInt("Lock"))
        {
            MoveInput = true;
        }
        else
        {
            MoveInput = false;
        }
        #endregion



        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Carry"))
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            //���������Ɛ��������̓��͂��擾
            Xvalue = Input.GetAxisRaw("Horizontal");
            Yvalue = Input.GetAxisRaw("Vertical");
        }

        #region �X�^�~�i�؂��
        if(Stamina >= 1f)
        {
            RunOut = false;
        }else if(Stamina <= 0f)
        {
            RunOut = true;
        }
        #endregion

        #region �ړ����Ȃ��Ƃ��̃X�^�~�i��
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            if (Stamina <= 1)
            {
                Stamina += Time.deltaTime / (StaminaUpSpeed = Follow ? 17f : 10f);
            }
            else//1�ɑ�����
            {
                Stamina = 1f;
            }
            PlayerPrefs.SetFloat("Stamina", Stamina);
        }
        #endregion

        #region �X�^�~�i�؂ꂪ�N��������
        PlayerPrefs.SetInt("RunOut", RunOut ?  1 : 0);//�N�������Ȃ�1
        #endregion

        #region �ړ����͏�ԋ���
        if (MoveInput)
        {
            #region MAP�g�p�����œ����Ȃ�����
            if (DesSystem.DesSystemStatus == true/*|| switchCamera.NiseMapON*/) // �}�b�v�\�����̓v���C���[�͓����Ȃ�����
            {
                rb.velocity = Vector3.zero;
                animator.SetBool("Walk", false);
            }
            
            #endregion
            #region ���Ί�g�p��
            else if (WaterHose.Hold)
            {

                MoveStatus = true;

                //�ʒu���ړ�
                Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue).normalized * CurrentSpeed * DebuffSpeed;

                //�i�s����������
                this.transform.position = posi;
                transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
                CurrentForward = transform.forward;
            }
            #endregion
            else
            {
                #region ���͂��Ȃ��Ƃ�
                if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                {
                    rb.velocity = Vector3.zero;
                    MoveStatus = false;
                    if (Follow)
                    {
                        animator.SetBool("CarryWalk", false);
                    }
                    else
                    {
                        animator.SetBool("Walk", false);
                    }

                }
                #endregion
                else
                #region ���͂����鎞
                {
                    #region �_�b�V����Ԕ���
                    // �_�b�V����Ԕ���
                    CurrentSpeed = IsPressedRun ? RunSpeed : Speed;
                    CurrentSpeed = RunOut ? Speed : CurrentSpeed;

                    #endregion

                    #region �f�o�t���ړ��{��
                    // �ړ����x�Ƀf�o�t�������邩�ǂ����𔻒�
                    DebuffSpeed = House || Follow || RunOut ? Debuff : 1;
                    #endregion

                    MoveStatus = true;

                    //�ʒu���ړ�
                    Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue).normalized * CurrentSpeed * DebuffSpeed;
                    if (!audiosource.isPlaying && !(Time.timeScale == 0f))
                    {
                        audiosource.Play();
                    }
                    rb.velocity = MoveDir;

                    if (Follow)//�d���҂�w�����Ă���
                    {
                        animator.SetBool("CarryWalk", true);
                        #region �w�������̃X�^�~�i�̑���
                        //�w�������̃X�^�~�i�����
                        StaminaDownSpeed = 3f;//3�b�����ď���
                        //�w�������̃X�^�~�i�񕜗�
                        StaminaUpSpeed = 17f;//17�b�����ĉ�
                        if (CurrentSpeed == Speed)//����
                        {
                            #region �w�����������̃X�^�~�i
                            if (Stamina <= 1)
                            {
                                Stamina += Time.deltaTime / StaminaUpSpeed;
                            }
                            else//1�ɑ�����
                            {
                                Stamina = 1f;
                            }
                            PlayerPrefs.SetFloat("Stamina", Stamina);
                            #endregion
                        }
                        else//�_�b�V��
                        {
                            #region �w�����_�b�V�����̃X�^�~�i
                            if (Stamina >= 0)
                            {
                                Stamina -= Time.deltaTime / StaminaDownSpeed;
                            }
                            else//0�ɑ�����
                            {
                                Stamina = 0f;
                            }
                            PlayerPrefs.SetFloat("Stamina", Stamina);
                            #endregion
                        }
                        #endregion
                    }
                    else//�d���҂�w�����Ă��Ȃ�
                    {
                        animator.SetBool("Walk", true);
                        #region ���펞�̃X�^�~�i�̑���
                        //�X�^�~�i�����
                        StaminaDownSpeed = 5f;//5�b�����Ė����Ȃ�
                        //�X�^�~�i�񕜗�
                        StaminaUpSpeed = 10f;//110�b�q���ĉ�
                        if (CurrentSpeed == Speed)//����
                        {
                            #region ����������̃X�^�~�i
                            if (Stamina <= 1)
                            {
                                Stamina += Time.deltaTime / StaminaUpSpeed;
                            }
                            else//1�ɑ�����
                            {
                                Stamina = 1f;
                            }
                            PlayerPrefs.SetFloat("Stamina", Stamina);
                            #endregion
                        }
                        else//�_�b�V��
                        {
                            #region ����_�b�V�����̃X�^�~�i
                            if (Stamina >= 0)
                            {
                                Stamina -= Time.deltaTime / StaminaDownSpeed;
                            }
                            else//0�ɑ�����
                            {
                                Stamina = 0f;
                            }
                            PlayerPrefs.SetFloat("Stamina", Stamina);
                            #endregion
                        }
                        #endregion
                    }

                    //�i�s����������
                    transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
                    CurrentForward = transform.forward;
                }
                #endregion
            }
        }
        #endregion
    }

}
