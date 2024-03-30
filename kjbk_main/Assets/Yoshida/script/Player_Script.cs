using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private float MoveSpeed = 40.0f;
    bool AI = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AI)
        {
            transform.position = new Vector3(20, 0, Mathf.Sin(Time.time) * MoveSpeed + 20);
        }
    }

    public void PlayerMove()
    {
        AI = true;
    }

    public void PlayerStop()
    {
        AI = false;
    }
}
