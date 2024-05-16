using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoalON : MonoBehaviour
{
    public SceneChange Clear;
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
            Debug.Log("K");
            Clear.GameClear2();
        }
        
    }
}
