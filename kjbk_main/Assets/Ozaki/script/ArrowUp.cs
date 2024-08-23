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
    private CharacterNavigation CharacterNavigation;
    public RescuePOP RPOP;
    // Start is called before the first frame update
    void Start()
    {
        CounterScript = FindObjectOfType<RescueCount>();
        CharacterNavigation = FindObjectOfType<CharacterNavigation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RPOP.ArrowONFlag || CharacterNavigation.NaviUp)
        {
            Arrow.SetActive(true);
        }
        else if(!RPOP.ArrowONFlag || !CharacterNavigation.NaviUp)
        {
            Arrow.SetActive(false);
        }
        //if(CounterScript.getNum() == 1)
        //{
        //    Arrow.SetActive(true);
        //}
        if(CounterScript.getNum() == 30)
        {
            Arrow.SetActive(false);
        }
    }
}
