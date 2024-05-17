using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RCountText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RCount;

    public RescueCount_verMatsuno RCounter;
    int Cnt;
    int Cnt2;

    // Start is called before the first frame update
    void Start()
    {
        RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        Cnt = RCounter.getNum();
        RCount.SetText(Cnt +"");
    }

    // Update is called once per frame
    public void Update()
    {
        Cnt = RCounter.getNum();
        if (RCounter.getNum()<5)
        {
            RCount.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            RCount.SetText(Cnt + "");
        }
        else
        {
            RCount.color = new Color(0.0f, 1.0f, 0.085f, 1.0f);
            RCount.SetText(Cnt + "");
        }
    }


}
