using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [SerializeField] GameObject     fpsCam;             // カメラ
    [SerializeField] float      distance = 3f;    // 検出可能な距離

    // オブジェクトのTag
    string Blaze = "Blaze"; // 火のTag
    string WaterPoint = "WaterPoint"; // 消火栓のTag
    //string Water = "Aqua"; // 水のTag
    string DesObj = "DesObj"; // 破壊可能のTag
    //string MinorInjuries = "MinorInjuries"; // 軽症者のTag
    //string SeriousInjuries = "SeriousInjuries"; // 重症者のTag

    
    public static bool HosuStatus = false;//ホースを持っているか

    
    public GameObject Hosu;//ホースのオブジェクトを格納

    //破壊システム
    public GameObject DesSystemUI; //破壊システムのUI

    //消火器用
    public static bool SHold = false; //長押し判定

    private Animator animator;

    bool isCalledOnce = false;


    void Start ()
    {
        Hosu = this.transform.Find("shokaki").gameObject;
        //Hosu = this.transform.Find("Syoukaki").gameObject;
        if(HosuStatus == false)
        {
            Hosu.SetActive(HosuStatus);
        }

         fpsCam = GameObject.Find("FPSCamera");

        //アニメーション読み込み
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
            Debug.Log("HitObject : " + raycastHit.collider.gameObject.name);
            // HitしたオブジェクトのTag何か判定
            if (raycastHit.collider.gameObject.CompareTag(Blaze))
            {
                Debug.Log("炎");
            }
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
                Debug.Log("消火栓");
                /*if(Input.GetKeyDown("t")){
                    if(HosuStatus == false)
                    {
                        //消火栓をアクティブ
                        Debug.Log("消火栓使用中");
                        HosuStatus = true;
                        Hosu.SetActive(HosuStatus);
                    }else{
                        //消火栓を非アクティブ
                        Debug.Log("消火栓使用してない");
                        WaterHose.WaterStatus = false;
                        HosuStatus = false;
                        Hosu.SetActive(HosuStatus);
                    }
                }*/
            }
        }

        //消火器用スクリプト
        if (Input.GetKeyDown("t"))
        {
            if (HosuStatus == false)
            {
                if (!isCalledOnce)
                {
                    isCalledOnce = true;
                    animator.SetBool("take", isCalledOnce);
                }
                //消火栓をアクティブ
                Debug.Log("消火栓使用中");
                HosuStatus = true;
                Hosu.SetActive(HosuStatus);
            }
            else
            {
                isCalledOnce = false;
                animator.SetBool("take", isCalledOnce);
                //消火栓を非アクティブ
                Debug.Log("消火栓使用してない");
                WaterHose.WaterStatus = false;
                HosuStatus = false;
                Hosu.SetActive(HosuStatus);
            }
            SHold = true;
        }
        if (Input.GetKeyDown("t"))
        {
            SHold = false;
        }
        if (SHold)
        {
            Debug.Log("Hold");
        }
    }
}
