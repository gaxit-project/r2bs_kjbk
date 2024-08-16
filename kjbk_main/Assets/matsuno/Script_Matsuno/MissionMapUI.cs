using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionMapUI : MonoBehaviour
{
    [SerializeField] public TMP_Text MissionMAPText;

    string MainMission;
    string Hint1;
    string Hint2;
    string Hint3;
    string Situation;
    // Start is called before the first frame update
    void Start()
    {
        MainMission = "☐10人以上人を助けろ！";
        Hint1 = "☐????????????????";
        Hint2 = "☐????????????????";
        Hint3 = "☐????????????????";
        Situation = "多くの人を救え！";
        MissionMAPText.SetText("<size=60>MISSION</size>\n" +
                                   "<size=40>" + MainMission + "</size>" + "\n\n\r"
                               + "<size=60>HINT</size>\n" +
                               "<size=40>" + Hint1 + "</size>\n" +
                               "<size=40>" + Hint2 + "</size>\n" +
                               "<size=40>" + Hint3 + "</size>\n\n\r" +
                               "<size=60>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //テキストを更新する
    public void MissionUpgread(string Text,int cnt)
    {
        if(cnt == 0)
        {
            Hint1 = Text;
            Hint2 = Text;
            Hint3 = Text;
        }
        else if(cnt == 1)
        {
            Hint1 = "■"+Text;
        }
        else if(cnt == 2)
        {
            Hint2 = "■" + Text;
        }
        else if(cnt == 3)
        {
            Hint3 = "■" + Text;
        }
        else if(cnt == 10)
        {
            MainMission = "■10人以上人を助ける！\n☐出口へ迎おう！";
            Situation = "時間の許す限り人を\n救って脱出しよう！";
        }
        MissionMAPText.SetText("<size=60>MISSION</size>\n" +
                                   "<size=40>" + MainMission + "</size>" + "\n\n\r"
                               + "<size=60>HINT</size>\n" +
                               "<size=40>" + Hint1 + "</size>\n" +
                               "<size=40>" + Hint2 + "</size>\n" +
                               "<size=40>" + Hint3 + "</size>\n\n\r" +
                               "<size=60>SITUATION</size>\n" +
                               "<size=40>" + Situation + "</size>");
    }
}
