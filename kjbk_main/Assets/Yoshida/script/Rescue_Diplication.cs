using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Rescue_Diplication : MonoBehaviour
{
    public bool diplication;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getFlag()
    {
        return diplication;
    }

    public void OnFlag()
    {
        diplication = true;
    }

    public void OffFlag()
    {
        diplication = false;
    }
}
