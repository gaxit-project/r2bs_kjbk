using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionMapUI : MonoBehaviour
{
    #region 変数の宣言
    [SerializeField] public TMP_Text MissionMAPText; // UIに表示するテキスト

    string MainMission; // メインミッションの内容
    string Hint1; // ヒント1の内容
    string Hint2; // ヒント2の内容
    string Hint3; // ヒント3の内容
    string Situation; // 現在の状況
    #endregion

    void Start()
    {
        #region 初期テキスト設定
        MainMission = "☐10人以上人を助けろ！";
        Hint1 = "■<sprite=1>の奥の方で\n　人が倒れている？";
        Hint2 = "☐????????????????";
        Hint3 = "☐????????????????";
        Situation = "多くの人を救え！";
        MissionMAPText.SetText("<size=55>MISSION</size>\n" +
                               "<size=40>" + MainMission + "</size>" + "\n\n\r" +
                               "<size=55>HINT</size>\n" +
                               "<size=40>" + Hint1 + "</size>\n" +
                               "<size=40>" + Hint2 + "</size>\n" +
                               "<size=40>" + Hint3 + "</size>\n\n\r" +
                               "<size=55>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>");
        #endregion
    }



    #region ミッションの更新
    // テキストを更新する
    public void MissionUpgread(string Text, int cnt)
    {
        #region ヒント更新処理
        if (cnt == 0)
        {
            Hint1 = Text;
            Hint2 = Text;
            Hint3 = Text;
        }
        else if (cnt == 1)
        {
            Hint1 = "■" + Text;
        }
        else if (cnt == 2)
        {
            Hint2 = "■" + Text;
        }
        else if (cnt == 3)
        {
            Hint3 = "■" + Text;
        }
        #endregion

        #region メインミッションと状況の更新処理
        else if (cnt == 10)
        {
            MainMission = "■10人以上人を助ける！\n☐出口へ迎おう！";
            Situation = "時間の許す限り人を\n救って脱出しよう！";
        }
        #endregion

        #region UIテキストの更新
        MissionMAPText.SetText("<size=55>MISSION</size>\n" +
                               "<size=40>" + MainMission + "</size>" + "\n\n\r" +
                               "<size=55>HINT</size>\n" +
                               "<size=40>" + Hint1 + "</size>\n" +
                               "<size=40>" + Hint2 + "</size>\n" +
                               "<size=40>" + Hint3 + "</size>\n\n\r" +
                               "<size=55>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>");
        #endregion
    }
    #endregion
}
