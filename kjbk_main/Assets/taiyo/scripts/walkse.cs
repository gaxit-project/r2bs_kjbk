using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey("w") || (Input.GetKey("a") || (Input.GetKey("s") || (Input.GetKey("d"))))) && !audiosource.isPlaying)
        {
            playwalkSE();
        }
    }
    public void playwalkSE()
    {
        audiosource.Play();
    }
}
