using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    private RescueCount CounterScript;
    public Camera mainCamera; //メインカメラ
    public Camera subCamera; //くそでかマップのカメラ
    bool map_status = false; //マップのボタンの処理用変数
    public bool initialMapStatusActivated = true; 
    public bool Ui_status = false;
    private InputAction MapAction;
    public GameObject Ui;
    public GameObject Mkey;
    public GameObject miniMap;
    public GameObject hat1;
    private RescueNPC rescueNPC;
    private Pause pause;
    public bool MapON = false;
    public bool NiseMapON = false;
    public GameObject MAPOFF;
    public GameObject MiniMAPOFF;
    public GameObject MiniMAP;
    public GameObject MiniMAPOFF2;
    public GameObject AllButton;
    private bool wasPaused;

    void Start()
    {
        MAPOFF.SetActive(false);
        MiniMAP.SetActive(false);
        mainCamera = Camera.main;
        pause=FindObjectOfType<Pause>();
        CounterScript = FindObjectOfType<RescueCount>();
        rescueNPC = FindObjectOfType<RescueNPC>();
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;
        MapAction = actionMap["Map"];
        wasPaused = pause.pause_status;
    }

    void Update()
    {
        bool Map = MapAction.triggered;
        if (CounterScript.getNum() == 1 && initialMapStatusActivated)
        {
            StartCoroutine(ActivateInitialMapStatusWithDelay(0f));
        }

        if(pause.pause_status){
            AllButton.SetActive(false);
            map_status=false;
            MAPOFF.SetActive(false);
            if(!MapON){
            MiniMAPOFF.SetActive(true);
            MiniMAPOFF2.SetActive(true);
            NiseMapON = false;
            }
        }else{
            if (wasPaused != pause.pause_status)
            {
                AllButton.SetActive(true);
            }
        if (Map || Input.GetKeyDown(KeyCode.M))
        {
            if(MapON)
            {
                map_status = !map_status;
            }

            else if(!NiseMapON)
            {
                MAPOFF.SetActive(true);
                MiniMAPOFF.SetActive(false);
                MiniMAPOFF2.SetActive(false);  
                NiseMapON = true;
                AllButton.SetActive(false);
            }
            else
            {
                MAPOFF.SetActive(false);
                MiniMAPOFF.SetActive(true);
                MiniMAPOFF2.SetActive(true);
                NiseMapON = false;
                if(!pause.pause_status){
                AllButton.SetActive(true);
                }
            }

        }
        }

        if (map_status)
        {
            subCamera.enabled = true;
            miniMap.SetActive(false);
            Ui.SetActive(Ui_status);
            Mkey.SetActive(true);
            AllButton.SetActive(false);
        }
        else
        {
            subCamera.enabled = false;
            miniMap.SetActive(true);
            Ui.SetActive(false);
            Mkey.SetActive(false);
            if(MapON && !pause.pause_status){
            AllButton.SetActive(true);
            }
        }

        if (rescueNPC != null && rescueNPC.IsItFollow())
        {
            initialMapStatusActivated = false;
            Ui_status = false;
            Ui.SetActive(false);
        }
        wasPaused = pause.pause_status;
    }

    private IEnumerator ActivateInitialMapStatusWithDelay(float delay)
    {
        MapON = true;
        MiniMAPOFF.SetActive(false);
        MiniMAP.SetActive(true);
        MAPOFF.SetActive(false);
        MiniMAPOFF2.SetActive(true);
        initialMapStatusActivated = false;
        yield return new WaitForSeconds(delay);
        if (hat1 != null)
        {
            Ui_status = true;
            Ui.SetActive(true);
            map_status = true;
        }
    }
}

