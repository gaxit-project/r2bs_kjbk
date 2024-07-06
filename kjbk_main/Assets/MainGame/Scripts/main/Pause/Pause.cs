using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public static bool pause_status;

    private InputAction PauseAction;

    public static Presente presenter;

    public static bool pause1;
    public static bool pause2;

    public GameObject Presen;
    public Button ResumeIcon;

    public GoalJudgement Goal;

    [SerializeField] GameObject EscapeON;
    void Start()
    {
        pause.SetActive(false);

        var pInput = GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
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
            ResumeIcon.Select();

            if(!Goal.PauseFlag)
            {
                EscapeON.SetActive(false);
                PauseCon();
            }

        }
    }

    public void PauseCon()
    {
        if (Time.timeScale == 0)
        {
            pause.SetActive(!pause.activeSelf);
            Time.timeScale = 1.0f;
        }
        else
        {
            pause.SetActive(!pause.activeSelf);
            Time.timeScale = 0.0f;
        }
    }
}
