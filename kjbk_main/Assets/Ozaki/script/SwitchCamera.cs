using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    private RescueCount CounterScript;
    public Camera mainCamera; //メインカメラ
    public Camera subCamera; //くそでかマップのカメラ
    bool map_status=false; //マップのボタンの処理用変数
    bool initialMapStatusActivated = false; 
    private InputAction MapAction;
    public GameObject Ui;
    public GameObject Mkey;
    private RescueNPC rescueNPC;
    void Start()
    {
        mainCamera=Camera.main;
        CounterScript = FindObjectOfType<RescueCount>();
        rescueNPC = FindObjectOfType<RescueNPC>();
        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        MapAction = actionMap["Map"];
    }

    // Update is called once per frame
    void Update()
    {
        bool Map = MapAction.triggered;

        if (CounterScript.getNum() == 1 && !initialMapStatusActivated)
        {
            StartCoroutine(ActivateInitialMapStatusWithDelay(2.0f));
            /*map_status = true;
            initialMapStatusActivated = true;*/
        }

        //キーボードのMが押されたら切り替える
        if (Map || Input.GetKeyDown(KeyCode.M))
        {// 初回アクティベーションの後のみトグルを許可
            //if (initialMapStatusActivated) {
                map_status = !map_status;
            //}
        }

        //くそでかマップ表示
        if(map_status == true)
        {
            subCamera.enabled=true;
            //Ui.SetActive(true);
            Mkey.SetActive(true);
        }
        //くそでかマップ非表示
        else
        {
            subCamera.enabled=false;
            //Ui.SetActive(false);
            Mkey.SetActive(false);
        }

        if (rescueNPC != null && rescueNPC.IsItFollow())
        {
            Ui.SetActive(false);
            //Ui=null;
        }
    }
    private IEnumerator ActivateInitialMapStatusWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Ui.SetActive(true);
        map_status = true;
        initialMapStatusActivated = true;
    }
}
