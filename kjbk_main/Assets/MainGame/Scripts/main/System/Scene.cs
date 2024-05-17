using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public static Scene Instance = null;

    public static Scene GetInstance()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<Scene>();
        }
        return Instance;
    }
    private void Awake()
    {
        if (this != GetInstance())
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Title() //タイトル
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main_title");
    }

    public void GamePlay() //ゲーム画面
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
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
