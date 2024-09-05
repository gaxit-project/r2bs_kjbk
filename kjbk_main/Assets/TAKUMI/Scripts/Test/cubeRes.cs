using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeRes : MonoBehaviour
{

    [SerializeField] HoldGauge HoldGauge;
    [SerializeField] private GameObject cube;


    // Update is called once per frame
    void Update()
    {
        if (HoldGauge != null && HoldGauge.gaugeStatus)
        {
            cube.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 255);
            Debug.Log("”½‰ž");
        }else
        {
            cube.GetComponent<Renderer>().material.color = new Color32(248, 168, 133, 255);
        }
    }
}
