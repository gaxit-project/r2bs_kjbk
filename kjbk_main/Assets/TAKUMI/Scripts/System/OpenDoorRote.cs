using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorRote : MonoBehaviour
{
    #region 変数宣言

    // ドアのコライダー
    public BoxCollider door1;
    public BoxCollider door2;

    // ドアの回転速度
    float DoorSpeed = 10f;

    public Transform doorTransform; // ドアのTransform
    private Quaternion initialRotation; // ドアの初期回転
    private Quaternion targetRotation; // ドアの目標回転

    public bool isOpen = false;
    #endregion

    #region Startメソッド
    void Start()
    {
        #region 変数の初期化
        initialRotation = doorTransform.rotation; // ドアの初期回転を記録
        targetRotation = initialRotation; // 初期の目標回転はドアが閉じている状態
        #endregion
    }
    #endregion

    #region Updateメソッド
    void Update()
    {


        // ドアの回転を更新
        doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation, targetRotation, Time.deltaTime * DoorSpeed);

        if (Close())
        {
            isOpen = false;
        }
    }
    #endregion



    #region ドアの動作
    // キャラクターの位置に基づいてドアを開く
    public void OpenDoorTowards(bool Front, bool Back, bool conect)
    {
        if (!isOpen)
        {
            DoorColOnOff(false);
            isOpen = true;

            float con = conect ? -1 : 1;

            if (Front)
            {
                // プレイヤーがドアの右側にいる場合
                targetRotation = doorTransform.rotation * Quaternion.Euler(0, -90f * con, 0);
            }
            else if (Back)
            {
                // プレイヤーがドアの左側にいる場合
                targetRotation = doorTransform.rotation * Quaternion.Euler(0, 90f * con, 0);
            }
        }
    }

    // ドアを閉じる
    public void CloseDoor()
    {
        DoorColOnOff(true);
        targetRotation = initialRotation; // ドアを元の回転位置に戻す
    }
    #endregion

    void DoorColOnOff(bool isDoor)
    {
        door1.enabled = isDoor;
        door2.enabled = isDoor;
    }

    public bool Close()
    {
        if (doorTransform.rotation == initialRotation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
