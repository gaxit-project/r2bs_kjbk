using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollapseDesign2 : MonoBehaviour
{
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 倒壊ゲージのデザインの定義
    /// 
    [SerializeField] GameObject TenDesign;
    [SerializeField] GameObject EightDesign;
    [SerializeField] GameObject SixDesign;
    [SerializeField] GameObject FourDesign;
    [SerializeField] GameObject TwoDesign;
    [SerializeField] GameObject OneDesign;

    // Start is called before the first frame update
    void Start()
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///倒壊ゲージのデザインを隠す
    {
        //追加コード
        TenDesign.SetActive(true);
        EightDesign.SetActive(false);
        SixDesign.SetActive(false);
        FourDesign.SetActive(false);
        TwoDesign.SetActive(false);
        OneDesign.SetActive(false);
    }
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// 倒壊ゲージのデザインを表示する
    public void EightHouse()
    {
        Debug.Log("EightHouse");
        //追加コード
        TenDesign.SetActive(false);        //80%の時の倒壊デザインを非表示
        EightDesign.SetActive(true);          //80%の時倒壊デザインを表示
    }

    public void SixHouse()
    {
        Debug.Log("SixHouse");
        EightDesign.SetActive(false);        //80%の時の倒壊デザインを非表示
        SixDesign.SetActive(true);           //60%の時倒壊デザインを表示
    }

    public void FourHouse()
    {
        Debug.Log("FourHouse");
        SixDesign.SetActive(false);          //60%の時の倒壊デザインを非表示
        FourDesign.SetActive(true);          //40%の時倒壊デザインを表示
    }

    public void TwoHouse()
    {
        Debug.Log("TwoHouse");
        FourDesign.SetActive(false);         //40%の時の倒壊デザインを非表示
        TwoDesign.SetActive(true);           //20%の時倒壊デザインを表示
    }

    public void OneHouse()
    {
        Debug.Log("OneHouse");
        TwoDesign.SetActive(false);          //20%の時の倒壊デザインを非表示
        OneDesign.SetActive(true);           //10%の時倒壊デザインを表示
    }
}