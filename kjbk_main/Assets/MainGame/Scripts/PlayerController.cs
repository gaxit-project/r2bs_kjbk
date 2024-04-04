using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mainCamera;      //メインカメラ格納用
    public GameObject subCamera;       //サブカメラ格納用 

    private bool CameraStatus = false; //カメラの状態
    public float Speed, RunSpeed; // 通常速度，ダッシュ速度
    float CurrentSpeed; // 現在速度
    float RotateSpeed = 15f; // 回転速度

    public static Vector3 CurrentForward;

    public static bool MoveStatus = false; //移動しているか

    void Start () {
        //メインカメラとサブカメラをそれぞれ取得
        GameObject mainCamera = GameObject.Find("Main Camera");
        GameObject subCamera = GameObject.Find("FPSCamera");

        //サブカメラを非アクティブにする
        mainCamera.SetActive(true); 
        subCamera.SetActive(false);
	}
    void Update()
    {
        Rigidbody rb = this.transform.GetComponent<Rigidbody>();

        if(DesSystem.DesSystemStatus == true)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                rb.velocity = Vector3.zero;
                MoveStatus = false;
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

                //垂直方向と水平方向の入力を取得
                float Xvalue = Input.GetAxis("Horizontal") * CurrentSpeed * Time.deltaTime;
                float Yvalue = Input.GetAxis("Vertical") * CurrentSpeed * Time.deltaTime;

                MoveStatus = true;

                //位置を移動
                Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue);
                rb.velocity += MoveDir;

                //進行方向を向く
                transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
                CurrentForward = transform.forward;
            }
        }
    }
}
