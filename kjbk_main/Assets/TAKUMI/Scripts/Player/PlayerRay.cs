using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] Camera     fpsCam;             // カメラ
    [SerializeField] float      distance = 3f;    // 検出可能な距離

    // オブジェクトのTag
    string Blaze = "Blaze"; // 火のTag
    string WaterPoint = "WaterPoint"; // 消火栓のTag
    string Water = "Water"; // 水のTag
    string DesObj = "DesObj"; // 破壊可能のTag
    string MinorInjuries = "MinorInjuries"; // 軽症者のTag
    string SeriousInjuries = "SeriousInjuries"; // 重症者のTag

    //ホースを持っているか
    public static bool HouseStatus = false;

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
                    Destroy(raycastHit.collider.gameObject);
                }
            }
            if(raycastHit.collider.gameObject.CompareTag(WaterPoint)){
                Debug.Log("消火栓");
                //スペースキーが押されている間、サブカメラをアクティブにする
                if(Input.GetKeyDown("t")){
                    if(HouseStatus == false)
                    {
                        //消火栓をアクティブ
                        Debug.Log("消火栓使用中");
                        HouseStatus = true;
                    }else{
                        //消火栓を非アクティブ
                        Debug.Log("消火栓使用してない");
                        HouseStatus = false; 
                    }
                }
            }
        }
    }
}