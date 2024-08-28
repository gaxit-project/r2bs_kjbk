using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPOP : MonoBehaviour
{

    //障害物の宣言
    [SerializeField] GameObject Corridor1;
    [SerializeField] GameObject Corridor2;
    [SerializeField] GameObject Corridor3;
    [SerializeField] GameObject Corridor4;
    [SerializeField] GameObject Wall1;
    [SerializeField] GameObject Wall2;

    //倒壊ゲージのフラグ
    [HideInInspector] public bool Generate40 = false;
    [HideInInspector] public bool Generate20 = false;
    [HideInInspector] public bool Generate10 = false;
    [HideInInspector] public bool Judge = true;


    // Start is called before the first frame update
    void Start()
    {
        Corridor1.SetActive(false);
        Corridor2.SetActive(false);
        Corridor3.SetActive(false);
        Corridor4.SetActive(false);
        Wall1.SetActive(true);
        Wall2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //障害物が生成される場所に誰もいないときに障害物を設置
        if(Judge)
        {
            //コラプスゲージが40%のときに障害物設置
            if (Generate40)
            {
                Corridor1.SetActive(true);
                Generate40 = false;
            }
            //コラプスゲージが20%のときに障害物設置
            else if (Generate20)
            {
                Corridor2.SetActive(true);
                Corridor3.SetActive(true);
                Wall1.SetActive(false);
                Generate20 = false;
            }
            //コラプスゲージが10%のときに障害物設置
            else if (Generate10)
            {
                Corridor4.SetActive(true);
                Wall2.SetActive(false);
                Generate10 = false;
            }
        } 
    }

    //障害物が湧く場所に人がいたらフラグをオフにする
    public void OnCollisionEnter(Collision Hit2)
    {
        if(Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "Minorlnjuries")
        {
            Judge = false;
            Debug.Log("障害物をPOPできないよ" + Judge);
        }
    }


    //障害物が湧く場所から人が離れたらフラグをオンにする
    public void OnCollisionExit(Collision Hit2)
    {
        if (Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "Minorlnjuries")
        {
            Judge = true;
            Debug.Log("障害物をPOPできるよ" + Judge);
        }
    }
}
