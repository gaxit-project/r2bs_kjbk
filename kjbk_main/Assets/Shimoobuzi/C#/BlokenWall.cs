using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlokenWall : MonoBehaviour
{
    private bool Near = false;
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.F) && Near)
        {
            Destroy(gameObject,0f);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = true;
        }
    }
 
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = false;
        }
    }
}
