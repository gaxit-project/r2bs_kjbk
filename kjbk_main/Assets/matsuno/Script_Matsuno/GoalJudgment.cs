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
    private bool GJonoff = true;

    public bool JudgeFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        EscapeON.SetActive(false);
        EscapeOFF.SetActive(false);
    }

    // Update is called once per frame

/// <summary>
/// ///////////出口に触れた瞬間ゴールするかのボタンを出す

    public void OnCollisionEnter(Collision Hit)
    {
        if(GJonoff)
        {
            if (Hit.gameObject.tag == "Player")
            {
                BackToTheTitle.SetActive(false);
                SoundSetting.SetActive(false);
                Time.timeScale = 0.5f;
                if(JudgeFlag)
                {
                    Invoke(nameof(gamestop), 0.5f);
                    JudgeFlag = false;
                }
                EscapeON.SetActive(true);
                EscapeOFF.SetActive(true);
                GJonoff = false;
            }
        }
        
    }


    private void gamestop()
    {
        Time.timeScale = 0;
    }
    /// ///////////出口から離れた瞬間ゴールするかのボタンを消す

    public void OnCollisionExit(Collision Hit)
    {
        if (Hit.gameObject.tag == "Player")
        {
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
