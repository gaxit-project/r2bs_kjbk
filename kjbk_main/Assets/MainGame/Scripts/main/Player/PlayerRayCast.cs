using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRayCast : MonoBehaviour
{
    #region 取得・アタッチ
    [SerializeField] GameObject     fpsCam;             // カメラ
    [SerializeField] float      distance = 3f;    // 検出可能な距離

    public GameObject Hosu;//ホースのオブジェクトを格納

    private Animator animator;

    private InputAction TakeAction;
    #endregion

    #region Tag
    // オブジェクトのTag
    string Blaze = "Blaze"; // 火のTag
    string WaterPoint = "WaterPoint"; // 消火栓のTag
    string DesObj = "DesObj"; // 破壊可能のTag
    #endregion

    #region フラグ
    public static bool HosuStatus = false;//ホースを持っているか

    //消火器用
    public static bool SHold = false; //長押し判定

    bool isCalledOnce = false;
    #endregion

    #region 変数宣言

    float MaxWater = 100f;
    float capacity = 0f;
    #endregion


    void Start ()
    {
        #region 初期化
        HosuStatus = false;
        if (HosuStatus == false)
        {
            Hosu.SetActive(HosuStatus);
        }

        capacity = 0f;
        PlayerPrefs.SetFloat("capacity", capacity);
        #endregion

        #region 取得・読み込み
        fpsCam = GameObject.Find("FPSCamera");

        //アニメーション読み込み
        animator = GetComponent<Animator>();

        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        TakeAction = actionMap["Take"];
        #endregion
    }

    void Update()
    {
        bool Take = TakeAction.triggered;

        #region Rayを飛ばしてオブジェクトを取得
        // Rayはカメラの位置からとばす
        var rayStartPosition   = fpsCam.transform.position;
        // Rayはカメラが向いてる方向にとばす
        var rayDirection       = fpsCam.transform.forward.normalized;

        // Hitしたオブジェクト格納用
        RaycastHit raycastHit;

        // Rayを飛ばす（out raycastHit でHitしたオブジェクトを取得する）
        var isHit = Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance);
        #endregion

        #region 検出したら
        // なにかを検出したら
        if (isHit)
        {
            // LogにHitしたオブジェクト名を出力
            // HitしたオブジェクトのTag何か判定
            #region Hitしたものが消火器BOXの時
            if (raycastHit.collider.gameObject.CompareTag(WaterPoint) || 1 == PlayerPrefs.GetInt("YS")){
                //消火器用スクリプト
                if (Take )
                {
                    Audio.GetInstance().PlaySound(1);
                    if (HosuStatus == false)
                    {
                        #region 初回取得時
                        if (!isCalledOnce)
                        {
                            isCalledOnce = true;
                            animator.SetBool("take", isCalledOnce);
                        }
                        Invoke(nameof(DelayMethod), 1.0f);
                        //消火栓をアクティブ
                        PlayerPrefs.SetFloat("capacity",MaxWater);
                        //消火栓使用中
                        HosuStatus = true;
                        Hosu.SetActive(HosuStatus);
                        #endregion
                    }
                    else
                    {
                        #region 2回目以降(消火器補給)
                        if (isCalledOnce)
                        {
                            isCalledOnce = false;
                            animator.SetBool("take", isCalledOnce);
                        }
                        PlayerPrefs.SetFloat("capacity", MaxWater);
                        #endregion

                    }
                    SHold = true;
                }

            }
            #endregion
        }
        #endregion
    }

    #region 遅延処理
    //遅延処理
    void DelayMethod()
    {
        isCalledOnce = false;
        animator.SetBool("take", isCalledOnce);
    }
    #endregion
}
