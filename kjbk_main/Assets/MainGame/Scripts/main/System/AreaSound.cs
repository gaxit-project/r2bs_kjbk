using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSound : MonoBehaviour
{
    public AudioSource AudioSource;
    public BlazeScan BlazeScan;
    public PlayerScan PlayerScan;

    void Update()
    {
        Debug.Log("InBlaze; " + BlazeScan.InBlaze);
        Debug.Log("InPlayer; " + PlayerScan.InPlayer);
        if (BlazeScan.InBlaze && PlayerScan.InPlayer)
        {
            if (!AudioSource.isPlaying)
            {
                AudioSource.Play();
            }
        }
        else
        {
            AudioSource.Stop();
        }
    }
}
