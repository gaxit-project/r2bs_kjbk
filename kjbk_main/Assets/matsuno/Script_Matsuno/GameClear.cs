using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{

    public SceneChange clear;                  //SceneChange����R�[�h�������Ă�����


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()                      //�Q�[���N���A�̃R�[�h
    {
        if (Input.GetKeyDown("o"))
        {
            clear.GameClear2();
        }
    }
}
