using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_AI : MonoBehaviour
{
    private float MoveSpeed = 40.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(20, 0, Mathf.Sin(Time.time) * MoveSpeed + 20);
    }
}
