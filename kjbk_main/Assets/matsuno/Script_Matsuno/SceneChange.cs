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

    public void GameOver()                  //�Q�[���I�[�o�[�ւ̃V�[���`�F���W
    {
        Debug.Log("GameOver");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Matsuno_GameOver");
    }

    public void GameClear2()                //�Q�[���N���A�ւ̃V�[���`�F���W
    {
        Debug.Log("GameClear2");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Matsuno_GameClear");
    }
}
