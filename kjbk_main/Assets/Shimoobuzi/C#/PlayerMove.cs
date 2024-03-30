using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{ 
      public float speed = 15f;
   private void FixedUpdate()
   {
        var angles = new Vector3(0,90,0);
        var direction = Quaternion.Euler(angles)* transform.forward;
        var veloX = speed * Input.GetAxisRaw("Horizontal")*direction;
        var veloZ = speed * Input.GetAxisRaw("Vertical") * transform.forward;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Rigidbody>().velocity = veloX/3 + veloZ/3;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = veloX + veloZ;
        }  
   }
}