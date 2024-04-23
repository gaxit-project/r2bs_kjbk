using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_daichi : MonoBehaviour
{
    public float Speed, RunSpeed, Debuff; // 通常速度，ダッシュ速度
    float CurrentSpeed; // 現在速度
    float DebuffSpeed;//移動速度低下割合
    float RotateSpeed = 15f; // 回転速度

    bool House = PlayerRay.HouseStatus;
    bool Follow = RescueNPC.Follow;//RescueNPCにおいてpublic staticになっていないので修正お願いします

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
            // 左シフト(ダッシュ)が押されているか判定
            if (Input.GetKey(KeyCode.LeftShift))
            {
                CurrentSpeed = RunSpeed;
            }
            else
            {
                CurrentSpeed = Speed;
            }
            //移動速度にデバフがかかるかどうかを判定
            if(House && Follow)
            {
                DebuffSpeed = Debuff;
            }
            else
            {
                DebuffSpeed = 1;
            }

            //垂直方向と水平方向の入力を取得
            float Xvalue = Input.GetAxis("Horizontal") * CurrentSpeed * DebuffSpeed * Time.deltaTime;
            float Yvalue = Input.GetAxis("Vertical") * CurrentSpeed * DebuffSpeed * Time.deltaTime;

            //位置を移動
            Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue);
            rb.velocity += MoveDir;

            //進行方向を向く
            transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
        }
    }
}

