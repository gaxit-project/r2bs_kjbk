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
        //Kを押せば脱出する
        if (Input.GetKeyDown(KeyCode.K))
        {
            Rcnt = Cnt.getNum();
            Debug.Log("K");

            //救助した人数が5人以上ならクリアへ移行
            if (Rcnt >= 5)
            {
                Scene.Instance.GameClear();
            }

            //違うならゲームオーバーに移行
            else
            {
                Scene.Instance.GameOver();
            }
        }

        //Lを押せば非表示にする
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L");
            Goal.EscapeONOFF();
            //EscapeON.SetActive(false);
            //EscapeOFF.SetActive(false);
        }
    }

    /// <summary>
    /// ///////////出口に触れた瞬間ゴールするかのボタンを出す

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

    /// ///////////出口から離れた瞬間ゴールするかのボタンを消す

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

        //救助した人数が5人以上ならクリアへ移行
        if (Rcnt >= 5)
        {
            Scene.Instance.GameClear();
        }

        //違うならゲームオーバーに移行
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
