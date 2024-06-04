using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    public Camera mainCamera; //メインカメラ
    public Camera subCamera; //くそでかマップのカメラ
    bool map_status=false; //マップのボタンの処理用変数
    // Start is called before the first frame update

    private InputAction MapAction;
    void Start()
    {
        mainCamera=Camera.main;

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

        //キーボードのMが押されたら切り替える
        if (Map || Input.GetKeyDown(KeyCode.M)){

            if (map_status == true)
            {
                map_status = false;
                
            }
            else
            {
                map_status = true;
                
            }
        }

        //くそでかマップ表示
        if(map_status == true)
        {
            subCamera.enabled=true;
            //mainCamera.enabled=true;
            Debug.Log("表示");
            //Time.timeScale = 0.0f;
        }
        //くそでかマップ非表示
        else
        {
            //mainCamera.enabled=true;
            subCamera.enabled=false;
            Debug.Log("非表示");
            //Time.timeScale = 1.0f;
        }
    }
}
