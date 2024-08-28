using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUI : MonoBehaviour
{
    #region éŒ¾
    public GameObject WaterBar;
    private Image shokaImg;
    private float PassedTime;
    #endregion

    #region ‰Šú‰»
    void Start()
    {
        shokaImg = WaterBar.GetComponent<Image>();
    }
    #endregion

    #region Á‰ÎŠíƒQ[ƒW‚Ì•\¦

    void Update()
    {
        float amount = PlayerPrefs.GetFloat("capacity") / 100;

        //“h‚è‚Â‚Ô‚µ—Ê‚ğ‘ã“ü‚·‚é
        shokaImg.fillAmount = amount;
    }
    #endregion
}