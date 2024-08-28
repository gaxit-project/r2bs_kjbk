using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescuePOP : MonoBehaviour
{
    #region 変数の宣言

    // RescuePOPオブジェクトの参照
    public RescuePOP Pop;

    // 重傷者用のゲームオブジェクト
    [SerializeField] GameObject RBalcony;
    [SerializeField] GameObject RKitchen;
    [SerializeField] GameObject RBath;
    [SerializeField] GameObject RCloset;
    [SerializeField] GameObject RBedRoom;

    // 軽症者用のゲームオブジェクト
    [SerializeField] GameObject hito1st;
    [SerializeField] GameObject JK1st;
    [SerializeField] GameObject kurohuku1st;
    [SerializeField] GameObject ILOVENY1st;
    [SerializeField] GameObject hito1_st;
    [SerializeField] GameObject hito2nd;
    [SerializeField] GameObject JK2nd;
    [SerializeField] GameObject kurohuku2nd;
    [SerializeField] GameObject hito3rd;
    [SerializeField] GameObject JK3rd;
    [SerializeField] GameObject hito3_2rd;
    [SerializeField] GameObject kurohuku4th;
    [SerializeField] GameObject ILOVENY4th;
    [SerializeField] GameObject hito4th;
    [SerializeField] GameObject JK5th;
    [SerializeField] GameObject kurohuku5th;
    [SerializeField] GameObject ILOVENY5th;

    // ポップアップ用のゲームオブジェクト
    [SerializeField] GameObject FirstPop;
    [SerializeField] GameObject SecondPop;
    [SerializeField] GameObject ThirdPop;
    [SerializeField] GameObject ForthPop;
    [SerializeField] GameObject FifthPop;

    // ランダムな値や軽症者の人数を格納する変数
    [HideInInspector] public int Rnd = 0;
    [HideInInspector] public int MCnt = -1;
    int a = 0;

    // 状態管理用のフラグ
    bool First = false;
    bool RndomONOFF = true;

    bool R1 = true;
    bool R2 = true;
    bool R3 = true;
    bool R4 = true;
    bool R5 = true;

    int cnt = 1;

    [HideInInspector] public bool ArrowONFlag = false;

    public int rndom;

    // 軽症者数
    public int AllRCnt = 3;

    // Radio_ver4の参照
    public Radio_ver4 Radio4;

    #endregion

    #region 軽症者を救った時の処理
    // 軽症者を救ったときに呼び出す関数
    public void LightR()
    {
        MCnter();  // 救った軽症者をカウントする関数
    }
    #endregion

    #region 重傷者を救った時の処理
    // 重傷者を救ったときに呼び出す関数
    public void HeavyR()
    {
        Radio4.BringDialogue(); // ダイアログを表示
        RndomONOFF = true;      // ランダム処理を有効化
        Rndom();                // ランダムに数値を生成
        Rpop();                 // 新しい重傷者をポップアップ
        ArrowONFlag = false;
    }
    #endregion

    #region 軽症者数のカウント
    // 救った軽症者の人数をカウント
    public int MCnter()
    {
        MCnt++;
        return MCnt;
    }
    #endregion

    #region ランダム処理
    // ランダムの数を入れる関数
    public int Rndom()
    {
        while (RndomONOFF)
        {
            if (!R1 && !R2 && !R3 && !R4 && !R5)  // 全員救助された場合
            {
                break;
            }
            else
            {
                Rnd = Random.Range(1, 6);  // 1～5の値の中でランダムに1つ生成

                // ランダム値の重複チェック
                if ((Rnd == 1 && R1) || (Rnd == 2 && R2) || (Rnd == 3 && R3) || (Rnd == 4 && R4) || (Rnd == 5 && R5))
                {
                    switch (Rnd)
                    {
                        case 1: R1 = false; break;
                        case 2: R2 = false; break;
                        case 3: R3 = false; break;
                        case 4: R4 = false; break;
                        case 5: R5 = false; break;
                    }
                    RndomONOFF = false;
                    break;
                }
            }
        }
        return Rnd;
    }
    #endregion

    #region 新たな重傷者の設置
    // 新たな重傷者の設置＋重傷者ヒントをスタックへプッシュ
    public void Rpop()
    {
        // ランダムの数値を受け取る
        rndom = Pop.Rnd;

        switch (rndom)
        {
            case 1:
                RBalcony.SetActive(true);
                Radio4.Push();
                break;
            case 2:
                RKitchen.SetActive(true);
                Radio4.Push();
                break;
            case 3:
                RBath.SetActive(true);
                Radio4.Push();
                break;
            case 4:
                RCloset.SetActive(true);
                Radio4.Push();
                break;
            case 5:
                RBedRoom.SetActive(true);
                Radio4.Push();
                break;
        }
    }
    #endregion

    #region 新たな軽症者の設置
    // 新たな軽症者の設置
    public void PopR()
    {
        switch (cnt)
        {
            case 1:
                FirstPop.SetActive(true);
                AllRCnt += 5;
                break;
            case 2:
                SecondPop.SetActive(true);
                AllRCnt += 4;
                break;
            case 3:
                ThirdPop.SetActive(true);
                AllRCnt += 5;
                break;
            case 4:
                ForthPop.SetActive(true);
                AllRCnt += 4;
                break;
            case 5:
                FifthPop.SetActive(true);
                AllRCnt += 3;
                break;
        }
        cnt++;
    }
    #endregion
}
