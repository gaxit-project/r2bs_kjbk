using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUp : MonoBehaviour
{
    private RescueCount CounterScript;
    [SerializeField, Tooltip("Arrow")]
    private GameObject Arrow;
    [SerializeField, Tooltip("軽症者助けた数")]
    int num = 1;
    [SerializeField, Tooltip("人数最大値")]
    private int maxnum;

    public RescuePOP RPOP;
    // Start is called before the first frame update
    void Start()
    {
        CounterScript = FindObjectOfType<RescueCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RPOP.ArrowONFlag)
        {
            Arrow.SetActive(true);
        }
        else if(!RPOP.ArrowONFlag)
        {
            Arrow.SetActive(false);
        }
        //if(CounterScript.getNum() == 1)
        //{
        //    Arrow.SetActive(true);
        //}
        if(CounterScript.getNum() == maxnum)
        {
            Arrow.SetActive(false);
        }
    }
}
