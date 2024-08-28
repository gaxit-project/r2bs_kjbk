using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollDesign : MonoBehaviour
{
    #region フィールドの定義
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 倒壊ゲージのデザインの定義
    ///
    [SerializeField] GameObject TenDesign;
    [SerializeField] GameObject EightDesign;
    [SerializeField] GameObject SixDesign;
    [SerializeField] GameObject FourDesign;
    [SerializeField] GameObject TwoDesign;
    [SerializeField] GameObject OneDesign;
    #endregion

    #region 初期化メソッド
    // Start is called before the first frame update
    void Start()
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 倒壊ゲージのデザインを隠す
    {
        TenDesign.SetActive(true);    // 100%の倒壊デザインを表示
        EightDesign.SetActive(false); // 80%の倒壊デザインを非表示
        SixDesign.SetActive(false);   // 60%の倒壊デザインを非表示
        FourDesign.SetActive(false);  // 40%の倒壊デザインを非表示
        TwoDesign.SetActive(false);   // 20%の倒壊デザインを非表示
        OneDesign.SetActive(false);   // 10%の倒壊デザインを非表示
    }
    #endregion

    #region 倒壊ゲージのデザインを表示するメソッド
    /// <summary>
    /// 80%の倒壊デザインを表示する
    /// </summary>
    public void EightHouse()
    {
        TenDesign.SetActive(false);    // 100%の倒壊デザインを非表示
        EightDesign.SetActive(true);   // 80%の倒壊デザインを表示
    }

    /// <summary>
    /// 60%の倒壊デザインを表示する
    /// </summary>
    public void SixHouse()
    {
        EightDesign.SetActive(false);  // 80%の倒壊デザインを非表示
        SixDesign.SetActive(true);     // 60%の倒壊デザインを表示
    }

    /// <summary>
    /// 40%の倒壊デザインを表示する
    /// </summary>
    public void FourHouse()
    {
        SixDesign.SetActive(false);    // 60%の倒壊デザインを非表示
        FourDesign.SetActive(true);    // 40%の倒壊デザインを表示
    }

    /// <summary>
    /// 20%の倒壊デザインを表示する
    /// </summary>
    public void TwoHouse()
    {
        FourDesign.SetActive(false);   // 40%の倒壊デザインを非表示
        TwoDesign.SetActive(true);     // 20%の倒壊デザインを表示
    }

    /// <summary>
    /// 10%の倒壊デザインを表示する
    /// </summary>
    public void OneHouse()
    {
        TwoDesign.SetActive(false);    // 20%の倒壊デザインを非表示
        OneDesign.SetActive(true);     // 10%の倒壊デザインを表示
    }
    #endregion
}
