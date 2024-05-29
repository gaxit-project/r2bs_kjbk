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
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        ExitAction = actionMap["Exit"];
    }

    // Update is called once per frame
    public void Update()
    {
        bool Exit = ExitAction.triggered;
        //Kを押せば脱出する
        if (Input.GetKeyDown(KeyCode.K) || Exit)
        {
            Rcnt = Cnt.getNum();
            Debug.Log("K");

            //救助した人数が5人以上ならクリアへ移行
            if(Rcnt >= 5)
            {
                Clear.GameClear2();
            }

            //違うならゲームオーバーに移行
            else
            {
                Over.GameOver();
            }
        }
        
    }
}
