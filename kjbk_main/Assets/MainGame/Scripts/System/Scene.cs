using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public static Scene Instance = null;
    public FadeSceneLoader fadeSceneLoader;
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
        //DontDestroyOnLoad(this.gameObject);
    }

    public void Title() //タイトル
    {
        Time.timeScale = 1.0f;
        fadeSceneLoader.CallCoroutine("main_title");
        
        //UnityEngine.SceneManagement.SceneManager.LoadScene("main_title");
    }

    public void GamePlay() //メインゲーム
    {
        fadeSceneLoader.CallCoroutine("main");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("maino");
    }

    public void GameOver() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main_gameover");
    }

    public void GameClear() //�N���A���
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main_clear");
    }
    public void GameResult() //リザルト
    {
        fadeSceneLoader.CallCoroutine("main_result");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("main_result");
    }


    public void EndGame() //Quit
    {
#if UNITY_EDITOR //unity���ŃQ�[����
        UnityEditor.EditorApplication.isPlaying = false;
#else //�r���h���ꂽ�Q�[���̎�
            Application.Quit();
#endif
    }
}
