using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_count : MonoBehaviour
{
    private GameObject cnter;
    Rescue_Counter cnterScript;

    private int cnt;

    // Start is called before the first frame update
    void Start()
    {
        cnter = GameObject.Find("RescueCounter");
        cnterScript = cnter.GetComponent<Rescue_Counter>();
    }

    // Update is called once per frame
    void Update()
    {
        cnt = cnterScript.getNum();
        Debug.Log(cnt);
    }
}
