using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcon : MonoBehaviour
{
    public GameObject DesSystemUi;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            DesSysteM.test = true;
            DesSystemUi.SetActive(true);
        }
        if (Input.GetKeyDown("2"))
        {
            DesSystemUi.SetActive(false);
        }
        if (DesSysteM.test == false)
        {
            DesSystemUi.SetActive(false);
        }
    }
}
