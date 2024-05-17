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

    public void Title() //�^�C�g��
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main_title");
    }

    public void GamePlay() //�Q�[�����
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }

    public void GameOver() //�Q�[���I�[�o�[���
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("gameover");
    }

    public void GameClear() //�N���A���
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("clear");
    }


    public void EndGame() //�Q�[���I��
    {
#if UNITY_EDITOR //unity���ŃQ�[����
        UnityEditor.EditorApplication.isPlaying = false;
#else //�r���h���ꂽ�Q�[���̎�
            Application.Quit();
#endif
    }
}
