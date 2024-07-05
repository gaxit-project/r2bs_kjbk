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

    void Start()
    {
        mainCamera = Camera.main;
        CounterScript = FindObjectOfType<RescueCount>();
        rescueNPC = FindObjectOfType<RescueNPC>();
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;
        MapAction = actionMap["Map"];
    }

    void Update()
    {
        bool Map = MapAction.triggered;

        if (CounterScript.getNum() == 1 && initialMapStatusActivated)
        {
            StartCoroutine(ActivateInitialMapStatusWithDelay(2.0f));
        }

        if (Map || Input.GetKeyDown(KeyCode.M))
        {
            map_status = !map_status;
        }

        if (map_status)
        {
            subCamera.enabled = true;
            miniMap.SetActive(false);
            Ui.SetActive(Ui_status);
            Mkey.SetActive(true);
        }
        else
        {
            subCamera.enabled = false;
            miniMap.SetActive(true);
            Ui.SetActive(false);
            Mkey.SetActive(false);
        }

        if (rescueNPC != null && rescueNPC.IsItFollow())
        {
            initialMapStatusActivated = false;
            Ui_status = false;
            Ui.SetActive(false);
        }
    }

    private IEnumerator ActivateInitialMapStatusWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (hat1 != null)
        {
            Ui_status = true;
            Ui.SetActive(true);
            map_status = true;
        }
        initialMapStatusActivated = false;
    }
}

