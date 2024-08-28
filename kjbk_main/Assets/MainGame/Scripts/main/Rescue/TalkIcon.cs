using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkIcon : MonoBehaviour
{
    #region 変数の宣言

    // アイコンのゲームオブジェクト
    [SerializeField] GameObject PlayerIcon;  // プレイヤーアイコン
    [SerializeField] GameObject NPCIcon;     // NPCアイコン

    // RescueNPCスクリプトの参照
    public RescueNPC RescueNPC;

    #endregion

    void Update()
    {
        #region アイコンの表示処理

        // アイコンがアクティブかつロックされていない場合
        if (RescueNPC.IsItActiveIcon() && !RescueNPC.IsItLock())
        {
            RescueNPC.SetLock(true);     // ロックを設定
            ActivePlayerIcon();          // プレイヤーアイコンを表示
            Invoke("ActiveNPCIcon", 1f); // 1秒後にNPCアイコンを表示
            Invoke("FinishTalk", 2f);    // 2秒後にトークを終了
        }

        // アイコンがアクティブかつ二度目の接触の場合
        if (RescueNPC.IsItActiveIcon() && RescueNPC.IsItSecondContact())
        {
            FinishTalk();                   // トークを終了
            RescueNPC.SetActiveIcon(false); // アイコンを非アクティブに設定
            PlayerIcon.SetActive(false);    // プレイヤーアイコンを非表示
            NPCIcon.SetActive(false);       // NPCアイコンを非表示
        }

        #endregion
    }

    #region アイコンの表示関連の処理

    // プレイヤーアイコンを表示
    private void ActivePlayerIcon()
    {
        if (!RescueNPC.IsItSecondContact())
        {
            PlayerIcon.SetActive(true);
        }
    }

    // NPCアイコンを表示
    private void ActiveNPCIcon()
    {
        if (!RescueNPC.IsItSecondContact())
        {
            PlayerIcon.SetActive(false);  // プレイヤーアイコンを非表示
            NPCIcon.SetActive(true);      // NPCアイコンを表示
        }
    }

    // トークを終了
    private void FinishTalk()
    {
        NPCIcon.SetActive(false);            // NPCアイコンを非表示
        RescueNPC.SetActiveIcon(false);      // アイコンを非アクティブに設定
        RescueNPC.SetLock(false);            // ロックを解除
    }

    #endregion
}
