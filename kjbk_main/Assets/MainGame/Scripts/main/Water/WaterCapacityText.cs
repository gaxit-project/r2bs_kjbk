using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCapacityText : MonoBehaviour
{
    public Text Capacity;
    void Start()
    {
        Capacity.text = "Á‰ÎŠí‚Ì—e—Ê:" + 0;
    }

    void Update()
    {
        if (PlayerPrefs.GetFloat("capacity") > 0)
        {
            Capacity.text = "Á‰ÎŠí‚Ì—e—Ê:" + PlayerPrefs.GetFloat("capacity");
        }
        else
        {
            Capacity.text = "Á‰ÎŠí‚Ì—e—Ê:" + "•â[‚µ‚Ä‚­‚¾‚³‚¢";
        }
        
    }
}
