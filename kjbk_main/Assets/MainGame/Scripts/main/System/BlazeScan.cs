using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeScan : MonoBehaviour
{
    public bool InBlaze;
    void Start()
    {
        InBlaze = false;
    }

    void Update()
    {
    }

    void OnTriggerStay(Collider t)
    {
        if (t.CompareTag("Blaze"))
        {
            InBlaze = true;
        }
    }
}
