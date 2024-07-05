using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManage : MonoBehaviour
{
    public GameObject Water;

    // Start is called before the first frame update
    void Start()
    {
        Water.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerRayCast.HosuStatus == true)
        {
            Water.SetActive(true);
        }
    }
}
