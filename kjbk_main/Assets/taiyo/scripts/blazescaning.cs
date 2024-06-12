using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blazescaning : MonoBehaviour
{
    private AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider t)
    {
        if(t.gameObject.tag == "Blaze")
        {
            Debug.Log("‰Š‚ ‚è");
            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
        }
    }
}
