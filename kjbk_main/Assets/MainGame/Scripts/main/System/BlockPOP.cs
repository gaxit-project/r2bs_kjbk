using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPOP : MonoBehaviour
{
    #region 障害物の宣言
    [SerializeField] private GameObject Corridor1;   // 障害物コリドール1
    [SerializeField] private GameObject Corridor2;   // 障害物コリドール2
    [SerializeField] private GameObject Corridor3;   // 障害物コリドール3
    [SerializeField] private GameObject Corridor4;   // 障害物コリドール4
    [SerializeField] private GameObject Wall1;       // 障害物ウォール1
    [SerializeField] private GameObject Wall2;       // 障害物ウォール2
    #endregion

    #region 倒壊ゲージのフラグ
    [HideInInspector] public bool Generate40 = false; // コラプスゲージが40%のときのフラグ
    [HideInInspector] public bool Generate20 = false; // コラプスゲージが20%のときのフラグ
    [HideInInspector] public bool Generate10 = false; // コラプスゲージが10%のときのフラグ
    [HideInInspector] public bool Judge = true;       // 障害物設置判定フラグ
    #endregion

    #region 初期化処理
    void Start()
    {
        #region 障害物の初期状態設定
        Corridor1.SetActive(false); // コリドール1を非表示
        Corridor2.SetActive(false); // コリドール2を非表示
        Corridor3.SetActive(false); // コリドール3を非表示
        Corridor4.SetActive(false); // コリドール4を非表示
        Wall1.SetActive(true);      // ウォール1を表示
        Wall2.SetActive(true);      // ウォール2を表示
        #endregion
    }
    #endregion

    #region 更新処理
    void Update()
    {
        #region 障害物の設置処理
        // 障害物が生成される場所に誰もいないときに障害物を設置
        if (Judge)
        {
            // コラプスゲージが40%のときに障害物設置
            if (Generate40)
            {
                Corridor1.SetActive(true);  // コリドール1を表示
                Generate40 = false;        // フラグをリセット
            }
            // コラプスゲージが20%のときに障害物設置
            else if (Generate20)
            {
                Corridor2.SetActive(true);  // コリドール2を表示
                Corridor3.SetActive(true);  // コリドール3を表示
                Wall1.SetActive(false);     // ウォール1を非表示
                Generate20 = false;        // フラグをリセット
            }
            // コラプスゲージが10%のときに障害物設置
            else if (Generate10)
            {
                Corridor4.SetActive(true);  // コリドール4を表示
                Wall2.SetActive(false);     // ウォール2を非表示
                Generate10 = false;        // フラグをリセット
            }
        }
        #endregion
    }
    #endregion

    #region コリジョン処理
    // 障害物が湧く場所に人がいたらフラグをオフにする
    public void OnCollisionEnter(Collision Hit2)
    {
        if (Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "MinorInjuries")
        {
            Judge = false; // 人がいるとフラグをオフ
        }
    }

    // 障害物が湧く場所から人が離れたらフラグをオンにする
    public void OnCollisionExit(Collision Hit2)
    {
        if (Hit2.gameObject.tag == "Player" || Hit2.gameObject.tag == "MinorInjuries")
        {
            Judge = true; // 人がいなくなったらフラグをオン
        }
    }
    #endregion
}
