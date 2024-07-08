using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeAYX : MonoBehaviour
{
    public GameObject button;
    public GameObject Xbutton;
    private RescueNPC rescueNPC;
    private RescueDiplication DiplicationScript;
    private GameObject Rescue;
    private bool Xb=false;
    // Start is called before the first frame update
    void Start()
    {
        Rescue = GameObject.Find("Rescue");
        rescueNPC = FindObjectOfType<RescueNPC>();
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        button.SetActive(false);
    }

    void Update()
    {
        if (rescueNPC.IsItFollow())
            {
                Xb=true;
                Xbutton.SetActive(false);
            }else{
                Xb=false;
            }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ButtonAYX"))
        {
            if(button != null){
                button.SetActive(true);
            }else if(!Xb && Xbutton != null){
                Xbutton.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ButtonAYX"))
        {
            if(button != null){
                button.SetActive(false);
            }else if(!Xb && Xbutton != null){
                Xbutton.SetActive(false);
            }
    }
    }

    void OnDestroy()
    {
        if (button != null)
        {
            button.SetActive(false);
        }
        if (Xbutton != null)
        {
            Xbutton.SetActive(false);
        }
    }
}
