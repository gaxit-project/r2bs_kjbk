using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text TextObject = null;
    private int ScoreNum = 0;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        

        Text ScoreText = TextObject.GetComponent<Text>();
        ScoreText.text = "Score�F" + ScoreNum;
    }
}
