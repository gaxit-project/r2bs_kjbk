using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationX : MonoBehaviour
{
    void Update()
    {
       transform.Rotate(0, Input.GetAxis("Mouse X") *3 , 0 );  
    }
}
