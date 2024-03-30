using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationY : MonoBehaviour
{
    private float currentAngleX;

     void Update()
    {
        transform.Rotate(Input.GetAxis("Mouse Y") * -3 , 0 , 0);
        currentAngleX = transform.localEulerAngles.x;
        if(currentAngleX > 180)
        {
           currentAngleX = currentAngleX - 360;
        }
       currentAngleX = Mathf.Clamp(currentAngleX, -90, 90);
       transform.localEulerAngles = new Vector3(currentAngleX, 0, 0);
    }

}
