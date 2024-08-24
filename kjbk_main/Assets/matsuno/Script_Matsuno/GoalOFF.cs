using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GoalOFF : MonoBehaviour
{
    [SerializeField] GameObject EscapeON;
    [SerializeField] GameObject EscapeOFF;

    public SceneChange Over;
    public GoalJudgement Goal;

    private InputAction ExitAction;

    // Start is called before the first frame update
    void Start()
    {
        var pInput = GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        ExitAction = actionMap["NotExit"];
    }

    // Update is called once per frame
    public void Update()
    {
        bool Exit = ExitAction.triggered;
        //L�������Δ�\���ɂ���
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("L");
            //EscapeON.SetActive(false);
            //EscapeOFF.SetActive(false);
        }
       
    }
}
