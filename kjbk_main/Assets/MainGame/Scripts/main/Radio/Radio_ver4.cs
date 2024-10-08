using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class Radio_ver4 : MonoBehaviour
{
    #region 変数の宣言
    [SerializeField] GameObject ChatPanel;
    [SerializeField] GameObject ChatPanel1;
    [SerializeField] GameObject ChatPanel2;
    [SerializeField] GameObject ChatPanel3;
    [SerializeField] GameObject ChatPanel4;
    [SerializeField] GameObject FirstChatPanel;
    [SerializeField] GameObject ChatR;


    [SerializeField] public TMP_Text RadioText;
    [SerializeField] private TMP_Text RadioText2;

    public RescuePOP RPOP;

    public ItemTake itemTake;
    


    //セリフを入れるスタック
    public Stack<string> stackObj = new Stack<string>();
    public Stack<string> stackRadio = new Stack<string>();
    public Stack<string> stackObjMan = new Stack<string>();
    public Stack<string> stackBring = new Stack<string>();

    //セリフをポップしたときに入れる変数
    string NewText;

    //軽症者の変数を入れる
    public int number1 = 1;
    //一番最初のテキストかどうかのフラグ
    public bool FirstFlag = true;
    //テキストが表示されているかのフラグ
    bool TextONFlag = false;
    //無線が待ち状態かどうかのフラグ
    bool CollapseDialogueFlag = false;
    //無線のアイコンを表示するかのフラグ
    bool CollapseIconFlag = false;
    //一番最初のテキストのフラグ
    bool FirstTextFlag = false;
    //missionMapUI
    public MissionMapUI MMUI;

    //コルーチンの確認
    private Coroutine activeCoroutine;
    
    //missionマップに送る変数
    public int MMcnt=0;
    //missionマップのスタック
    public Stack<string> stackMM = new Stack<string>();
    //missionマップのスタックをポップしたときに入れる変数
    string MMHint;

    //マップを与える関数を呼び出す変数
    public SwitchCamera SCame;
    //最初にプレイヤーを止めるフラグ
    public bool FirstStopPlayer = false;

    [SerializeField] GameObject FirstRescueWall;

    //アイテムのフラグ
    [HideInInspector] public bool Item1 = false;
    [HideInInspector] public bool Item2 = false;
    [HideInInspector] public bool Item3 = false;
    [HideInInspector] public bool Item4 = false;
    [HideInInspector] public bool Item5 = false;
    [HideInInspector] public bool Item6 = false;
    [HideInInspector] public bool Item7 = false;
    [HideInInspector] public bool Item8 = false;
    [HideInInspector] public bool Item9 = false;
    [HideInInspector] public bool ItemOff = false;
    //アイテムの配列
    int[] ItemCountArray = {1,2,3,4,5,6,7,8,9};
    //アイテム用のカウント
    int ItemCnt = 3;
    //救助者人数を送ってくる
    public RescueCount RCnt;

    //アイテムを列挙
    enum Item 
    {
        Kitchen = 0,
        Dining,
        Shower,
        Datsui,
        Toire,
        PC,
        Bed,
        Omotya,
        Soto,
    };

    //状況説明用のテキスト
    //アイテムテキスト関連
    string ItemText;
    [SerializeField] public TMP_Text ItemTextMeshPro;
    public GameObject ItemTextObject;
    private Coroutine ActiveCoroutine2;

    public static bool NPCStop = false;
    public static bool CharStop = false;

    //サブmission開始時のフラグ
    public static bool SMFlag = false;

    bool ClearFlag = false;

    #endregion

    void Start()
    {
        #region 初期化
        stackObj.Clear();
        stackRadio.Clear();
        stackBring.Clear();
        ChatPanel.SetActive(false);
        ChatPanel1.SetActive(false);
        ChatPanel2.SetActive(false);
        ChatPanel3.SetActive(false);
        ChatPanel4.SetActive(false);
        FirstChatPanel.SetActive(false);
        ChatR.SetActive(false);
        Item1 = false;
        Item2 = false;
        Item3 = false;
        Item4 = false;
        Item5 = false;
        Item6 = false;
        Item7 = false;
        Item8 = false;
        Item9 = false;
        ItemOff = false;

        CharStop = false;
        NPCStop = false;
        SMFlag = false;
        ClearFlag = false;
        #endregion

        #region スタックに無線テキストをpush
        //スタックの中身を入れる
        stackObj.Push("え、間取りがわからない?\r\nマップをあげるから確認してみて");
        stackObj.Push("助かったよ！<sprite=1>の\r\n奥の方で人が倒れてたの!");
        stackMM.Push(" <sprite=1>の奥の方で\n　人が倒れている？");
        stackRadio.Push("倒壊寸前だぞ\r\n速く逃げろ");
        stackRadio.Push("延焼が全然止まらない...\r\n他の防火シャッターも閉めるぞ");
        stackRadio.Push("防火シャッターをおろして\n\r炎の延焼を防いでいくぞ");
        stackRadio.Push("ヒビが拡大しているもしかしたら崩れるぞ");
        stackRadio.Push("何か建物にヒビが入っていないか？");
        stackRadio.Push("現場は学生寮だ！行方不明者の内\n\r10人を救うのが君の任務だ");
        stackBring.Push("重傷者はもういなさそうだ！");
        stackBring.Push("まだ重傷者がいるようだ！\n\r引き続き調査を頼む！");
        stackBring.Push("まだ重傷者がいるようだ！\n\r引き続き調査を頼む！");
        stackBring.Push("まだ重傷者がいるようだ！\n\r引き続き調査を頼む！");
        stackBring.Push("まだ重傷者がいるようだ！\n\r引き続き調査を頼む！");
        stackBring.Push("この建物の構造を入手した\n\rぜひ活用してみてくれ");
        #endregion

        FirstFlag = true;
        MMcnt = 0;
        ItemCnt = 3;
        //ルール説明のラジオを出す
        CollapseDialogue();
        FirstRescueWall.SetActive(true);
        #region　アイテムの設定
        //配列の中身をランダムにする
        for (int i=0;i< ItemCountArray.Length; i++)
        {
            int temp = ItemCountArray[i];
            int RndIndex = Random.Range(0, ItemCountArray.Length);
            ItemCountArray[i] = ItemCountArray[RndIndex];
            ItemCountArray[RndIndex] = temp;
        }
        for (int i = 0; i < ItemCountArray.Length; i++)
        {
            Debug.Log("配列の中身:" + ItemCountArray[i]);
        }
        #endregion
    }



    #region ヒント
    // 軽症者のヒントをプッシュする
    public void Push()
    {
        //スタックの中身をリセット
        stackObj.Clear();
        int RandomText = RPOP.rndom;
        //missionマップのリセット
        MMcnt = 0;
        MMUI.MissionUpgread("☐????????????????", MMcnt);
        if(RandomText == 1)
        {
            //軽症者テキストのヒント
            stackObj.Push("<sprite=6>で人が動けないってい叫んでたわ");
            stackObj.Push("西南方面に人影があったかもしれない\n\r一応向かってほしいの");
            stackObj.Push("西の方に人が逃げていったよ");
            stackObjMan.Push("<sprite=6>で人が動けないってい叫んでぞ");
            stackObjMan.Push("西南方面に人影があったかも\n\rしれない一応向かってくれないか");
            stackObjMan.Push("西の方に人が逃げていったぞ");
            //missionマップのヒント
            stackMM.Push(" <sprite=6>に人がいる？");
            stackMM.Push("西南方面に人影あり");
            stackMM.Push("西方面に人が逃げた");
        }
        else if(RandomText == 2)
        {
            //軽症者テキストのヒント
            stackObj.Push("<sprite=5>で人が倒れていたわ");
            stackObj.Push("さっきから<sprite=5>方面で叫び声が聞こえるの");
            stackObj.Push("北側に人が走っていったよ");
            stackObjMan.Push("<sprite=5>で人が倒れていたぞ");
            stackObjMan.Push("さっきから<sprite=5>方面で叫び声が聞こえるんだ");
            stackObjMan.Push("北側に人が走っていったぞ");
            //missionマップのヒント
            stackMM.Push(" <sprite=5>に人が倒れている？");
            stackMM.Push(" <sprite=5>方面で叫び声？");
            stackMM.Push("北側に人が逃げた");
        }
        else if(RandomText == 3)
        {
            //軽症者テキストのヒント
            stackObj.Push("さっき<sprite=2>に入った人が\n\rでてこないの...");
            stackObj.Push("炎で汗が止まらないわ...\n\rお風呂に入りたい...");
            stackObj.Push("さっき北側に人が向かっていったわ");
            stackObjMan.Push("さっき<sprite=2>に入った人が\n\rでてこないんだ...");
            stackObjMan.Push("炎で汗が止まらないぜ...\n\rお風呂に入りたい...");
            stackObjMan.Push("さっき北側に人が向かっていったぞ");
            //missionマップのヒント
            stackMM.Push(" <sprite=2>で人がでてこない？");
            stackMM.Push("お風呂に入りたい？\n　家事場でのんきな奴だ");
            stackMM.Push("北側に人が逃げた");
        }
        else if(RandomText == 4)
        {
            //軽症者テキストのヒント
            stackObj.Push("<sprite=3>で物が倒れて\n\r動けない人がいるの！");
            stackObj.Push("服が煙まみれ～\n\rいち早く着替えたい！");
            stackObj.Push("南側に人が向かっていったわ");
            stackObjMan.Push("<sprite=3>で物が倒れて\n\r動けない人がいるぞ！");
            stackObjMan.Push("服が煙まみれだ...\n\rいち早く着替えたい！");
            stackObjMan.Push("南側に人が向かっていったよ");
            //missionマップのヒント
            stackMM.Push(" <sprite=3>に重体者がいる？");
            stackMM.Push("服が煙まみれか...\n　確かに早く着替えたいな");
            stackMM.Push("南側に人が逃げた");
        }
        else if(RandomText == 5)
        {
            //軽症者テキストのヒント
            stackObj.Push("<sprite=4>で酔っぱらった人が寝てて\n\r起きないの！助けてほしい！");
            stackObj.Push("まさか...こんな状況で\n\r寝てるやつはいないよね...");
            stackObj.Push("さっき西側に走って逃げる人がいたわ");
            stackObjMan.Push("<sprite=4>で酔っぱらったやつが寝てて\n\r起きないんだ！助けてやってくれ");
            stackObjMan.Push("まさか...こんな状況で\n\r寝てるやつはいないよな...");
            stackObjMan.Push("さっき西側に走って逃げる人がいたぞ");
            //missionマップのヒント
            stackMM.Push(" <sprite=4>で酔っぱらって\n　倒れた人がいる？");
            stackMM.Push("火事場で寝てるやつは\n　いないだろ...");
            stackMM.Push("西側に人が逃げた");
        }
        if(FirstFlag)
        {
            SCame.MapON = true;
            FirstFlag = false;
        }
    }
    #endregion

    #region ヒント無し時のランダムテキスト
    // ヒントが出ないときはここからランダムでテキストを出力する
    void RandomDialugue()
    {
        int RndDialugue;
        //アイテムが出きってなければアイテムを含んだランダム
        if (!ItemOff)
        {
            RndDialugue = Random.Range(1, 9);
            if (RndDialugue == 1 || RndDialugue == 2)
            {
                RadioText.SetText("ほんとうに助かったよ！\n\r君は命の恩人だ！");
            }
            else if (RndDialugue == 3 || RndDialugue == 4)
            {
                RadioText.SetText("ありがとう...\n\r生きて帰れる...");
            }
            //50%の確率でアイテムを出現させる
            else if (RndDialugue == 7 || RndDialugue == 8 || RndDialugue == 5 || RndDialugue == 6)
            {
                ItemRandom();
            }
        }
        else
        {
            Debug.Log("アイテムもうないよ");
            RndDialugue = Random.Range(1, 4);
            if (RndDialugue == 1)
            {
                RadioText.SetText("ほんとうに助かったよ！\n\r君は命の恩人だ！");
            }
            else if (RndDialugue == 2)
            {
                RadioText.SetText("ありがとう...\n\r生きて帰れる...");
            }
            else if (RndDialugue == 3)
            {
                RadioText.SetText("なんてすばらしい身のこなしなんだ！\n\rありがとう！");
            }
        }
    }
    #endregion

    #region 救助人数が変わったときのテキスト
    public void ChangeCnt()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }
        Debug.Log("クリア条件達成テキスト！");
        ClearFlag = true;
        RadioText.SetText("無事任務達成だ！おめでとう！");
        ChatPanel.SetActive(true);
        ChatR.SetActive(true);
        HMIUI(4);
        // 新しいコルーチンを開始し、その参照を保存する
        activeCoroutine = StartCoroutine(Simple1());
    }
    #endregion

    #region セリフの表示
    //セリフを表示する
    public void Dialogue()
    {
        //テキストリセット
        TextPanelOFF();
        RPOP.AllRCnt--;
        int ResCnt = RCnt.getNum();
        int StackCnt = stackObj.Count;

        CharStop = true;
        number1 = PlayerPrefs.GetInt("R_number");

        // 現在のコルーチンが実行中なら停止する
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        ////もしアイテムの数と残り軽傷者数+ヒントの数が同じならアイテム出現をする
        //if(30-ResCnt-StackCnt == ItemCnt)
        //{
        //    ItemRandom();
        //}

        int RndHalf = Random.Range(1, 3);
        //スタックの中身がカラだったらランダムにテキストを入れる
        if(stackObj.Count == 0)
        {
            RandomDialugue();
        }
        //スタックからポップをしてそのテキストを入れる
        else if (RndHalf == 1 || FirstFlag)
        {
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                NewText = stackObjMan.Pop();
                stackObj.Pop();
            }
            else
            {
                NewText = stackObj.Pop();
                if(!FirstFlag)
                {
                    stackObjMan.Pop();
                }
            }
            
            RadioText.SetText(NewText);
            //missionマップにヒントを送る
            MMHint = stackMM.Pop();
            MMcnt++;
            MMUI.MissionUpgread(MMHint,MMcnt);
            HMIUI(1);
            //もし1人目の時
            if (FirstFlag)
            {
                //StartCoroutine(FirstTenmetsu());
                FirstStopPlayer = true;
                SCame.MapON = true;
                FirstTextFlag = true;
                FirstRescueWall.SetActive(false);
            }
            FirstFlag = false;
        }
        //軽症者数とスタックの中身が同数もしくは以下の場合ポップしてテキストに入れる
        else if(stackObj.Count >= RPOP.AllRCnt)
        {
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                NewText = stackObjMan.Pop();
                stackObj.Pop();
            }
            else
            {
                NewText = stackObj.Pop();
                stackObjMan.Pop();
            }
            RadioText.SetText(NewText);
            //missionマップにヒントを送る
            MMHint = stackMM.Pop();
            MMcnt++;
            MMUI.MissionUpgread(MMHint, MMcnt);
            HMIUI(1);
        }
        //それ以外はランダムでテキストに入れる
        else
        {
            RandomDialugue();
        }
        // 新しいコルーチンを開始し、その参照を保存する
        activeCoroutine = StartCoroutine(Simple1());
        TextPanelON();
    }
    #endregion

    #region コラプスゲージ表示
    //コラプスゲージを表示する
    public void CollapseDialogue()
    {
        CollapseDialogueFlag = true;
        //もしテキストが出ていなければ
        if(!TextONFlag)
        {
            CollapseIconFlag = true;
            NewText = stackRadio.Pop();
            RadioText.SetText(NewText);
            TextPanelON();
            CollapseDialogueFlag = false;
            // 新しいコルーチンを開始し、その参照を保存する
            activeCoroutine = StartCoroutine(Simple1());
        }
    }
    #endregion


    #region　アイテムの表示
    /// <summary>アイテムを表示</summary>
    void ItemRandom()
    {
        number1 = PlayerPrefs.GetInt("R_number");
        HMIUI(2);
        SMFlag = true;
        Debug.Log("ヒントオン！" + SMFlag);
        if (ItemCountArray[ItemCnt] == (int)Item.Kitchen)
        {
            Item1 = true;
            itemTake.ItemSet(0);
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                RadioText.SetText("友人と買ったマグカップを\n\r持ってきてほしいんだ...");
            }
            else
            {
                RadioText.SetText("友人と買ったマグカップを\n\r持ってきてほしいの...");
            }
            MMUI.MissionUpgread("☐マグカップを探せ！", ItemCnt,1);

        }
        else if (ItemCountArray[ItemCnt] == (int)Item.Dining)
        {
            Item2 = true;
            itemTake.ItemSet(1);
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                RadioText.SetText("スマホが見つからない...\n\rどこに落としたんだ...");
            }
            else
            {
                RadioText.SetText("スマホが見つからない...\n\rどこに落としちゃったんだろ...");
            }
            MMUI.MissionUpgread("☐スマホを探せ！", ItemCnt, 1);
        }
        else if (ItemCountArray[ItemCnt] == (int)Item.Shower)
        {
            Item3 = true;
            itemTake.ItemSet(2);
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                RadioText.SetText("限定品のシャンプーを\n\r持ってきてほしいんだ...");
            }
            else
            {
                RadioText.SetText("限定品のシャンプーを\n\r持ってきてほしいの...");
            }
            MMUI.MissionUpgread("☐シャンプーを探せ！", ItemCnt, 1);
        }
        else if (ItemCountArray[ItemCnt] == (int)Item.Datsui)
        {
            Item4 = true;
            itemTake.ItemSet(3);
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                RadioText.SetText("お気に入りのTシャツを\n\r持って帰ってきてほしい...");
            }
            else
            {
                RadioText.SetText("お気に入りのTシャツを\n\r持って帰ってきてほしいの...");
            }
            MMUI.MissionUpgread("☐Tシャツを探せ！", ItemCnt, 1);
        }
        else if (ItemCountArray[ItemCnt] == (int)Item.Toire)
        {
            Item5 = true;
            itemTake.ItemSet(4);
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                RadioText.SetText("思い出の絵画を\n\r取ってくれ...");
            }
            else
            {
                RadioText.SetText("思い出の絵画を\n\r取ってほしいの...");
            }
            MMUI.MissionUpgread("☐絵画を探せ！", ItemCnt, 1);
        }
        else if (ItemCountArray[ItemCnt] == (int)Item.PC)
        {
            Item6 = true;
            itemTake.ItemSet(5);
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                RadioText.SetText("俺の命より大事な\n\rゲーム機を取ってきてほしい...");
            }
            else
            {
                RadioText.SetText("私の命より大事な\n\rゲーム機を取ってきてほしいの...");
            }
            MMUI.MissionUpgread("☐ゲーム機を探せ！", ItemCnt, 1);
        }
        else if (ItemCountArray[ItemCnt] == (int)Item.Bed)
        {
            Item7 = true;
            itemTake.ItemSet(6);
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                RadioText.SetText("家族との写真を持って\n\r帰ってきてほしい...");
            }
            else
            {
                RadioText.SetText("家族との写真を持って\n\r帰ってきてほしいの...");
            }
            MMUI.MissionUpgread("☐写真を探せ！", ItemCnt, 1);
        }
        else if (ItemCountArray[ItemCnt] == (int)Item.Omotya)
        {
            Item8 = true;
            itemTake.ItemSet(7);
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                RadioText.SetText("子供のころから一緒に過ごしていた\n\rくまのぬいぐるみも助けてほしい！");
            }
            else
            {
                RadioText.SetText("子供のころから一緒に過ごしていた\n\rくまのぬいぐるみも助けてほしいの！");
            }
            MMUI.MissionUpgread("☐くまのぬいぐるみ\n　を探せ！", ItemCnt, 1);
        }
        else if (ItemCountArray[ItemCnt] == (int)Item.Soto)
        {
            Item9 = true;
            itemTake.ItemSet(8);
            if (number1 == 0 || number1 == 1 || number1 == 3 || number1 == 4)
            {
                RadioText.SetText("大切に育てた花が燃えてしまう...\n\r1輪でもいいから持ってきてほしい...");
            }
            else
            {
                RadioText.SetText("大切に育てた花が燃えてしまう...\n\r1輪でもいいから持ってきてほしいの...");
            }
            MMUI.MissionUpgread("☐花を探せ！", ItemCnt, 1);
        }
        ItemCnt--;
        if(ItemCnt==0)
        {
            ItemOff = true;
        }
    }
    #endregion

    #region ミッションやヒントを受け取った際の処理
    public void HMIUI(int HMICnt)
    {
        // 現在のコルーチンが実行中なら停止する
        if (ActiveCoroutine2 != null)
        {
            StopCoroutine(ActiveCoroutine2);
        }
        //ヒントを受け取ったときの処理
        if (HMICnt == 1)
        {
            ItemText = "ヒントを入手";
        }
        //サブミッションを受け取ったときの処理
        else if (HMICnt == 2)
        {
            ItemText = "サブミッションを開始";
        }
        //サブミッションを達成したときの処理
        else if (HMICnt == 3)
        {
            ItemText = "アイテムを取得";
        }
        else if (HMICnt == 4)
        {
            ItemText = "メインミッション達成";
        }
        ItemTextMeshPro.SetText(ItemText);
        ItemTextObject.SetActive(true);
        ActiveCoroutine2 = StartCoroutine(TextStop());
    }

    private IEnumerator TextStop()
    {
        yield return new WaitForSeconds(2f);
        ItemTextObject.SetActive(false);
    }
    #endregion

    #region コルーチン
    public void BringDialogue()
    {
        CollapseIconFlag = true;
        // 現在のコルーチンが実行中なら停止する
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }
        NewText = stackBring.Pop();
        RadioText.SetText(NewText);
        TextPanelON();
        // 新しいコルーチンを開始し、その参照を保存する
        activeCoroutine = StartCoroutine(Simple1());
    }
    #endregion

    #region テキスト処理
    //テキストを処理するプログラム

    #region テキストを1文字ずつ表示
    //テキストを一文字ずつ表示するコード
    private IEnumerator Simple1()
    {
        RMaxText.NoText = false;
        TextONFlag = true;
        RadioText2.maxVisibleCharacters = 0;

        for (var i = 0; i < RadioText2.text.Length; i++)
        {
            //ここの値変更すると秒数変更可能
            yield return new WaitForSeconds(0.06f);
            RadioText2.maxVisibleCharacters = i + 1;
        }

        //1人目の救助の時のみの動作
        if (FirstTextFlag)
        {
            yield return new WaitForSeconds(0.5f);
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }
            //一番最初に重傷者を救ったときのセリフをスタックからポップする
            stackBring.Pop();
            stackBring.Push("まだ重傷者がいるようだ！\n\r引き続き調査を頼む！");
            NewText = stackObj.Pop();
            RadioText.SetText(NewText);
            activeCoroutine = StartCoroutine(Simple1());
            FirstTextFlag = false;
        }
        //クリア条件達成時
        if(ClearFlag)
        {
            yield return new WaitForSeconds(0.5f);
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }
            RadioText.SetText("まだ建物に行方不明者がいるみたいだ\n\r時間の許す限り多くの人を救ってくれ");
            activeCoroutine = StartCoroutine(Simple1());
            ClearFlag = false;
        }

        //テキストを数秒後にオフにする
        yield return new WaitForSeconds(1.5f);
        TextPanelOFF();
        TextONFlag = false;
        
        //NPCとキャラ停止
        if(CharStop)
        {
            NPCStop = true;
        }
        
        CharStop = false;

        //もしコラプスゲージの無線の順番待ちがあったら実行する
        if (CollapseDialogueFlag)
        {
            yield return new WaitForSeconds(2.0f);
            CollapseDialogue();
        }
    }

    //最初のテキストの強調表示
    private IEnumerator FirstTenmetsu()
    {
        for(int FTcnt = 0; FTcnt <5; FTcnt++)
        {
            FirstChatPanel.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            FirstChatPanel.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    #endregion


    //テキストを非表示にする
    private void TextPanelOFF()
    {
        ChatPanel.SetActive(false);
        ChatPanel1.SetActive(false);
        ChatPanel2.SetActive(false);
        ChatPanel3.SetActive(false);
        ChatPanel4.SetActive(false);
        ChatR.SetActive(false);
        RMaxText.NoText = false;


    }


    //テキストを表示する
    public void TextPanelON()
    {
        number1 = PlayerPrefs.GetInt("R_number");
        if (CollapseIconFlag)
        {
            ChatPanel.SetActive(true);
            CollapseIconFlag = false;
        }
        else if (number1 == 1)
        {
            ChatPanel1.SetActive(true);
        }
        else if (number1 == 2)
        {
            ChatPanel2.SetActive(true);
        }
        else if (number1 == 3)
        {
            ChatPanel3.SetActive(true);
        }
        else if (number1 == 4)
        {
            ChatPanel4.SetActive(true);
        }
        ChatR.SetActive(true);
    }
    #endregion

}