using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeAYX : MonoBehaviour
{
    public GameObject YDbutton;
    public GameObject YSbutton;
    public GameObject Abutton;
    public GameObject Xbutton;
    /*public GameObject YDTwobutton;
    public GameObject YSTwobutton;
    public GameObject ATwobutton;
    public GameObject XTwobutton;*/
    private RescueNPC rescueNPC;
    private RescueDiplication DiplicationScript;
    private GameObject Rescue;
    private bool Xb = false;
    private bool x=false;
    public int cnt = 0;

    void Start()
    {
        // Assign Rescue GameObject and get components
        Rescue = GameObject.Find("Rescue");
        rescueNPC = FindObjectOfType<RescueNPC>();
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        // ObjectMonitorのイベントを登録
        ObjectMonitor.OnObjectDestroyed += HandleObjectDestroyed;

        // Deactivate all buttons initially
        DeactivateAllButtons();
    }

    void Update()
    {

        if (rescueNPC.IsItFollow())
        {
            
            Xb = true;
            if(Xbutton != null){
            Xbutton.SetActive(false);        
            }
            /*if(XTwobutton != null){
            XTwobutton.SetActive(false);
            }*/
            
        }
        else
        {
            Xb = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ButtonAYX"))
        {
            cnt++;
            switch (other.gameObject.name)
            {
                
                case "DoorButton":
                    /*if (cnt == 1 && YDbutton != null) YDbutton.SetActive(true);
                    else if (cnt == 2 && YDTwobutton != null) YDTwobutton.SetActive(true);*/
                    if(!x){
                    DeactivateAllButtons();
                    if (YDbutton != null) YDbutton.SetActive(true);
                    x=true;
                    }
                    break;
                case "SyoukaButton":
                    /*if (cnt == 1 && YSbutton != null) YSbutton.SetActive(true);
                    else if (cnt == 2 && YSTwobutton != null) YSTwobutton.SetActive(true);*/
                    if(!x){
                    DeactivateAllButtons();
                    if(YSbutton != null)YSbutton.SetActive(true);
                    x=true;
                    }
                    break;
                case "AButton":
                    /*if (cnt == 1 && Abutton != null) Abutton.SetActive(true);
                    else if (cnt == 2 && ATwobutton != null) ATwobutton.SetActive(true);*/
                    if(!x){
                    DeactivateAllButtons();
                    if (Abutton != null) Abutton.SetActive(true);
                    x=true;
                    }
                    break;
                case "XButton":
                    /*if (cnt == 1 && !Xb && Xbutton != null) Xbutton.SetActive(true);
                    else if (cnt == 2 && !Xb && XTwobutton != null) XTwobutton.SetActive(true);*/
                    if (!Xb && Xbutton != null) Xbutton.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ButtonAYX"))
        {
            cnt = cnt-1;
            DeactivateButton(other.gameObject.name);
        }
    }

    void OnDestroy()
    {
        ObjectMonitor.OnObjectDestroyed -= HandleObjectDestroyed;
        DeactivateAllButtons();
    }

    void HandleObjectDestroyed(string buttonName)
    {
        cnt=0;
        DeactivateButton(buttonName);
    }

    private void DeactivateAllButtons()
    {
        if(YDbutton != null){
        YDbutton.SetActive(false);
        }
        /*if(YDTwobutton != null){
        YDTwobutton.SetActive(false);
        }*/
        if(YSbutton != null){
        YSbutton.SetActive(false);
        }
        /*if(YSTwobutton != null){
        YSTwobutton.SetActive(false);
        }*/
        if(Abutton != null){
        Abutton.SetActive(false);
        }
        /*if(ATwobutton != null){
        ATwobutton.SetActive(false);
        }*/
        if (!Xb){ 
        if(Xbutton != null){
        Xbutton.SetActive(false);
        }
        /*if(XTwobutton != null){
        XTwobutton.SetActive(false);
        }*/
        }
        x = false;
    }

    private void DeactivateButton(string buttonName)
    {
        switch (buttonName)
        {
            case "DoorButton":
                if (YDbutton != null) YDbutton.SetActive(false);
                x=false;
                /*if (YDTwobutton != null) YDTwobutton.SetActive(false);*/
                break;
            case "SyoukaButton":
                if (YSbutton != null) YSbutton.SetActive(false);
                x=false;
                /*if (YSTwobutton != null) YSTwobutton.SetActive(false);*/
                break;
            case "AButton":
                if (Abutton != null) Abutton.SetActive(false);
                x=false;
                /*if (ATwobutton != null) ATwobutton.SetActive(false);*/
                break;
            case "XButton":
                if (Xbutton != null && !Xb) Xbutton.SetActive(false);
                /*if (XTwobutton != null) XTwobutton.SetActive(false);*/
                break;
            default:
                break;
        }
    }
}
