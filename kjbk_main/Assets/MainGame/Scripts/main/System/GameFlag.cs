using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameFlag : MonoBehaviour
{
    public RescueCount_verMatsuno Cnt;
    int Rcnt = 0;

    [SerializeField] GameObject EscapeON;
    [SerializeField] GameObject EscapeOFF;
    private bool GJonoff = true;

    public GoalJudgement Goal;

    private InputAction ExitAction;
    private InputAction NotExitAction;


    // Start is called before the first frame update
    void Start()
    {
        EscapeON.SetActive(false);
        EscapeOFF.SetActive(false);

        var pInput = GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        ExitAction = actionMap["Exit"];

        //�A�N�V�����}�b�v����A�N�V�������擾
        NotExitAction = actionMap["NotExit"];
    }

    // Update is called once per frame
    void Update()
    {
        bool Exit = ExitAction.triggered;
        bool NotExit = NotExitAction.triggered;
        //�E�o�{�^�����\������Ă��鎞
        if(Goal.EscStatus() == false)
        {
            //K�������ΒE�o����
            if (Exit || Input.GetKeyDown(KeyCode.K))
            {
                Rcnt = PlayerPrefs.GetInt("RescueCount");
                Debug.Log("K");

                //�~�������l����5�l�ȏ�Ȃ�N���A�ֈڍs
                if (Rcnt >= 5)
                {
                    PlayerPrefs.SetString("Result", "CLEAR");
                    Scene.Instance.GameResult();
                    //Scene.Instance.GameClear();
                }

                //�Ⴄ�Ȃ�Q�[���I�[�o�[�Ɉڍs
                else
                {
                    PlayerPrefs.SetString("Result", "GAMEOVER");
                    Scene.Instance.GameResult();
                    //Scene.Instance.GameOver();
                }
            }

            //L�������Δ�\���ɂ���
            if (NotExit || Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("L");
                Goal.EscapeONOFF();
                Time.timeScale = 1;
                Invoke(nameof(FlagONOFF), 5);
                //EscapeON.SetActive(false);
                //EscapeOFF.SetActive(false);
            }

        }
    }

    public void FlagONOFF()
    {
        Goal.JudgeFlag = true;
    }

    public void EscapeONOFF()
    {
        EscapeON.SetActive(false);
        EscapeOFF.SetActive(false);
    }

    public void EscapeUI()
    {
        Goal.EscapeONOFF();
    }
}
