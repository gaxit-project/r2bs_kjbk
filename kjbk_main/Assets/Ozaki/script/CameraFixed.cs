using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixed : MonoBehaviour
{
    // Start is called before the first frame update
    //ミニマップカメラにつける
    private GameObject player;//FFを読み込む
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーのオブジェクト情報を格納
        this.player = GameObject.Find("FF");
        
        //初期のポジション保存
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //追従
        transform.position = this.player.transform.position + offset;
    }
}
