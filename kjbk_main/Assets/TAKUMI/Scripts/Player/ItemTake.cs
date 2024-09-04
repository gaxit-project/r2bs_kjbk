using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTake : MonoBehaviour
{
    public bool[] ItemFlag = new bool[9];
    int ItemCount = 0;
    void Start()
    {
        for(int i = 0; i < ItemFlag.Length; i++)
        {
            ItemFlag[i] = false;
        }
        int rand = Random.Range(0, 9);
        ItemFlag[rand] = true;
        
    }


    void Update()
    {
        
    }
}
