using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed, RunSpeed; // �ʏ푬�x�C�_�b�V�����x
    float CurrentSpeed; // ���ݑ��x
    float RotateSpeed = 15f; // ��]���x

    // Update is called once per frame
    void Update()
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

        //���������Ɛ��������̓��͂��擾
        float Xvalue = Input.GetAxis("Horizontal") * CurrentSpeed * Time.deltaTime;
        float Yvalue = Input.GetAxis("Vertical") * CurrentSpeed * Time.deltaTime;

        //�ʒu���ړ�
        Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue);
        transform.position += MoveDir;

        //�i�s����������
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
    }
}