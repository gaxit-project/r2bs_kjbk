using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    #region 宣言
    // メインカメラ格納用
    public GameObject mainCamera;
    // サブカメラ格納用
    public GameObject subCamera;
    // カメラの状態
    private bool CameraStatus = false;
    // 通常速度，ダッシュ速度
    public float Speed, RunSpeed, Debuff;
    // 現在速度
    float CurrentSpeed;
    // 回転速度
    float RotateSpeed = 15f;

    //現在のスタミナ
    float Stamina = 1f;//0〜1で最大スタミナが1
    //スタミナ消費量
    public float StaminaDownSpeed = 5f;//5秒かけて無くなる
    //スタミナ回復猟
    public float StaminaUpSpeed = 10f;//110秒賭けて回復
    //スタミナ切れを起こしたか
    bool RunOut = false;

    // 移動速度低下割合
    float DebuffSpeed;

    // プレイヤーの状態
    bool House = PlayerRayCast.HosuStatus;
    bool Follow = RescueNPC.Follow;

    // 現在の進行方向
    public static Vector3 CurrentForward;

    // 移動しているか
    public static bool MoveStatus = false;

    // アニメーターコンポーネント
    private Animator animator;

    // ボタンの押下状態
    private bool IsPressedRun;

    // オーディオソース
    private AudioSource audiosource;

    // 入力アクション
    private InputAction MoveAction;
    // 移動入力の状態
    public static bool MoveInput;

    // 入力値
    float Yvalue;
    float Xvalue;

    #endregion

    #region PlayerInputコールバック
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
    #endregion

    #region 初期化
    private void Awake()
    {
        // オーディオソースを取得
        audiosource = GetComponent<AudioSource>();
    }

    void Start()
    {
        #region 取得・読み込み
        // メインカメラとサブカメラをそれぞれ取得
        mainCamera = GameObject.Find("Main Camera");
        subCamera = GameObject.Find("FPSCamera");

        // アニメーターコンポーネントを取得
        animator = GetComponent<Animator>();

        var pInput = GetComponent<PlayerInput>();
        // 現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        // アクションマップからアクションを取得
        MoveAction = actionMap["Move"];


        #endregion

        #region 変数初期化
        // メインカメラをアクティブにする
        mainCamera.SetActive(true);
        // サブカメラを非アクティブにする
        subCamera.SetActive(false);

        //スタミナ初期化
        Stamina = 1f;
        PlayerPrefs.SetFloat("Stamina", Stamina);

        // 移動入力受付状態
        MoveInput = true;
        // 入力ロック　0 = 許可: 1 = 拒否
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


        #region Lockが0の時入力許可
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
            //垂直方向と水平方向の入力を取得
            Xvalue = Input.GetAxisRaw("Horizontal");
            Yvalue = Input.GetAxisRaw("Vertical");
        }

        #region スタミナ切れ回復
        if(Stamina >= 1f)
        {
            RunOut = false;
        }else if(Stamina <= 0f)
        {
            RunOut = true;
        }
        #endregion

        #region 移動がないときのスタミナ回復
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            if (Stamina <= 1)
            {
                Stamina += Time.deltaTime / (StaminaUpSpeed = Follow ? 17f : 10f);
            }
            else//1に揃える
            {
                Stamina = 1f;
            }
            PlayerPrefs.SetFloat("Stamina", Stamina);
        }
        #endregion

        #region スタミナ切れが起こったか
        PlayerPrefs.SetInt("RunOut", RunOut ?  1 : 0);//起こったなら1
        #endregion

        #region 移動入力状態許可
        if (MoveInput)
        {
            #region MAP使用時等で動けなくする
            if (DesSystem.DesSystemStatus == true/*|| switchCamera.NiseMapON*/) // マップ表示時はプレイヤーは動けなくする
            {
                rb.velocity = Vector3.zero;
                animator.SetBool("Walk", false);
            }
            
            #endregion
            #region 消火器使用時
            else if (WaterHose.Hold)
            {

                MoveStatus = true;

                //位置を移動
                Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue).normalized * CurrentSpeed * DebuffSpeed;

                //進行方向を向く
                this.transform.position = posi;
                transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
                CurrentForward = transform.forward;
            }
            #endregion
            else
            {
                #region 入力がないとき
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
                #region 入力がある時
                {
                    #region ダッシュ状態判定
                    // ダッシュ状態判定
                    CurrentSpeed = IsPressedRun ? RunSpeed : Speed;
                    CurrentSpeed = RunOut ? Speed : CurrentSpeed;

                    #endregion

                    #region デバフ時移動倍率
                    // 移動速度にデバフがかかるかどうかを判定
                    DebuffSpeed = House || Follow || RunOut ? Debuff : 1;
                    #endregion

                    MoveStatus = true;

                    //位置を移動
                    Vector3 MoveDir = new Vector3(Xvalue, 0, Yvalue).normalized * CurrentSpeed * DebuffSpeed;
                    if (!audiosource.isPlaying && !(Time.timeScale == 0f))
                    {
                        audiosource.Play();
                    }
                    rb.velocity = MoveDir;

                    if (Follow)//重傷者を背負っている
                    {
                        animator.SetBool("CarryWalk", true);
                        #region 背負い時のスタミナの増減
                        //背負い時のスタミナ消費量
                        StaminaDownSpeed = 3f;//3秒かけて消費
                        //背負い時のスタミナ回復量
                        StaminaUpSpeed = 17f;//17秒かけて回復
                        if (CurrentSpeed == Speed)//歩き
                        {
                            #region 背負い歩き時のスタミナ
                            if (Stamina <= 1)
                            {
                                Stamina += Time.deltaTime / StaminaUpSpeed;
                            }
                            else//1に揃える
                            {
                                Stamina = 1f;
                            }
                            PlayerPrefs.SetFloat("Stamina", Stamina);
                            #endregion
                        }
                        else//ダッシュ
                        {
                            #region 背負いダッシュ時のスタミナ
                            if (Stamina >= 0)
                            {
                                Stamina -= Time.deltaTime / StaminaDownSpeed;
                            }
                            else//0に揃える
                            {
                                Stamina = 0f;
                            }
                            PlayerPrefs.SetFloat("Stamina", Stamina);
                            #endregion
                        }
                        #endregion
                    }
                    else//重傷者を背負っていない
                    {
                        animator.SetBool("Walk", true);
                        #region 平常時のスタミナの増減
                        //スタミナ消費量
                        StaminaDownSpeed = 5f;//5秒かけて無くなる
                        //スタミナ回復猟
                        StaminaUpSpeed = 10f;//110秒賭けて回復
                        if (CurrentSpeed == Speed)//歩き
                        {
                            #region 平常歩き時のスタミナ
                            if (Stamina <= 1)
                            {
                                Stamina += Time.deltaTime / StaminaUpSpeed;
                            }
                            else//1に揃える
                            {
                                Stamina = 1f;
                            }
                            PlayerPrefs.SetFloat("Stamina", Stamina);
                            #endregion
                        }
                        else//ダッシュ
                        {
                            #region 平常ダッシュ時のスタミナ
                            if (Stamina >= 0)
                            {
                                Stamina -= Time.deltaTime / StaminaDownSpeed;
                            }
                            else//0に揃える
                            {
                                Stamina = 0f;
                            }
                            PlayerPrefs.SetFloat("Stamina", Stamina);
                            #endregion
                        }
                        #endregion
                    }

                    //進行方向を向く
                    transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
                    CurrentForward = transform.forward;
                }
                #endregion
            }
        }
        #endregion
    }

}
