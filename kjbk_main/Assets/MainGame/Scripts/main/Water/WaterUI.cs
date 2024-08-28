using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUI : MonoBehaviour
{
    public GameObject WaterBar;
    private Image shokaImg;
    private float PassedTime;

    void Start()
    {
        shokaImg = WaterBar.GetComponent<Image>();
    }

    void Update()
    {
        float amount = PlayerPrefs.GetFloat("capacity") / 100;

        //“h‚è‚Â‚Ô‚µ—Ê‚ð‘ã“ü‚·‚é
        shokaImg.fillAmount = amount;
    }
}
