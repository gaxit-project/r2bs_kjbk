using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_OnOff : MonoBehaviour
{

    BoxCollider FireCol;

    public void Start()
    {
        FireCol = this.GetComponent<BoxCollider>();
    }


    // 炎のコライダーをオンにする
    #region 炎のコライダーオン


    public void OnCollisionEnter(Collision Hit)
    {
        if (Hit.gameObject.tag == "Player")
        {
            FireCol.enabled = false;
            Invoke(nameof(FireOn), 5f);
        }
    }
    #endregion


    // 炎のコライダーをオフにする
    #region 炎のコライダーオフ

    public void FireOn()
    {
        FireCol.enabled = true;
    }
    #endregion
}
