using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blazescaning : MonoBehaviour
{
    private AudioSource audiosource;
    void Start()
    {
        
    }

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider t)
    {
        if(t.gameObject.tag == "Blaze")
        {
            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
        }
    }
}
