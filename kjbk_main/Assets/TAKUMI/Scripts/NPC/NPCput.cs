using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCput : MonoBehaviour
{

    float ran, rand;                       // ランダムな回転角度


    void Start()
    {
        this.transform.Rotate(-90f, 0f, 0f, Space.World); // ランダムな角度で回転
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ran = UnityEngine.Random.Range(0f, 360f); // ランダムな回転角度を生成
            this.transform.Rotate(0f, ran, 0f, Space.World); // ランダムな角度で回転
        }
    }
}
