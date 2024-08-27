using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    private bool Near;
    private bool NPCNear;
    private Animator animator;
    public BoxCollider doorCollider;

    private InputAction TakeAction;
    bool DoorOpen=false;
    void Start()
    {
        Near = false;
        NPCNear = false;
        animator = GetComponentInChildren<Animator>();
        //doorCollider = GetComponentInChildren<BoxCollider>();

        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        TakeAction = actionMap["Take"];
    }
    void Update()
    {
        bool Take = TakeAction.triggered;

        if ( Near)
        {
            doorCollider.enabled = false;
            animator.SetBool("Open", true);
            //Audio.Instance.PlaySound(0);
        }

        if (NPCNear)
        {          
            doorCollider.enabled = false;
            animator.SetBool("Open", true);
            //Audio.Instance.PlaySound(0);
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

        if (col.tag == "MinorInjuries")
        {
            NPCNear = false;
            animator.SetBool("Open", false);
            doorCollider.enabled = true;
            //Audio.Instance.PlaySound(1);

        }
    }

    void DoorOpenONOFF()
    {
        animator.SetBool("Open", false);
        doorCollider.enabled = true;
        DoorOpen = false;
    }
}
