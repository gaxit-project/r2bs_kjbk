using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCol : MonoBehaviour
{
    public OpenTheDoor OpenTheDoor;
    public FrontCol FrontCol;
    public bool Back = false;

    // ��]�p��90�x



    void OnTriggerEnter(Collider other)
    {
        if (OpenTheDoor.DoorOpen)
        {
            if ((other.CompareTag("Player") || other.CompareTag("MinorInjuries")) && !FrontCol.Front)
            {
                Back = true;
            }
        }
    }
}
