using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUp : MonoBehaviour
{
    private RescueCount_verMatsuno CounterScript;
    [SerializeField, Tooltip("Arrow")]
    private GameObject Arrow;
    [SerializeField, Tooltip("軽症者助けた数")]
    private int num;
    [SerializeField, Tooltip("人数最大値")]
    private int maxnum;
    // Start is called before the first frame update
    void Start()
    {
        CounterScript = FindObjectOfType<RescueCount_verMatsuno>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CounterScript.getNum() == num)
        {
            Arrow.SetActive(true);
        }
        else if(CounterScript.getNum() == maxnum)
        {
            Arrow.SetActive(false);
        }
    }
}