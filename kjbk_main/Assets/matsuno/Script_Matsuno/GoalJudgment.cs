using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalJudgement : MonoBehaviour
{
    [SerializeField] GameObject EscapeON;
    [SerializeField] GameObject EscapeOFF;
    [SerializeField] GameObject BackToTheTitle;
    [SerializeField] GameObject SoundSetting;
    [SerializeField] Button Esc;
    private bool GJonoff = true;

    public bool JudgeFlag = false;
    public bool PauseFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        EscapeON.SetActive(false);
        EscapeOFF.SetActive(false);
        Invoke(nameof(FlagONOFF), 2);
    }

    // Update is called once per frame

    /// <summary>
    /// ///////////出口に触れた瞬間ゴールするかのボタンを出す

    public void OnCollisionEnter(Collision Hit)
    {
        if (GJonoff)
        {
            if (Hit.gameObject.tag == "Player")
            {
                BackToTheTitle.SetActive(false);
                SoundSetting.SetActive(false);
                Time.timeScale = 0;
                EscapeON.SetActive(true);
                EscapeOFF.SetActive(true);
                GJonoff = false;
                PauseFlag = true;
                Esc.Select();
            }
        }

    }

    private void FlagONOFF()
    {
        JudgeFlag = true;
    }

    private void gamestop()
    {
        Time.timeScale = 0;
    }
    private void GameRestart()
    {
        Time.timeScale = 1;
    }
    /// ///////////出口から離れた瞬間ゴールするかのボタンを消す

    public void OnCollisionExit(Collision Hit)
    {
        if (Hit.gameObject.tag == "Player")
        {
            Time.timeScale = 1f;
            EscapeON.SetActive(false);
            EscapeOFF.SetActive(false);
            GJonoff = true;
            BackToTheTitle.SetActive(true);
            SoundSetting.SetActive(true);
        }
    }

    public void EscapeONOFF()
    {
        EscapeON.SetActive(false);
        EscapeOFF.SetActive(false);
    }

    public bool EscStatus()
    {
        return GJonoff;
    }
}
