using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRayCast : MonoBehaviour
{
    [SerializeField] GameObject     fpsCam;             // カメラ
    [SerializeField] float      distance = 3f;    // 検出可能な距離

    // オブジェクトのTag
    string Blaze = "Blaze"; // 火のTag
    string WaterPoint = "WaterPoint"; // 消火栓のTag
    string DesObj = "DesObj"; // 破壊可能のTag

    
    public static bool HosuStatus = false;//ホースを持っているか

    
    public GameObject Hosu;//ホースのオブジェクトを格納
    //public GameObject HosuObj;//ホースのオブジェクトを格納

    //破壊システム
    public GameObject DesSystemUI; //破壊システムのUI

    //消火器用
    public static bool SHold = false; //長押し判定

    private Animator animator;

    bool isCalledOnce = false;

    float MaxWater = 100f;
    float capacity = 0f;

    private InputAction TakeAction;


    void Start ()
    {
        HosuStatus = false;
        if (HosuStatus == false)
        {
            Hosu.SetActive(HosuStatus);
        }

         fpsCam = GameObject.Find("FPSCamera");

        //アニメーション読み込み
        animator = GetComponent<Animator>();

        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        TakeAction = actionMap["Take"];

        capacity = 0f;

        PlayerPrefs.SetFloat("capacity", capacity);
    }

    void Update()
    {
        bool Take = TakeAction.triggered;

        // Rayはカメラの位置からとばす
        var rayStartPosition   = fpsCam.transform.position;
        // Rayはカメラが向いてる方向にとばす
        var rayDirection       = fpsCam.transform.forward.normalized;

        // Hitしたオブジェクト格納用
        RaycastHit raycastHit;

        // Rayを飛ばす（out raycastHit でHitしたオブジェクトを取得する）
        var isHit = Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance);
        
        // Debug.DrawRay (Vector3 start(rayを開始する位置), Vector3 dir(rayの方向と長さ), Color color(ラインの色));
        Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);
        
        // なにかを検出したら
        if (isHit)
        {
            // LogにHitしたオブジェクト名を出力
            // HitしたオブジェクトのTag何か判定

            if(raycastHit.collider.gameObject.CompareTag(DesObj)){
                Debug.Log("破壊可能オブジェクト");
                if(Input.GetKeyDown("g")){
                    DesSystem.DesSystemStatus = true;
                    DesSystemUI.SetActive(true);
                }
                if(DesSystem.DesSystemStatus == false)
                {
                    if(DesSystem.DesSystemInput == true)
                    {
                        Destroy(raycastHit.collider.gameObject);
                    }
                    DesSystemUI.SetActive(false);
                    DesSystem.DesSystemInput = false;
                    DesSystem.DesSystemStatus = false;
                }

            }
            if(raycastHit.collider.gameObject.CompareTag(WaterPoint)){
                //消火器用スクリプト
                if (Take )
                {

                    if (HosuStatus == false)
                    {
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
                    }
                    else
                    {
                        if (isCalledOnce)
                        {
                            isCalledOnce = false;
                            animator.SetBool("take", isCalledOnce);
                        }
                        PlayerPrefs.SetFloat("capacity", MaxWater);

                    }
                    SHold = true;
                }


            }
        }
    }

    void DelayMethod()
    {
        isCalledOnce = false;
        animator.SetBool("take", isCalledOnce);
    }
}
