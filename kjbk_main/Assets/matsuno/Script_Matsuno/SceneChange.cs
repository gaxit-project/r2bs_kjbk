using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameOver()                  //ゲームオーバーへのシーンチェンジ
    {
        Debug.Log("GameOver");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Matsuno_GameOver");
    }

    public void GameClear2()                //ゲームクリアへのシーンチェンジ
    {
        Debug.Log("GameClear2");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Matsuno_GameClear");
    }
}
