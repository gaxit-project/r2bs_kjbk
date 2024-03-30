using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeWater : MonoBehaviour
{
    public GameObject Child; //水のパーティクルを格納
    
    bool WaterStatus = false;
    void Start(){
        Child = gameObject.transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if (PlayerRay.HouseStatus == true){
            if(Input.GetMouseButton(1)){
                if (WaterStatus == false){
                    WaterStatus = true;
                }else{
                    WaterStatus = false;
                }
            }
        }else{
            WaterStatus = false;
        }

        if (WaterStatus == true){
            Child.SetActive(true);
        }else{
            Child.SetActive(false);
        }
    }
    
}
