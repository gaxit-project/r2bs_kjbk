using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScan : MonoBehaviour
{
    public bool InPlayer;
    void Start()
    {
        InPlayer = false;
    }


    void OnTriggerStay(Collider t)
    {
        if (t.CompareTag("Player"))
        {
            InPlayer = true;
        }
    }
    void OnTriggerExit(Collider t)
    {
        if (t.CompareTag("Player"))
        {
            InPlayer = false;
        }
    }
}
