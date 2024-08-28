using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIFE_Bar : MonoBehaviour
{
    #region 宣言: ゲームオブジェクト
    // HP1の赤色表示
    [SerializeField] private GameObject HP1Red;

    // HP2の通常表示
    [SerializeField] private GameObject HP2;

    // HP2の赤色表示
    [SerializeField] private GameObject HP2Red;

    // HP3の通常表示
    [SerializeField] private GameObject HP3;

    // HP3の赤色表示
    [SerializeField] private GameObject HP3Red;
    #endregion

    #region 宣言: 状態フラグ
    // 状態フラグ（未使用）
    private bool flag = true;
    #endregion

    #region 宣言: HPカウント
    // 現在のHP
    private int i = 3;
    #endregion

    #region 初期化: Startメソッド
    void Start()
    {
        // 初期状態でHP関連のゲームオブジェクトを非表示にする
        HP1Red.SetActive(false);
        HP2.SetActive(false);
        HP2Red.SetActive(false);
        HP3.SetActive(false);
        HP3Red.SetActive(false);
    }
    #endregion

    #region 関数: HPBarメソッド
    public void HPBar()
    {
        // HPの状態に応じた表示設定
        if (i == 3)
        {
            // HP3、HP2、HP1の表示設定
            HP1Red.SetActive(true);
            HP2Red.SetActive(false);
            HP3Red.SetActive(true);
            HP3.SetActive(true);
            HP2.SetActive(true);

            // HP3の点滅開始
            StartCoroutine(HPBar2());

            // 一定時間後に各HPオブジェクトを非表示にする
            Invoke(nameof(HP1RedKesu), 5f);
            Invoke(nameof(HP2RedKesu), 5f);
            Invoke(nameof(HP3RedKesu), 5f);
            Invoke(nameof(HP3Kesu), 5f);
            Invoke(nameof(HP2Kesu), 5f);
        }
        else if (i == 2)
        {
            // HP2、HP1の表示設定
            HP2.SetActive(false);
            HP1Red.SetActive(true);
            HP2Red.SetActive(true);
            HP3.SetActive(true);

            // HP2の点滅開始
            StartCoroutine(HPBar1());

            // 一定時間後に各HPオブジェクトを非表示にする
            Invoke(nameof(HP1RedKesu), 5f);
            Invoke(nameof(HP2RedKesu), 5f);
            Invoke(nameof(HP2Kesu), 5f);
            Invoke(nameof(HP3Kesu), 5f);
        }
        // HPカウントをデクリメント
        i--;
    }
    #endregion

    #region 関数: HPBar2メソッド
    IEnumerator HPBar2()
    {
        // HP3の赤色表示の点滅処理
        for (int i = 0; i < 10; i++)
        {
            // HP3Redを非表示にする
            HP3Red.SetActive(false);
            yield return new WaitForSeconds(0.2f);

            // HP3Redを表示する
            HP3Red.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        // 最終的にHP3Redを非表示にする
        HP3Red.SetActive(false);
    }
    #endregion

    #region 関数: HPBar1メソッド
    IEnumerator HPBar1()
    {
        // HP2の赤色表示の点滅処理
        for (int i = 0; i < 10; i++)
        {
            // HP2Redを非表示にする
            HP2Red.SetActive(false);
            yield return new WaitForSeconds(0.2f);

            // HP2Redを表示する
            HP2Red.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        // 最終的にHP2Redを非表示にする
        HP2Red.SetActive(false);
    }
    #endregion

    #region 関数: HP削除メソッド
    // HP3Redを非表示にするメソッド
    public void HP3RedKesu()
    {
        HP3Red.SetActive(false);
    }

    // HP2Redを非表示にするメソッド
    public void HP2RedKesu()
    {
        HP2Red.SetActive(false);
    }

    // HP1Redを非表示にするメソッド
    public void HP1RedKesu()
    {
        HP1Red.SetActive(false);
    }

    // HP3を非表示にするメソッド
    public void HP3Kesu()
    {
        HP3.SetActive(false);
    }

    // HP2を非表示にするメソッド
    public void HP2Kesu()
    {
        HP2.SetActive(false);
    }
    #endregion
}
