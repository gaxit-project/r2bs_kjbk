using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoalON : MonoBehaviour
{
    public SceneChange Clear;
    public SceneChange Over;
    public RescueCount_verMatsuno Cnt;

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
        //K�������ΒE�o����
        if(Input.GetKeyDown(KeyCode.K))
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
