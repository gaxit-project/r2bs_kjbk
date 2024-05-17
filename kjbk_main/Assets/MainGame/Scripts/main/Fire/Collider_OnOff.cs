using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_OnOff : MonoBehaviour
{

    BoxCollider FireCol;

    // Start is called before the first frame update
    void Start()
    {
        FireCol = this.GetComponent<BoxCollider>();
    }

    // Update is called once per frame

    /// 
    /// 炎のコライダーをオフにする
    /// 
    public void FireOff()
    {
        FireCol.enabled = false;
    }


    /// 
    /// 炎のコライダーオンにするためにn秒間待つ
    /// 
    public void FireOn()
    {
        Invoke(nameof(FireOn2), 5f);
    }


    /// 
    /// 炎のコライダーをオンにする
    /// 

    public void FireOn2()
    {
        FireCol.enabled = true;
    }
}
