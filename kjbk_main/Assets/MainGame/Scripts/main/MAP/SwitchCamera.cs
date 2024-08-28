using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    private RescueCount CounterScript;
    public Camera mainCamera; //メインカメラ
    public Camera subCamera; //ビックマップのカメラ
    public bool map_status = false; //マップのボタンの処理用変数
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
    public GameObject MissionMap;
    private bool wasPaused;
    public Radio_ver4 Radio4;

    void Start()
    {
        MAPOFF.SetActive(false);
        MiniMAP.SetActive(false);
        MissionMap.SetActive(false);
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
        //マップ取得時に自動でマップを開く
        if (CounterScript.getNum() == 1 && initialMapStatusActivated)
        {
            StartCoroutine(ActivateInitialMapStatusWithDelay(5f));
        }

        if (pause.pause_status)
        {
            AllButton.SetActive(false);
            map_status = false;
            MAPOFF.SetActive(false);
            if (!MapON)
            {
                MiniMAPOFF.SetActive(true);
                MiniMAPOFF2.SetActive(true);
                NiseMapON = false;
            }
        }
        else
        {
            if (wasPaused != pause.pause_status)
            {
                AllButton.SetActive(true);
            }
            if (Map)
            {
                if (MapON)
                {
                    map_status = !map_status;
                }
                //偽マップの表示
                else if (!NiseMapON)
                {
                    MAPOFF.SetActive(true);
                    MiniMAPOFF.SetActive(false);
                    MiniMAPOFF2.SetActive(false);
                    NiseMapON = true;
                    AllButton.SetActive(false);
                    MissionMap.SetActive(true);
                }
                //偽マップを閉じる
                else
                {
                    MAPOFF.SetActive(false);
                    MiniMAPOFF.SetActive(true);
                    MiniMAPOFF2.SetActive(true);
                    NiseMapON = false;
                    MissionMap.SetActive(false);
                    if (!pause.pause_status)
                    {
                        {
                            AllButton.SetActive(true);
                        }
                    }

                }
            }

            //マップのステータス
            if (map_status)
            {
                subCamera.enabled = true;
                miniMap.SetActive(false);
                Ui.SetActive(Ui_status);
                Mkey.SetActive(true);
                AllButton.SetActive(false);
                MissionMap.SetActive(true);
            }
            else
            {
                subCamera.enabled = false;
                miniMap.SetActive(true);
                Ui.SetActive(false);
                Mkey.SetActive(false);
                if(MapON)
                {
                    MissionMap.SetActive(false);
                }
                if (MapON && !pause.pause_status)
                {
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
    }

        private IEnumerator ActivateInitialMapStatusWithDelay(float delay)
        {
            MapON = true;
            MiniMAPOFF.SetActive(false);
            MiniMAP.SetActive(true);
            MAPOFF.SetActive(false);
            MiniMAPOFF2.SetActive(true);
            MissionMap.SetActive(true);
            initialMapStatusActivated = false;
        yield return new WaitForSeconds(delay);
            if (hat1 != null)
            {
                Ui_status = true;
                Ui.SetActive(true);
                map_status = true;
            }
        Radio4.FirstStopPlayer = false;
    }
}

