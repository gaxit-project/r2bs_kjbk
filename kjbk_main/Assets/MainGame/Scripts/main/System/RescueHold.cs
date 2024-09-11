using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueHold : MonoBehaviour
{
    public HoldGauge holdgauge;



    // Start is called before the first frame update
    void Start()
    {
        holdgauge.HoldInIt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
