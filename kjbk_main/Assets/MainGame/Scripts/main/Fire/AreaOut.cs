using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOut : MonoBehaviour
{
    #region 炎がマップ外に出たら破壊
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blaze"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Blaze"))
        {
            Destroy(other.gameObject);
        }
    }
    #endregion
}
