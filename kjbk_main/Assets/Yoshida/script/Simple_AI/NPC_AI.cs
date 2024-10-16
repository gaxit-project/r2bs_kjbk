using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class NPC_AI : MonoBehaviour
{
    [SerializeField] public bool Severe = false;
    [SerializeField] bool interact = false;
    private float MoveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (interact == false && Severe == false)
        {
            transform.position = new Vector3(Mathf.Sin(Time.time) * MoveSpeed + 10, 0, 60);
        }
    }

    public void MoveNPC()
    {
        interact = true;
    }

    void OnCollisionStay(UnityEngine.Collision collision)
    {


        if (collision.gameObject.name == "RescuePoint")
        {
            interact = false;
        }
    }
}
