using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
    public bool test_status = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button1_test()
    {
        Debug.Log("ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚Ü‚µ‚½");
        if (test_status)
        {
            test_status = false;
            Debug.Log("false");
        }
        else
        {
            test_status = true;
            Debug.Log("true");
        }
    }
}
