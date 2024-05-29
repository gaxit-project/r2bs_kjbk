using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    private bool Near;
    private bool NPCNear;
    private Animator animator;
    private BoxCollider doorCollider;

    private InputAction TakeAction;
    void Start()
    {
        Near = false;
        NPCNear = false;
        animator = GetComponentInChildren<Animator>();
        doorCollider = GetComponentInChildren<BoxCollider>();

        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        TakeAction = actionMap["Take"];
    }
    void Update()
    {
        bool Take = TakeAction.triggered;

        if ((Input.GetKeyDown(KeyCode.F) || Take) && Near)
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
