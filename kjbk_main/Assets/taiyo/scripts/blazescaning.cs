using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blazescaning : MonoBehaviour
{
    public AudioSource audiosource;

    void Start()
    {
        //audiosource.Play();
    }


    void OnTriggerStay(Collider t)
    {
        if (t.CompareTag("Blaze"))
        {
            if (!audiosource.isPlaying)
            {
                Debug.Log("はいているよーーーーーーーーーーーーーーーーーー");
                audiosource.Play();
            }
        }
    }


}
