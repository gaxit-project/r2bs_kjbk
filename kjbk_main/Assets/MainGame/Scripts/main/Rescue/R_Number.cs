using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Number : MonoBehaviour
{
    #region 宣言
    // 軽症者識別のための変数
    public int Number;
    #endregion

    #region 初期化
    void Start()
    {
        PlayerPrefs.SetInt("R_number", 0);
    }
    #endregion


    #region 関数
    // 軽症者を識別するための関数
    public void RNumber()
    {
        PlayerPrefs.SetInt("R_number", Number);
    }
    #endregion
}
