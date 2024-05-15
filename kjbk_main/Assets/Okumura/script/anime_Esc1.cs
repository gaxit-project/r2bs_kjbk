using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anime_Esc1 : MonoBehaviour
{
    Animator anima;
    bool animeHold = false;

    void Start()
    {
        anima = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            animeHold = true;
        }
        else if (Input.GetKeyUp("m"))
        {
            animeHold = false;
        }
        if (animeHold)
        {
            anima.SetBool("FFcarry", true);
        }
        else
        {
            anima.SetBool("FFcarry", false);
        }
    }
}
