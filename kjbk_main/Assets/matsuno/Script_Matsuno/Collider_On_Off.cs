using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_On_Off : MonoBehaviour
{

    BoxCollider FireCol;

    // Start is called before the first frame update
    public void Start()
    {
        FireCol = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame

    /// 
    /// 炎のコライダーをオンにする
    /// 


    public void OnCollisionEnter(Collision Hit)
    {
        if (Hit.gameObject.tag == "Player")
        {
            FireCol.enabled = false;
            Invoke(nameof(FireOn), 5f);
        }
    }


        /// 
        /// 炎のコライダーをオフにする
        /// 

    public void FireOn()
    {
        FireCol.enabled = true;
    }


        /// 
        /// 炎のコライダーオンにするためにn秒間待つ
        /// 
        //public void FireOn()
        //{
        //    Invoke(nameof(FireOn2), 5f);
        //}
    
}
