using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public static Scene instance;

    void Awake()
    {
        CheckInstance();
    }

    void CheckInstance() //シングルトン
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Title() //タイトル
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("title");
    }

    public void GamePlay() //ゲーム画面
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void GameOver() //ゲームオーバー画面
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("gameover");
    }

    public void GameClear() //クリア画面
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("clear");
    }


    public void EndGame() //ゲーム終了
    {
#if UNITY_EDITOR //unity内でゲーム時
        UnityEditor.EditorApplication.isPlaying = false;
#else //ビルドされたゲームの時
            Application.Quit();
#endif
    }
}
