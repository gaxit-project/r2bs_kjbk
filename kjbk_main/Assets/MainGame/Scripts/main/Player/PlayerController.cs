using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayController : MonoBehaviour
{
    public GameObject mainCamera;      //メインカメラ格納用
    public GameObject subCamera;       //サブカメラ格納用 

    private bool CameraStatus = false; //カメラの状態
    public float Speed, RunSpeed, Debuff; // 通常速度，ダッシュ速度
    float CurrentSpeed; // 現在速度
    float RotateSpeed = 15f; // 回転速度

    float DebuffSpeed;//移動速度低下割合

    bool House = PlayerRayCast.HosuStatus;
    bool Follow = RescueNPC.Follow;

    public static Vector3 CurrentForward;

    public static bool MoveStatus = false; //移動しているか

    private Animator animator;

    private bool IsPressedRun; // ボタンの押下状態

    private AudioSource audiosource;

    private InputAction MoveAction;

    public static bool MoveInput;

    // PlayerInput側から呼ばれるコールバック
    public void OnRun(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                // ボタンが押された時の処理
                IsPressedRun = true;
                break;

            case InputActionPhase.Canceled:
                // ボタンが離された時の処理
                IsPressedRun = false;
                break;
        }
    }

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    void Start()
    {
        //メインカメラとサブカメラをそれぞれ取得
        GameObject mainCamera = GameObject.Find("Main Camera");
        GameObject subCamera = GameObject.Find("FPSCamera");

        //サブカメラを非アクティブにする
        mainCamera.SetActive(true);
        subCamera.SetActive(false);

        MoveInput = true;
        PlayerPrefs.SetInt("Lock", 0);

        //アニメーション読み込み
        animator = GetComponent<Animator>();

        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        MoveAction = actionMap["Move"];



    }
    void Update()
    {
        Follow = RescueNPC.Follow;

        print($"isPressed = {IsPressedRun}");

        Rigidbody rb = this.transform.GetComponent<Rigidbody>();

        Vector3 posi = this.transform.position;

        if(0 == PlayerPrefs.GetInt("Lock"))
        {
            MoveInput = true;
        }
        else
        {
            MoveInput = false;
        }

        if (MoveInput)
        {
            if (DesSystem.DesSystemStatus == true)
            {
                rb.velocity = Vector3.zero;
                animator.SetBool("Walk", false);
            }
            else if (WaterHose.Hold)
            {

                //垂直方向と水平方向の入力を取得
                float Xvalue = Input.GetAxisRaw("Horizontal");
                float Yvalue = Input.GetAxisRaw("Vertical");

                MoveStatus = true;

                //位置を移動
                Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue).normalized * CurrentSpeed * DebuffSpeed;

                //進行方向を向く
                this.transform.position = posi;
                transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
                CurrentForward = transform.forward;
            }
            else
            {
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
                else
                {
                    // ダッシュ状態判定
                    if (IsPressedRun)
                    {
                        CurrentSpeed = RunSpeed;
                    }
                    else
                    {
                        CurrentSpeed = Speed;
                    }

                    //移動速度にデバフがかかるかどうかを判定
                    if (House || Follow)
                    {
                        DebuffSpeed = Debuff;
                        UnityEngine.Debug.Log("デバフ");
                    }
                    else
                    {
                        DebuffSpeed = 1;
                    }

                    //垂直方向と水平方向の入力を取得
                    float Xvalue = Input.GetAxisRaw("Horizontal");
                    float Yvalue = Input.GetAxisRaw("Vertical");

                    MoveStatus = true;

                    //位置を移動
                    Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue).normalized * CurrentSpeed * DebuffSpeed;
                    if (!audiosource.isPlaying && !(Time.timeScale == 0f))
                    {
                        audiosource.Play();
                    }
                    rb.velocity = MoveDir;
                    if (Follow)
                    {
                        animator.SetBool("CarryWalk", true);
                    }
                    else
                    {
                        animator.SetBool("Walk", true);
                    }

                    //進行方向を向く
                    transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
                    CurrentForward = transform.forward;
                }
            }
        }
    }
}
