using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SampleManager : MonoBehaviour
{
    public GameObject Option;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if(Option.activeInHierarchy)
            {
                Option.SetActive(false);
            }
            else
            {
                Option.SetActive(true);
            }
        }
    }
}
