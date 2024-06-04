using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalJudgement : MonoBehaviour
{
    [SerializeField] GameObject EscapeON;
    [SerializeField] GameObject EscapeOFF;
    private bool GJonoff = true;

    // Start is called before the first frame update
    void Start()
    {
        EscapeON.SetActive(false);
        EscapeOFF.SetActive(false);
    }

    // Update is called once per frame

/// <summary>
/// ///////////�o���ɐG�ꂽ�u�ԃS�[�����邩�̃{�^�����o��

    public void OnCollisionEnter(Collision Hit)
    {
        if(GJonoff)
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

    public bool EscStatus()
    {
        return GJonoff;
    }
}
