using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorInjuryFixed : MonoBehaviour
{
    public GameObject player;//FFを読み込む
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        //初期のポジション保存
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //追従
        if(player != null){
        transform.position = this.player.transform.position + offset;
        transform.rotation = Quaternion.Euler(90f, 0f, 0f); 
        }
    }
}
