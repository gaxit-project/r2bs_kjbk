using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_daichi : MonoBehaviour
{
    public float Speed, RunSpeed, Debuff; // �ʏ푬�x�C�_�b�V�����x
    float CurrentSpeed; // ���ݑ��x
    float DebuffSpeed;//�ړ����x�ቺ����
    float RotateSpeed = 15f; // ��]���x

    bool House = PlayerRay.HouseStatus;
    bool Follow = RescueNPC.Follow;//RescueNPC�ɂ�����public static�ɂȂ��Ă��Ȃ��̂ŏC�����肢���܂�

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = this.transform.GetComponent<Rigidbody>();

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            rb.velocity = Vector3.zero;
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
            if(House && Follow)
            {
                DebuffSpeed = Debuff;
            }
            else
            {
                DebuffSpeed = 1;
            }

            //���������Ɛ��������̓��͂��擾
            float Xvalue = Input.GetAxis("Horizontal") * CurrentSpeed * DebuffSpeed * Time.deltaTime;
            float Yvalue = Input.GetAxis("Vertical") * CurrentSpeed * DebuffSpeed * Time.deltaTime;

            //�ʒu���ړ�
            Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue);
            rb.velocity += MoveDir;

            //�i�s����������
            transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
        }
    }
}

