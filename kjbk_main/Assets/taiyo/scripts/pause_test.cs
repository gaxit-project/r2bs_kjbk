using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    public GameObject pause;
    public GameObject soundsetting;
    bool pause_status;

    public Button sound;
    public Button pausemenu;

    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
        soundsetting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("âüÇ≥ÇÍÇ‹ÇµÇΩ");
            pause.SetActive(!pause.activeSelf);

            if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
                pause.SetActive(false);
                soundsetting.SetActive(false);
                pausemenu.Select();
            }
            else
            {
                Time.timeScale = 0.0f;
            }
        }
    }

    public void EP()
    {
        pause.SetActive(false);
        //Debug.Log("ÉQÅ[ÉÄíÜÇ≈Ç∑");
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
        SceneManager.LoadScene("title");
    }
}

