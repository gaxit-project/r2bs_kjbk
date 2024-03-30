using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private GameObject HighCamera;      //メインカメラ格納用
    private GameObject LowCamera;       //サブカメラ格納用 
    private GameObject Player1; 
    private GameObject Player2; 
    private int swich = 0;
 
    //呼び出し時に実行される関数
    void Start () 
    {
        //メインカメラとサブカメラをそれぞれ取得
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
        HighCamera = GameObject.Find("3rd_Person_Camera");
        LowCamera = GameObject.Find("1st_Person_Camera");
 
        //サブカメラを非アクティブにする
        LowCamera.SetActive(false); 
        Player2.SetActive(false);
	}
	
 
	//単位時間ごとに実行される関数
	void Update () {
        if(Input.GetKeyDown("space"))
        {
            swich++;
        }
		//スペースキーが押されている間、サブカメラをアクティブにする
        if(swich % 3 == 0)
        {
            //サブカメラをアクティブに設定
            Player1.SetActive(true);
            Player2.SetActive(false);
            HighCamera.SetActive(true);
            LowCamera.SetActive(false);
        }
        else if(swich % 3 == 1)
        {
            //メインカメラをアクティブに設定
            Player1.SetActive(true);
            Player2.SetActive(false);
            HighCamera.SetActive(false);
            LowCamera.SetActive(true);
        }
        else if(swich % 3 == 2)
        {
            Player1.SetActive(false);
            Player2.SetActive(true);
        }
	}
}
