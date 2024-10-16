using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterUI : MonoBehaviour
{
    #region 宣言
    public GameObject WaterBar;
    private Image shokaImg;
    private float PassedTime;
    #endregion

    #region 初期化
    void Start()
    {
        shokaImg = WaterBar.GetComponent<Image>();
    }
    #endregion

    #region 消火器ゲージの表示

    void Update()
    {
        float amount = PlayerPrefs.GetFloat("capacity") / 100;

        //塗りつぶし量を代入する
        shokaImg.fillAmount = amount;
    }
    #endregion
}