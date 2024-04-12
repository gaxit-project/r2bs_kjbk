using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random : MonoBehaviour
{
    public float MoveRange; // 最初の位置から半径MoveRange以内の範囲をランダムに移動
    private float ChargeTime = 2.0f;
    private float TimeCount;
    private Vector3 Position;

    // Start is called before the first frame update
    void Start()
    {
        // 最初の位置を取得
        Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount += Time.deltaTime;

        // 自動前進
        transform.position += transform.forward * 20.0f * Time.deltaTime;

        // 範囲内かどうか判定し，範囲外であれば最初の位置に戻る
        Vector3 MovePosition = transform.position - Position;
        if (MovePosition.magnitude > MoveRange)
        {
            transform.position = Position;
        }

        // 一定時間経過したかどうか
        if (TimeCount > ChargeTime)
        {
            // 進路をランダムに変更する
            Vector3 course = new Vector3(0, Random.Range(0, 180), 0);
            transform.localRotation = Quaternion.Euler(course);

            // タイムカウントを0に戻す
            TimeCount = 0;
        }
    }
}
