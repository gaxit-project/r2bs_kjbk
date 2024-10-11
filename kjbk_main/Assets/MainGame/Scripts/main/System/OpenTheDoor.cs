using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenTheDoor : MonoBehaviour
{

    #region 変数宣言
    // プレイヤーやNPCが近くにいるかどうかのフラグ
    private bool Near;
    private bool NPCNear;

    //ドアの回転
    public OpenDoorRote DoorRote;

    // ドアの回転方向
    public FrontCol FrontCol;
    public BackCol BackCol;


    // ドアが開いているかどうかのフラグ
    public bool DoorOpen = false;

    // 2枚ドアか
    public GameObject ConectDoor;
    public bool Conect = false;


    #endregion

    #region Startメソッド
    // Startは最初のフレームの前に1度呼び出されます
    void Start()
    {
        #region 変数の初期化
        Near = false;
        NPCNear = false;

        if ( ConectDoor != null )
        {
            Conect = true;
        }
        #endregion
    }
    #endregion

    void Update()
    {
        #region ドアの状態更新
        if (Near || NPCNear)
        {

            DoorOpen = true;
            // ドアをプレイヤーが優先して開けるようにする

            if (Near || (!Near && NPCNear) && DoorRote.Close()) // プレイヤーが優先される
            {
                DoorRote.OpenDoorTowards(FrontCol.Front, BackCol.Back, Conect);
            }
            else if (NPCNear && DoorRote.Close())
            {
                DoorRote.OpenDoorTowards(FrontCol.Front, BackCol.Back, Conect);
            }

        }
        else
        {
            DoorRote.CloseDoor(); // ドアを閉じる
            FrontCol.Front = false;
            BackCol.Back = false;
        }




        #endregion
    }

    #region コライダーのトリガー処理
    // トリガーに入ったときの処理
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = true;  // プレイヤーが近くにいる
        }

        if (col.tag == "MinorInjuries")
        {
            NPCNear = true;  // NPCが近くにいる
        }

    }

    // トリガーから出たときの処理
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Near = false;  // プレイヤーが離れた
        }

        if (col.tag == "MinorInjuries")
        {
            NPCNear = false;  // NPCが離れた
        }
    }
    #endregion
}
