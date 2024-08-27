using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HosuArea : MonoBehaviour
{
    private bool Near;
    private SphereCollider AreaCol;
    public GameObject Hosu;
    public PlayerRayCast PlayerRayCast;

    void Start()
    {
        AreaCol = this.GetComponent<SphereCollider>();
    }

    void Update()
    {
        
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            PlayerRayCast.HosuStatus = false;
            Hosu.SetActive(PlayerRayCast.HosuStatus);
        }

    }
}
