using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPlate : MonoBehaviour
{
    private GameObject player;
    private GameObject dark;
    

    void Start()
    {
        dark = GameObject.Find("Dark");
        player = GameObject.Find("Player"); 
    }
   
   void OnCollisionEnter(Collision player)
    {
        dark.SetActive(false);
    }
    void OnCollisionExit(Collision player)
    {
        dark.SetActive(true);
    }
 
}
