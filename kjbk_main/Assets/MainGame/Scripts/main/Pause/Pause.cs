using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public static bool pause_status;
    // Start is called before the first frame update
    void Start()
    {
        pause_status = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PauseCon();
        }
    }

    public void PauseCon()
    {
        if (pause_status == true)
        {
           pause_status = false;
        }
        else
        {
            pause_status = true;
        }

        if (pause_status == true)
        {
            pause.SetActive(pause_status);
            //Debug.Log("ポーズ中です");
            Time.timeScale = 0.0f;
        }
        else
        {
            pause.SetActive(pause_status);
            //Debug.Log("ゲーム中です");
            Time.timeScale = 1.0f;
        }
    }
}
