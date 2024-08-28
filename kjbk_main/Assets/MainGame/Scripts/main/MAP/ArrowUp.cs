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
        //ナビが開始中は表示
        if(RPOP.ArrowONFlag || CharacterNavigation.NaviUp)
        {
            Arrow.SetActive(true);
        }
        //ナビが停止中は非表示
        else if(!RPOP.ArrowONFlag || !CharacterNavigation.NaviUp)
        {
            Arrow.SetActive(false);
        }

        if(CounterScript.getNum() == 30)
        {
            Arrow.SetActive(false);
        }
    }
}
