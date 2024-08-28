using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    #region 変数宣言
    private RescueCount CounterScript; // 救助カウントスクリプト
    public Camera mainCamera; // メインカメラ
    public Camera subCamera; // ビックマップのカメラ
    public bool map_status = false; // マップのボタンの処理用変数
    public bool initialMapStatusActivated = true; // 初期マップステータス
    public bool Ui_status = false; // UIステータス
    private InputAction MapAction; // マップ操作
    public GameObject Ui; // UIオブジェクト
    public GameObject Mkey; // Mキーオブジェクト
    public GameObject miniMap; // ミニマップ
    public GameObject hat1; // 帽子オブジェクト
    private RescueNPC rescueNPC; // 救助NPC
    private Pause pause; // ポーズスクリプト
    public bool MapON = false; // マップONフラグ
    public bool NiseMapON = false; // 偽マップONフラグ
    public GameObject MAPOFF; // マップOFFオブジェクト
    public GameObject MiniMAPOFF; // ミニマップOFFオブジェクト
    public GameObject MiniMAP; // ミニマップオブジェクト
    public GameObject MiniMAPOFF2; // ミニマップOFFオブジェクト2
    public GameObject AllButton; // 全ボタンオブジェクト
    public GameObject MissionMap; // ミッションマップ
    private bool wasPaused; // ポーズ状態フラグ
    public Radio_ver4 Radio4; // ラジオスクリプト
    #endregion

    #region 初期化
    void Start()
    {
        MAPOFF.SetActive(false);
        MiniMAP.SetActive(false);
        MissionMap.SetActive(false);
        mainCamera = Camera.main;
        pause = FindObjectOfType<Pause>();
        CounterScript = FindObjectOfType<RescueCount>();
        rescueNPC = FindObjectOfType<RescueNPC>();
        var pInput = GetComponent<PlayerInput>();
        var actionMap = pInput.currentActionMap;
        MapAction = actionMap["Map"];
        wasPaused = pause.pause_status;
    }
    #endregion

    void Update()
    {
        #region マップ初期ステータス処理
        bool Map = MapAction.triggered;
        if (CounterScript.getNum() == 1 && initialMapStatusActivated)
        {
            StartCoroutine(ActivateInitialMapStatusWithDelay(5f));
        }
        #endregion

        #region ポーズ中の処理
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
        #endregion

        #region ポーズ解除時の処理
        else
        {
            if (wasPaused != pause.pause_status)
            {
                AllButton.SetActive(true);
            }

            #region マップ切り替え処理
            if (Map)
            {
                if (MapON)
                {
                    map_status = !map_status;
                }
                else if (!NiseMapON)
                {
                    MAPOFF.SetActive(true);
                    MiniMAPOFF.SetActive(false);
                    MiniMAPOFF2.SetActive(false);
                    NiseMapON = true;
                    AllButton.SetActive(false);
                    MissionMap.SetActive(true);
                }
                else
                {
                    MAPOFF.SetActive(false);
                    MiniMAPOFF.SetActive(true);
                    MiniMAPOFF2.SetActive(true);
                    NiseMapON = false;
                    MissionMap.SetActive(false);
                    if (!pause.pause_status)
                    {
                        AllButton.SetActive(true);
                    }
                }
            }
            #endregion

            #region マップステータス処理
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
                if (MapON)
                {
                    MissionMap.SetActive(false);
                }
                if (MapON && !pause.pause_status)
                {
                    AllButton.SetActive(true);
                }
            }
            #endregion

            #region 救助NPCのフォロー処理
            if (rescueNPC != null && rescueNPC.IsItFollow())
            {
                initialMapStatusActivated = false;
                Ui_status = false;
                Ui.SetActive(false);
            }
            #endregion

            wasPaused = pause.pause_status;
        }
        #endregion
    }

    #region マップ自動表示処理
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
    #endregion
}
