using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound1 : MonoBehaviour
{
    public AudioClip sound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioSource.PlayOneShot(sound);
        }
    }
}
