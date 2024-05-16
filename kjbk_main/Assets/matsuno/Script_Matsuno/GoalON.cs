using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoalON : MonoBehaviour
{
    public SceneChange Clear;
    public SceneChange Over;
    public Rescue_Counter Cnt;

    int Rcnt;
    //[SerializeField] Button EscapeON;

    // Start is called before the first frame update
    void Start()
    {
        //EscapeON.onClick.AddListener(() => { OnClick(); }) ;
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Rcnt = Cnt.getNum();
            Debug.Log("K");
            if(Rcnt >= 5)
            {
                Clear.GameClear2();
            }
            else
            {
                Over.GameOver();
            }
        }
        
    }
}
