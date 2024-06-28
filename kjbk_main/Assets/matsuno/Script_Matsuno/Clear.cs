using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clear : MonoBehaviour
{

    MeshRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent < MeshRenderer >();
        sr.material.color = sr.material.color - new Color32(0,0,0,255);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
