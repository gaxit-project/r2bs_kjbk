using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHose : MonoBehaviour
{
    public GameObject Child; //水のパーティクルを格納
    
    public static bool WaterStatus = false; //放水状態
    public static bool Hold = false; //長押し判定

    public GameObject FF;

    private Animator animator;

    bool isOnes = false;

    private void OnEnable()
    {
        Debug.Log("aaaa");
        WaterStatus = false;
        Hold = false;
        WaterCannon(WaterStatus);
    }
    void Start(){
        Child = gameObject.transform.GetChild(0).gameObject;
        //アニメーション読み込み
        animator = FF.GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Hold = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Hold = false;
        }
        if (Hold)
        {
            Debug.Log("Hold");
        }
        if (PlayerRayCast.HosuStatus == true)
        {
            Debug.Log("ホースは持っている");
            if (Hold)
            {
                if (!isOnes)
                {
                    isOnes = true;
                    animator.SetBool("Gun", isOnes);
                    
                }
                WaterStatus = true;

            }
            else
            {
                WaterStatus = false;
                if (isOnes)
                {
                    isOnes = false;
                    animator.SetBool("Gun", isOnes);
                }
            }
        }
        else
        {
            WaterStatus = false;
        }
        WaterCannon(WaterStatus);

    }
    void WaterCannon(bool WaterStatus)
    {
        if (WaterStatus == true)
        {
            Child.SetActive(true);
        }
        else
        {
            Child.SetActive(false);
        }
    }
}
