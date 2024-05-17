using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RescueCount_verMatsuno : MonoBehaviour
{
    public int RescueMaxNum;   //Å‘å‹~•Ò
    [SerializeField] private int RescueNum = 0;   //Œ»İ‹~•Ò”
    public bool RescueAll = false;   //Å‘å‹~•Ò”‚ğ–‚½‚µ‚½‚Æ‚«‚Ìƒtƒ‰ƒO
    public RCountText countText;
    public Radio AloneRadio;

    // Start is called before the first frame update
    void Start()
    {
        RescueNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (RescueNum == RescueMaxNum)   //Å‘å‹~•Ò”‚ğ–‚½‚µ‚Ä‚¢‚é‚©‚Ì”äŠr
        {
            RescueAll = true;
        }
        if(RescueAll)
        {
            AloneRadio.AloneRadio();
        }
    }

    public void Count()   //Œ»İ‹~•Ò”‚ÌƒJƒEƒ“ƒg
    {
        RescueNum++;
    }

    public int getNum()   //Œ»İ‹~•Ò”‚Ìæ“¾
    {
        return RescueNum;
    }

    public bool getRescueAll()   //ƒtƒ‰ƒO‚Ìæ“¾
    {
        return RescueAll;
    }
}
