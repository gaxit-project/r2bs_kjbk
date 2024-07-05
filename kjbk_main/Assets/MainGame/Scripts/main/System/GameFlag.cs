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
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        ExitAction = actionMap["Exit"];

        //アクションマップからアクションを取得
        NotExitAction = actionMap["NotExit"];
    }

    // Update is called once per frame
    void Update()
    {
        bool Exit = ExitAction.triggered;
        bool NotExit = NotExitAction.triggered;
        //脱出ボタンが表示されている時
        if(Goal.EscStatus() == false)
        {
            //Kを押せば脱出する
            if (Exit || Input.GetKeyDown(KeyCode.K))
            {
                Rcnt = PlayerPrefs.GetInt("RescueCount");
                Debug.Log("K");

                //救助した人数が5人以上ならクリアへ移行
                if (Rcnt >= 5)
                {
                    PlayerPrefs.SetString("Result", "CLEAR");
                    Scene.Instance.GameResult();
                    //Scene.Instance.GameClear();
                }

                //違うならゲームオーバーに移行
                else
                {
                    PlayerPrefs.SetString("Result", "GAMEOVER");
                    Scene.Instance.GameResult();
                    //Scene.Instance.GameOver();
                }
            }

            //Lを押せば非表示にする
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
