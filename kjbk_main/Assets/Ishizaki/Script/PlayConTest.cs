using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayConTest : MonoBehaviour
{
    public GameObject mainCamera;      //���C���J�����i�[�p
    public GameObject subCamera;       //�T�u�J�����i�[�p 

    private bool CameraStatus = false; //�J�����̏��
    public float Speed, RunSpeed, Debuff; // �ʏ푬�x�C�_�b�V�����x
    float CurrentSpeed; // ���ݑ��x
    float RotateSpeed = 15f; // ��]���x

    float DebuffSpeed;//�ړ����x�ቺ����

    bool House = PlayerRayCast.HosuStatus;
    bool Follow = RescueNPC.Follow;

    public static Vector3 CurrentForward;

    public static bool MoveStatus = false; //�ړ����Ă��邩

    private Animator animator;

    void Start()
    {
        //���C���J�����ƃT�u�J���������ꂼ��擾
        GameObject mainCamera = GameObject.Find("Main Camera");
        GameObject subCamera = GameObject.Find("FPSCamera");

        //�T�u�J�������A�N�e�B�u�ɂ���
        mainCamera.SetActive(true);
        subCamera.SetActive(false);

        //�A�j���[�V�����ǂݍ���
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Rigidbody rb = this.transform.GetComponent<Rigidbody>();

        if (DesSystem.DesSystemStatus == true)
        {
            rb.velocity = Vector3.zero;
            animator.SetBool("Walk", false);
        }
        else if (WaterHose.Hold)
        {
            //���������Ɛ��������̓��͂��擾
            float Xvalue = Input.GetAxis("Horizontal") * CurrentSpeed * Time.deltaTime;
            float Yvalue = Input.GetAxis("Vertical") * CurrentSpeed * Time.deltaTime;

            //�ʒu���ړ�
            Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue);
            animator.SetBool("Walk", false);

            //�i�s����������
            transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
            CurrentForward = transform.forward;
        }
        else
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                rb.velocity = Vector3.zero;
                MoveStatus = false;
                animator.SetBool("Walk", false);
            }
            else
            {
                // ���V�t�g(�_�b�V��)��������Ă��邩����
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    CurrentSpeed = RunSpeed;
                }
                else
                {
                    CurrentSpeed = Speed;
                }

                //�ړ����x�Ƀf�o�t�������邩�ǂ����𔻒�
                if (House || Follow)
                {
                    DebuffSpeed = Debuff;
                    UnityEngine.Debug.Log("�f�o�t");
                }
                else
                {
                    DebuffSpeed = 1;
                }

                //���������Ɛ��������̓��͂��擾
                float Xvalue = Input.GetAxisRaw("Horizontal");
                float Yvalue = Input.GetAxisRaw("Vertical");

                MoveStatus = true;

                //�ʒu���ړ�
                Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue).normalized * CurrentSpeed * DebuffSpeed;
                rb.velocity = MoveDir;
                animator.SetBool("Walk", true);

                //�i�s����������
                transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
                CurrentForward = transform.forward;
            }
        }
    }
}
