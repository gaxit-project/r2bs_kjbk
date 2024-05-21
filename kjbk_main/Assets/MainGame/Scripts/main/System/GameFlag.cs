using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlag : MonoBehaviour
{
    public RescueCount_verMatsuno Cnt;
    int Rcnt = 0;

    [SerializeField] GameObject EscapeON;
    [SerializeField] GameObject EscapeOFF;
    private bool GJonoff = true;

    public GoalJudgement Goal;


    // Start is called before the first frame update
    void Start()
    {
        EscapeON.SetActive(false);
        EscapeOFF.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //K�������ΒE�o����
        if (Input.GetKeyDown(KeyCode.K))
        {
            Rcnt = PlayerPrefs.GetInt("RescueCount");
            Debug.Log("K");

            //�~�������l����5�l�ȏ�Ȃ�N���A�ֈڍs
            if (Rcnt >= 2)
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L");
            Goal.EscapeONOFF();
            //EscapeON.SetActive(false);
            //EscapeOFF.SetActive(false);
        }
    }

    /// <summary>
    /// ///////////�o���ɐG�ꂽ�u�ԃS�[�����邩�̃{�^�����o��

    public void OnCollisionEnter(Collision Hit)
    {
        if (GJonoff)
        {
            if (Hit.gameObject.tag == "Player")
            {
                EscapeON.SetActive(true);
                EscapeOFF.SetActive(true);
                GJonoff = false;
            }
        }

    }

    /// ///////////�o�����痣�ꂽ�u�ԃS�[�����邩�̃{�^��������

    public void OnCollisionExit(Collision Hit)
    {
        if (Hit.gameObject.tag == "Player")
        {
            EscapeON.SetActive(false);
            EscapeOFF.SetActive(false);
            GJonoff = true;
        }
    }

    public void EscapeONOFF()
    {
        EscapeON.SetActive(false);
        EscapeOFF.SetActive(false);
    }

    public void EscapeResult()
    {
        Rcnt = Cnt.getNum();
        Debug.Log("K");

        //�~�������l����5�l�ȏ�Ȃ�N���A�ֈڍs
        if (Cnt.getNum() >= 2)
        {
            Scene.Instance.GameClear();
        }

        //�Ⴄ�Ȃ�Q�[���I�[�o�[�Ɉڍs
        else
        {
            Scene.Instance.GameOver();
        }
    }

    public void EscapeUI()
    {
        Goal.EscapeONOFF();
    }
}
