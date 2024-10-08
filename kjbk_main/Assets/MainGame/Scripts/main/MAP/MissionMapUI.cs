using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionMapUI : MonoBehaviour
{
    #region 変数の宣言
    [SerializeField] public TMP_Text MissionMAPText; // UIに表示するテキスト

    [SerializeField] Scrollbar Scroll; // UIに表示するテキスト

    float ScrollValue = 1;
    float Yvalue;

    string MainMission; // メインミッションの内容
    string Hint1; // ヒント1の内容
    string Hint2; // ヒント2の内容
    string Hint3; // ヒント3の内容
    string SubMission1; //サブミッション1の内容
    string SubMission2; //サブミッション2の内容
    string SubMission3; //サブミッション3の内容
    string Situation; // 現在の状況

    public float delay = 0.9f;      // 1文字あたりの表示間隔

    // 前後の固定テキスト
    private string beforeText = "これは[";
    private string afterText = "]文章です。";

    // タイピングエフェクトを適用するテキスト
    private string typingText = "部分的に表示される";

    // 現在表示中のテキスト
    private string currentText = "";
    #endregion

    void Start()
    {

        #region 初期テキスト設定
        MainMission = "☐10人以上人を助けろ！";
        Hint1 = "<sprite=1>の奥の方で\n　人が倒れている？";
        Hint2 = "☐????????????????";
        Hint3 = "☐????????????????";
        SubMission1 = "☐????????????????";
        SubMission2 = "";
        SubMission3 = "";
        Situation = "多くの人を救え！";
        MissionMAPText.SetText("<size=55>MISSION</size>\n" +
                               "<size=40>" + MainMission + "</size>" + "\n\n\r" +
                               "<size=55>HINT</size>\n" +
                               "<size=40>" + Hint1 + "</size>\n" +
                               "<size=40>" + Hint2 + "</size>\n" +
                               "<size=40>" + Hint3 + "</size>\n\n\r" +
                               "<size=55>SUBMISSION</size>\n" +
                               "<size=40>" + SubMission1 + "</size>\n" +
                               "<size=40>" + SubMission2 + "</size>\n" +
                               "<size=40>" + SubMission3 + "</size>\n\n\r" +
                               "<size=55>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>");

        #endregion


    }

    private void Update()
    {
        #region スクロールバーの更新
        float Yvalue = Input.GetAxisRaw("Vertical");

        if (Yvalue != 0)
        {
            ScrollValue += Yvalue / 100;
            if (ScrollValue > 1)
            {
                ScrollValue = 1;
            }
            if (ScrollValue < 0)
            {
                ScrollValue = 0;
            }
            Scroll.value = ScrollValue;
        }
        #endregion
    }

    // スクロールバーの入力を無効化
    public void DisableScroll()
    {
        Scroll.interactable = false;
    }

    // スクロールバーの入力を有効化
    public void EnableScroll()
    {
        Scroll.interactable = true;
    }


    private void OnEnable()
    {
        if(SwitchCamera.SMMFlag)
        {
            ScrollValue = 0;
            Scroll.value = ScrollValue;
            DisableScroll();
            SwitchCamera.SMMFlag = false;
        }
        else
        {
            ScrollValue = 1;
            Scroll.value = ScrollValue;
        }
        
    }


    #region ミッションの更新
    /// <summary>テキストを更新する</summary>
    /// <param name="Text">文字を受け取る</param>
    /// <param name="cnt">数値を受け取る</param>
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
                               "<size=55>SUBMISSION</size>\n" +
                               "<size=40>" + SubMission1 + "</size>\n" +
                               "<size=40>" + SubMission2 + "</size>\n" +
                               "<size=40>" + SubMission3 + "</size>\n\n\r" +
                               "<size=55>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>");
        #endregion
    }
    #region サブミッションの更新
    public void MissionUpgread(string Text, int SMTCnt,int a)
    {
        if (SMTCnt == 3)
        {
            SubMission1 = Text;
            typingText = Text;
            beforeText = "<size=55>MISSION</size>\n" +
                               "<size=40>" + MainMission + "</size>" + "\n\n\r" +
                               "<size=55>HINT</size>\n" +
                               "<size=40>" + Hint1 + "</size>\n" +
                               "<size=40>" + Hint2 + "</size>\n" +
                               "<size=40>" + Hint3 + "</size>\n\n\r" +
                               "<size=55>SUBMISSION</size>\n";
            afterText = "<size=40>" + SubMission2 + "</size>\n" +
                               "<size=40>" + SubMission3 + "</size>\n\n\r" +
                               "<size=55>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>";
        }
        else if (SMTCnt == 2)
        {
            SubMission2 = Text;
            typingText = Text;
            beforeText = "<size=55>MISSION</size>\n" +
                               "<size=40>" + MainMission + "</size>" + "\n\n\r" +
                               "<size=55>HINT</size>\n" +
                               "<size=40>" + Hint1 + "</size>\n" +
                               "<size=40>" + Hint2 + "</size>\n" +
                               "<size=40>" + Hint3 + "</size>\n\n\r" +
                               "<size=55>SUBMISSION</size>\n" +
                               "<size=40>" + SubMission1 + "</size>\n";
            afterText = "<size=40>" + SubMission3 + "</size>\n\n\r" +
                               "<size=55>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>";
        }
        else if (SMTCnt == 1)
        {
            SubMission3 = Text;
            typingText = Text;
            beforeText = "<size=55>MISSION</size>\n" +
                               "<size=40>" + MainMission + "</size>" + "\n\n\r" +
                               "<size=55>HINT</size>\n" +
                               "<size=40>" + Hint1 + "</size>\n" +
                               "<size=40>" + Hint2 + "</size>\n" +
                               "<size=40>" + Hint3 + "</size>\n\n\r" +
                               "<size=55>SUBMISSION</size>\n" +
                               "<size=40>" + SubMission1 + "</size>\n" +
                               "<size=40>" + SubMission2 + "</size>\n";
            afterText = "<size=55>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>";
        }
        #region UIテキストの更新
        MissionMAPText.SetText("<size=55>MISSION</size>\n" +
                               "<size=40>" + MainMission + "</size>" + "\n\n\r" +
                               "<size=55>HINT</size>\n" +
                               "<size=40>" + Hint1 + "</size>\n" +
                               "<size=40>" + Hint2 + "</size>\n" +
                               "<size=40>" + Hint3 + "</size>\n\n\r" +
                               "<size=55>SUBMISSION</size>\n" +
                               "<size=40>" + SubMission1 + "</size>\n" +
                               "<size=40>" + SubMission2 + "</size>\n" +
                               "<size=40>" + SubMission3 + "</size>\n\n\r" +
                               "<size=55>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>");
        #endregion
    }
    #endregion

    public void TextUpDate()
    {
        MissionMAPText.SetText("<size=55>MISSION</size>\n" +
                              "<size=40>" + MainMission + "</size>" + "\n\n\r" +
                              "<size=55>HINT</size>\n" +
                              "<size=40>" + Hint1 + "</size>\n" +
                              "<size=40>" + Hint2 + "</size>\n" +
                              "<size=40>" + Hint3 + "</size>\n\n\r" +
                              "<size=55>SUBMISSION</size>\n" +
                              "<size=40>" + SubMission1 + "</size>\n" +
                              "<size=40>" + SubMission2 + "</size>\n" +
                              "<size=40>" + SubMission3 + "</size>\n\n\r" +
                              "<size=55>SITUATION</size>\n" +
                              "<size=40>" + Situation + "</size>");
    }

    public IEnumerator ShowText()
    {
        // 最初に前の固定テキストを表示
        currentText = beforeText;
        MissionMAPText.text = currentText;
        //Audio.GetInstance().PlaySound(18);  //マップを開いたときの音

        // タイピングエフェクトを適用する部分を一文字ずつ表示
        for (int i = 0; i <= typingText.Length; i++)
        {
            currentText = beforeText + "<size=45><color=red>" + typingText.Substring(0, i) + "</color></size>\n" + afterText;
            MissionMAPText.text = currentText;
            yield return new WaitForSeconds(delay); // 文字の表示間隔
        }
        // サブミッションがでた後に点滅を開始する
        StartCoroutine(BlinkText());
    }

    //サブミッションのテキスト点滅
    private IEnumerator BlinkText()
    {
        bool isVisible = true;
        while (true)
        {
            if (isVisible)
            {
                // typingTextを表示
                currentText = beforeText + "<size=45><color=red>" + typingText + "</color></size>\n" + afterText;
            }
            else
            {
                // typingTextを非表示
                currentText = beforeText + "<size=45><color=red>" + " " + "</color></size>\n" + afterText;
            }

            MissionMAPText.text = currentText;
            isVisible = !isVisible; // 表示・非表示を切り替え

            yield return new WaitForSeconds(0.25f); // 点滅の間隔
        }
    }
    #endregion
}
