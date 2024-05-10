using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//使ってない
public class GrowMap : MonoBehaviour
{
    // オブジェクトが大きくなる速度
    public float growthRate = 0.1f;

    // プレイヤーとの接触フラグ
    private bool playerTouching = false;

    void Update()
    {
        // プレイヤーが触れていないかつプレイヤーとの接触フラグがfalseの場合
        if (!playerTouching)
        {
            // オブジェクトを大きくする(Y軸は変化させない)
            Grow();
        }
    }

    // プレイヤーとの接触開始時に呼び出される
    void OnTriggerEnter(Collider other)
    {
        // 衝突したオブジェクトがプレイヤーである場合
        if (other.CompareTag("Player"))
        {
            // プレイヤーとの接触フラグをtrueにする
            playerTouching = true;
        }
    }

    // プレイヤーとの接触終了時に呼び出される
    void OnTriggerExit(Collider other)
    {
        // 衝突したオブジェクトがプレイヤーである場合
        if (other.CompareTag("Player"))
        {
            // プレイヤーとの接触フラグをfalseにする
            playerTouching = false;
        }
    }

    // オブジェクトを大きくする
    void Grow()
    {
        // オブジェクトのスケールを増加させるが、Y軸は変化させない
        Vector3 growth = new Vector3(growthRate, 0, growthRate);
        transform.localScale += growth * Time.deltaTime;
    }
}
