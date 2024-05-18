using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool Near;
    private bool NPCNear;
    private Animator animator;
    private BoxCollider doorCollider;
    void Start()
    {
        Near = false;
        NPCNear = false;
        animator = GetComponentInChildren<Animator>();
        doorCollider = GetComponentInChildren<BoxCollider>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Near)
        {
            animator.SetBool("Open", true);
            //Audio.Instance.PlaySound(0);
            doorCollider.enabled = false;
        }

        if (NPCNear)
        {
            animator.SetBool("Open", true);
            //Audio.Instance.PlaySound(0);
            doorCollider.enabled = false;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = true;
        }

        if (col.tag == "MinorInjuries")
        {
            NPCNear = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = false;
            animator.SetBool("Open", false);
            //Audio.Instance.PlaySound(1);
            doorCollider.enabled = true;
        }

        if (col.tag == "Minorlnjuries")
        {
            NPCNear = false;
            animator.SetBool("Open", false);
            //Audio.Instance.PlaySound(1);
            doorCollider.enabled = true;
        }
    }
}
