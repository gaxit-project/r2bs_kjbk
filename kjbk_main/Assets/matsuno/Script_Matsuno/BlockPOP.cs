using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPOP : MonoBehaviour
{

    //áŠQ•¨‚ÌéŒ¾
    [SerializeField] GameObject Corridor1;
    [SerializeField] GameObject Corridor2;
    [SerializeField] GameObject Corridor3;
    [SerializeField] GameObject Corridor4;
    [SerializeField] GameObject Wall1;
    [SerializeField] GameObject Wall2;

    //“|‰óƒQ[ƒW‚Ìƒtƒ‰ƒO
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
        //áŠQ•¨‚ª¶¬‚³‚ê‚éêŠ‚É’N‚à‚¢‚È‚¢‚È‚ç
        if(Judge)
        {
            if (Generate40)
            {
                Corridor1.SetActive(true);
                Generate40 = false;
            }
            else if(Generate20)
            {
                Corridor2.SetActive(true);
                Corridor3.SetActive(true);
                Wall1.SetActive(false);
                Generate20 = false;
            }
            else if(Generate10)
            {
                Corridor4.SetActive(true);
                Wall2.SetActive(false);
                Generate10 = false;
            }
        } 
    }

    //áŠQ•¨‚ª—N‚­êŠ‚Él‚ª‚¢‚½‚çƒtƒ‰ƒO‚ğƒIƒt‚É‚·‚é
    public void OnCollisionEnter(Collision Hit2)
    {
        if(Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "Minorlnjuries")
        {
            Judge = false;
            Debug.Log("áŠQ•¨‚ğPOP‚Å‚«‚È‚¢‚æ" + Judge);
        }
    }


    //áŠQ•¨‚ª—N‚­êŠ‚©‚çl‚ª—£‚ê‚½‚çƒtƒ‰ƒO‚ğƒIƒ“‚É‚·‚é
    public void OnCollisionExit(Collision Hit2)
    {
        if (Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "Minorlnjuries")
        {
            Judge = true;
            Debug.Log("áŠQ•¨‚ğPOP‚Å‚«‚é‚æ" + Judge);
        }
    }
}
