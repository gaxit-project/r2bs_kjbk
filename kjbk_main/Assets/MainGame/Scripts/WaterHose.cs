using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHose : MonoBehaviour
{
    public GameObject Child; //水のパーティクルを格納
    
    bool WaterStatus = false; //放水状態
    bool Hold = false; //長押し判定

    void Start(){
        Child = gameObject.transform.GetChild(0).gameObject;
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
                WaterStatus = true;
            }
            else
            {
                WaterStatus = false;
            }
        }
        else
        {
            WaterStatus = false;
        }

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
