using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCput : MonoBehaviour
{

    float ran, rand;                       // �����_���ȉ�]�p�x


    void Start()
    {
        this.transform.Rotate(-90f, 0f, 0f, Space.World); // �����_���Ȋp�x�ŉ�]
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ran = UnityEngine.Random.Range(0f, 360f); // �����_���ȉ�]�p�x�𐶐�
            this.transform.Rotate(0f, ran, 0f, Space.World); // �����_���Ȋp�x�ŉ�]
        }
    }
}
