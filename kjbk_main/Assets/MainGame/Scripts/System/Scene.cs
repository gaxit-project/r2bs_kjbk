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

    void CheckInstance() //�V���O���g��
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

    public void Title() //�^�C�g��
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("title");
    }

    public void GamePlay() //�Q�[�����
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
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
