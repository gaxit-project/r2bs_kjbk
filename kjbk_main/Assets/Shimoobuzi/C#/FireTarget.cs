using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTarget : MonoBehaviour
{
    private GameObject water;
    

    void Start()
    {
       water = GameObject.Find("Water"); 
    }
   
   void OnCollisionEnter(Collision water)
    {
        Destroy(gameObject, 0.5f);
    }
}
