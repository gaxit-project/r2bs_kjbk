using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Number : MonoBehaviour
{
    // Start is called before the first frame update

    public int Number;
    void Start()
    {
        PlayerPrefs.SetInt("R_number", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ŒyÇÒ‚ğ¯•Ê‚·‚é•Ï”
    public void RNumber()
    {
        PlayerPrefs.SetInt("R_number", Number);
    }
}
