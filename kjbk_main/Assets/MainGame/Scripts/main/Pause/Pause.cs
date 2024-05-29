using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public static bool pause_status;
    // Start is called before the first frame update

    private InputAction PauseAction;
    void Start()
    {
        pause_status = false;

        var pInput = GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        PauseAction = actionMap["Pause"];
    }


    void Update()
    {
        bool pause = PauseAction.triggered;
        if (Input.GetKeyDown(KeyCode.Tab) || pause)
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
            //Debug.Log("�|�[�Y���ł�");
            Time.timeScale = 0.0f;
        }
        else
        {
            pause.SetActive(pause_status);
            //Debug.Log("�Q�[�����ł�");
            Time.timeScale = 1.0f;
        }
    }
}
