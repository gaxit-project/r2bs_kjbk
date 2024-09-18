using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCol : MonoBehaviour
{
    public OpenTheDoor OpenTheDoor;
    public BackCol BackCol;
    public bool Front = false;

    // ��]�p��-90�x

    void OnTriggerEnter(Collider other)
    {
        if (OpenTheDoor.DoorOpen)
        {
            if ((other.CompareTag("Player") || other.CompareTag("MinorInjuries")) && !BackCol.Back)
            {
                Front = true;
            }
        }
    }
}
