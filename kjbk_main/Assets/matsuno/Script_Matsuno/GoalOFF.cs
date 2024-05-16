using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoalOFF : MonoBehaviour
{
    [SerializeField] GameObject EscapeON;
    [SerializeField] GameObject EscapeOFF;

    public SceneChange Over;
    public GoalJudgement Goal;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L");
            Goal.EscapeONOFF();
            //EscapeON.SetActive(false);
            //EscapeOFF.SetActive(false);
        }
       
    }
}
