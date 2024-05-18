using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlag : MonoBehaviour
{
    public Rescue_Counter Cnt;
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
            Rcnt = Cnt.getNum();
            Debug.Log("K");

            //�~�������l����5�l�ȏ�Ȃ�N���A�ֈڍs
            if (Rcnt >= 5)
            {
                Scene.Instance.GameClear();
            }

            //�Ⴄ�Ȃ�Q�[���I�[�o�[�Ɉڍs
            else
            {
                Scene.Instance.GameOver();
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
        if (Rcnt >= 5)
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
