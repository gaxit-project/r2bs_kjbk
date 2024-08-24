using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GoalJudgement : MonoBehaviour
{
    [SerializeField] GameObject ExitUI;



    void Start()
    {
        ExitUI.SetActive(false);
    }




    //出口に触れた瞬間ゴールするかのボタンを出す


    void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {

            ExitUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            ExitUI.SetActive(false);
        }


    }

}
