using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{

    public SceneChange clear;                  //SceneChangeからコードを持ってくるやつ


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()                      //ゲームクリアのコード
    {
        if (Input.GetKeyDown("o"))
        {
            clear.GameClear2();
        }
    }
}
