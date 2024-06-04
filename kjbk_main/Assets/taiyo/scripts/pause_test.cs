using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    public GameObject pause;
    public GameObject soundsetting;
    public GameObject backtitle;
    bool pause_status;

    public Button sound;
    public Button pausemenu;
    public Button titleback;

    // Start is called before the first frame update
    void Start()
    {
        sound = GameObject.Find("/Canvas/Button1").GetComponent<Button>();
        pausemenu = GameObject.Find("/Canvas/Button2").GetComponent<Button>();
        titleback = GameObject.Find("/Canvas/Button3").GetComponent<Button>();

        pause.SetActive(false);
        soundsetting.SetActive(false);
        backtitle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("押されました");
            pause.SetActive(!pause.activeSelf);

            if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
                pause.SetActive(false);
                soundsetting.SetActive(false);
                backtitle.SetActive(false);
                
            }
            else
            {
                Time.timeScale = 0.0f;
                pausemenu.Select();
            }
        }

        //SEテスト用（
        if (Input.GetKeyDown("o"))
        {
            AudioManager.GetInstance().PlaySound(3);
        }

        //音止める
        if (Input.GetKeyDown("z"))
        {
            AudioManager.GetInstance().StopSound();
        }
    }

    public void EP()
    {
        pause.SetActive(false);
        //Debug.Log("ゲーム中です");
        Time.timeScale = 1.0f;
    }
    public void setsound()
    {
         soundsetting.SetActive(!soundsetting.activeSelf);
         pause.SetActive(!pause.activeSelf);
        if (soundsetting.activeSelf == true)
        {
            sound.Select();
        }
        if(pause.activeSelf == true)
        {
            pausemenu.Select();
        }
    }
    public void title()
    {
        pause.SetActive(!pause.activeSelf);
        backtitle.SetActive(!backtitle.activeSelf);
        if(backtitle.activeSelf == true)
        {
            titleback.Select();
        }
        if(pause.activeSelf==true)
        {
            pausemenu.Select();
        }
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
    public void Title()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("title");
    }
}

