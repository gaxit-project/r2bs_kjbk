using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public static bool pause_status;

    private InputAction PauseAction;

    public static Presente presenter;

    public static bool pause1;
    public static bool pause2;

    public GameObject Presen;
    void Start()
    {
        pause_status = false;

        var pInput = GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        PauseAction = actionMap["Pause"];

        presenter = Presen.GetComponent<Presente>();
    }


    void Update()
    {
        pause1 = presenter.ConfigSta;
        pause2 = presenter.TitleSta;
        bool pause = PauseAction.triggered;
        if ((Input.GetKeyDown(KeyCode.Tab) || pause) && (!pause1 && !pause2))
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
