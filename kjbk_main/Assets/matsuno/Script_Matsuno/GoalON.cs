using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GoalON : MonoBehaviour
{
    public SceneChange Clear;
    public SceneChange Over;
    public RescueCount_verMatsuno Cnt;

    int Rcnt;
    //[SerializeField] Button EscapeON;

    // Start is called before the first frame update

    private InputAction ExitAction;
    void Start()
    {
        //EscapeON.onClick.AddListener(() => { OnClick(); }) ;

        var pInput = GetComponent<PlayerInput>();
        //���݂̃A�N�V�����}�b�v���擾
        var actionMap = pInput.currentActionMap;

        //�A�N�V�����}�b�v����A�N�V�������擾
        ExitAction = actionMap["Exit"];
    }

    // Update is called once per frame
    public void Update()
    {
        bool Exit = ExitAction.triggered;
        //K�������ΒE�o����
        if (Input.GetKeyDown(KeyCode.K) || Exit)
        {
            Rcnt = Cnt.getNum();
            Debug.Log("K");

            //�~�������l����5�l�ȏ�Ȃ�N���A�ֈڍs
            if(Rcnt >= 5)
            {
                Clear.GameClear2();
            }

            //�Ⴄ�Ȃ�Q�[���I�[�o�[�Ɉڍs
            else
            {
                Over.GameOver();
            }
        }
        
    }
}
