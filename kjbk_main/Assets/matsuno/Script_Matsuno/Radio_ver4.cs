using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class Radio_ver4 : MonoBehaviour
{
    [SerializeField] GameObject ChatPanel;
    [SerializeField] GameObject ChatPanel1;
    [SerializeField] GameObject ChatPanel2;
    [SerializeField] GameObject ChatPanel3;
    [SerializeField] GameObject ChatPanel4;
    [SerializeField] GameObject ChatR;


    [SerializeField] public TMP_Text RadioText;
    [SerializeField] private TMP_Text RadioText2;

    public RescuePOP RPOP;
    [HideInInspector] public bool Radio80;
    [HideInInspector] public bool Radio60;
    [HideInInspector] public bool Radio40;
    [HideInInspector] public bool Radio20;
    [HideInInspector] public bool Radio10;


    //セリフを入れるスタック
    public Stack<string> stackObj = new Stack<string>();
    public Stack<string> stackRadio = new Stack<string>();
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

    //コルーチンの確認
    private Coroutine activeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        stackObj.Clear();
        stackRadio.Clear();
        stackBring.Clear();
        ChatPanel.SetActive(false);
        ChatPanel1.SetActive(false);
        ChatPanel2.SetActive(false);
        ChatPanel3.SetActive(false);
        ChatR.SetActive(false);
        //スタックの中身を入れる
        stackObj.Push("え、間取りがわからない?\r\nマップをあげるから確認してみて");
        stackObj.Push("助かったよ！<sprite=1>の\r\n奥の方で人が倒れてたの!");
        stackRadio.Push("倒壊寸前だぞ\r\n速く逃げろ");
        stackRadio.Push("天井が崩れ始めてるぞ\r\n急いでくれ");
        stackRadio.Push("防火シャッターをおろして炎の延焼を防いでいくぞ");
        stackRadio.Push("ヒビが拡大しているもしかしたら崩れるぞ");
        stackRadio.Push("何か建物にヒビが入っていないか？");
        stackRadio.Push("現場は学生寮だ！行方不明者の内10人を救うのが君の任務だ");
        stackBring.Push("重傷者はもういなさそうだ！");
        stackBring.Push("まだ重傷者がいるようだ！引き続き調査を頼む！");
        stackBring.Push("まだ重傷者がいるようだ！引き続き調査を頼む！");
        stackBring.Push("まだ重傷者がいるようだ！引き続き調査を頼む！");
        stackBring.Push("まだ重傷者がいるようだ！引き続き調査を頼む！");
        stackBring.Push("この建物の構造を入手した\n\rぜひ活用してみてくれ");
        FirstFlag = true;
        //ルール説明のラジオを出す
        CollapseDialogue();
    }

    // 軽症者のヒントをプッシュする
    public void Push()
    {
        //スタックの中身をリセット
        stackObj.Clear();
        int RandomText = RPOP.rndom;
        if(RandomText == 1)
        {
            stackObj.Push("<sprite=6>で人が動けないってい叫んでたわ");
            stackObj.Push("西南方面に人影があったかもしれない一応向かってくれないか");
            stackObj.Push("西の方に人が逃げていったぞ");
        }
        else if(RandomText == 2)
        {
            stackObj.Push("<sprite=5>で人が倒れていたわ");
            stackObj.Push("さっきから<sprite=5>方面で叫び声が聞こえるの");
            stackObj.Push("北側に人が走っていったよ");
        }
        else if(RandomText == 3)
        {
            stackObj.Push("さっき<sprite=2>に入った人がでてこないの...");
            stackObj.Push("炎で汗が止まらないわ...お風呂に入りたい...");
            stackObj.Push("さっき北側に人が向かっていったぞ");
        }
        else if(RandomText == 4)
        {
            stackObj.Push("<sprite=3>で物が倒れて動けない人がいるの！");
            stackObj.Push("服が煙まみれ～いち早く着替えたい！");
            stackObj.Push("南側に人が向かっていったわ");
        }
        else if(RandomText == 5)
        {
            stackObj.Push("<sprite=4>で酔っぱらったやつが寝てて起きないんだ！助けてやってくれ");
            stackObj.Push("まさかこんな状況で寝てるやつはいないよな...");
            stackObj.Push("さっき西側に走って逃げる人がいたぞ");
        }
    }

    // ヒントが出ないときはここからランダムでテキストを出力する
    void RandomDialugue()
    {
        int RndDialugue = Random.Range(1, 4);
        if(RndDialugue == 1)
        {
            RadioText.SetText("ほんとうに助かったよ！君は命の恩人だ！");
        }
        else if(RndDialugue == 2)
        {
            RadioText.SetText("ありがとう...生きて帰れる...");
        }
        else if(RndDialugue == 3)
        {
            RadioText.SetText("なんてすばらしい身のこなしなんだ！ありがとう！");
        }
    }

    //セリフを表示する
    public void Dialogue()
    {
        //テキストリセット
        TextPanelOFF();
        RPOP.AllRCnt--;

        // 現在のコルーチンが実行中なら停止する
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }

        int RndHalf = Random.Range(1, 3);
        //スタックの中身がカラだったらランダムにテキストを入れる
        if(stackObj.Count == 0)
        {
            RandomDialugue();
        }
        //スタックからポップをしてそのテキストを入れる
        else if (RndHalf == 1 || FirstFlag)
        {
            NewText = stackObj.Pop();
            RadioText.SetText(NewText);

            if(FirstFlag)
            {
                FirstTextFlag = true;
            }
            FirstFlag = false;
        }
        //軽症者数とスタックの中身が同数もしくは以下の場合ポップしてテキストに入れる
        else if(stackObj.Count >= RPOP.AllRCnt)
        {
            NewText = stackObj.Pop();
            RadioText.SetText(NewText);
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






//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //テキストを処理するプログラム

    //テキストを一文字ずつ表示するコード
    private IEnumerator Simple1()
    {
        TextONFlag = true;
        RadioText2.maxVisibleCharacters = 0;

        for (var i = 0; i < RadioText2.text.Length; i++)
        {
            //ここの値変更すると秒数変更可能
            yield return new WaitForSeconds(0.06f);
            RadioText2.maxVisibleCharacters = i + 1;
        }

        //1人目の救助の時のみの動作
        if(FirstTextFlag)
        {
            yield return new WaitForSeconds(0.5f);
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
            }
            //一番最初に重傷者を救ったときのセリフをスタックからポップする
            stackBring.Pop();
            stackBring.Push("まだ重傷者がいるようだ！引き続き調査を頼む！");
            NewText = stackObj.Pop();
            RadioText.SetText(NewText);
            activeCoroutine = StartCoroutine(Simple1());
            FirstTextFlag = false;
        }

        //テキストを数秒後にオフにする
        yield return new WaitForSeconds(1.5f);
        TextPanelOFF();
        TextONFlag = false;

        //もしコラプスゲージの無線の順番待ちがあったら実行する
        if(CollapseDialogueFlag)
        {
            yield return new WaitForSeconds(2.0f);
            CollapseDialogue();
        }
    }


    //テキストを非表示にする
    private void TextPanelOFF()
    {
        ChatPanel.SetActive(false);
        ChatPanel1.SetActive(false);
        ChatPanel2.SetActive(false);
        ChatPanel3.SetActive(false);
        ChatPanel4.SetActive(false);
        ChatR.SetActive(false);
    }


    //テキストを表示する
    public void TextPanelON()
    {
        number1 = PlayerPrefs.GetInt("R_number");
        if(CollapseIconFlag)
        {
            ChatPanel.SetActive(true);
            CollapseIconFlag = false;
        }
        else if(number1 == 1)
        {
            ChatPanel1.SetActive(true);
        }
        else if(number1 == 2)
        {
            ChatPanel2.SetActive(true);
        }
        else if(number1 == 3)
        {
            ChatPanel3.SetActive(true);
        }
        else if(number1 == 4)
        {
            ChatPanel4.SetActive(true);
        }
        ChatR.SetActive(true);
    }
}
