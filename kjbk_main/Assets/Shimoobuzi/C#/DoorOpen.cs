using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private bool Near;
    private Animator animator;
    private BoxCollider doorCollider;
    void Start()
    {
        Near = false;
        animator = GetComponentInChildren<Animator>();
        doorCollider = GetComponentInChildren<BoxCollider>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Near)
        {
            animator.SetBool("Open", true);
            doorCollider.enabled = false;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = true;
        }
    }
 
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = false;
            animator.SetBool("Open", false);
            doorCollider.enabled = true;
        }
    }
}
