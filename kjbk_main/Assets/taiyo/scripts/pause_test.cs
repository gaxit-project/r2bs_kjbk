using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject pause;
    bool pause_status;
    // Start is called before the first frame update
    void Start()
    {
        pause_status = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("������܂���");
            if(pause_status == true)
            {
                pause_status=false;
            }
            else
            {
                pause_status = true;
            }
        }

        if (pause_status == true)
        {
            pause.SetActive(pause_status);
            //Debug.Log("�|�[�Y���ł�");
            Time.timeScale = 0.0f;
        }
        else
        {
            pause.SetActive(pause_status);
            //Debug.Log("�Q�[�����ł�");
            Time.timeScale= 1.0f;
        }
    }
}
