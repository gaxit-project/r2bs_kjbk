using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class RescueNPC : MonoBehaviour
{
    #region 変数
    //入力
    [SerializeField] public bool Severe;
    [SerializeField] int NpcUp;   //NPCを運搬するときにプレイヤーの頭上のどれだけ上に追従するかの数値
    [SerializeField] public string text;   //NPCに近づいたときに表示されるtext


    //アタッチ
    public GameObject Player;   //PlayerのGameObject
    public GameObject Zone;   //救出判定のGameObject
    [SerializeField] TextMeshPro TMP;   //NPCに近づいたときに表示されるtextMesh
    [SerializeField] public NPCAI NPCAI;   //NPCのAIスクリプト
    [SerializeField] public RadioText RadioText;   //無線制御
    [SerializeField] public RescuePOP POP;
    [SerializeField] HoldGauge HoldGauge;
    private GameObject Rescue;
    private GameObject ResCounter;
    private PlayController PlayCon;

    public GameObject AudioManager;//大地変更点

    RescueCount CounterScript;   //救助者カウント
    RescueDiplication DiplicationScript;

    Audio talkScript;//大地変更点

    MeshRenderer mesh;   //MeshRendere

    public static bool Follow = false;   //NPCの追従 true = 追従 : false = 待機
    bool InGoal = false;   //救出地点に接触 true =　接触 : false = 非接触
    bool InZone = false;   //救出範囲に接触 true = 接触 : false = 非接触
    bool NPCrun = false;   //NPCの自動操作 true = 自動操作 : false = NPC_AIによる操作
    bool Rescued = false;   //キーボードを一回だけ入力するためのフラグ
    bool ActiveIcon = false;   //会話アイコンの制御
    bool FirstContact = false;   //会話回数の判定
    bool SecondContact = false;   //会話回数の判定
    bool Lock = false;   //Playerの動きの固定

    private InputAction TalkAction;
    private InputAction ResTalkAction;

    private Animator FFanimator;
    private Animator NPCanimator;

    BoxCollider NPCCol;

    public static int r_num = 0;
    [HideInInspector] public int MCnt = 0;  //軽傷者のカウント，MCntが3になったら0に戻してカウントをし直す

    public Radio_ver4 Radio4;

    public R_Number number;

    GameObject gameManagerObj;
    GameManager gameManager;

    public int NumberR;

    public RadioText RText;
    public bool ArrowON = false;

    public static bool FirstFlag = false;

    bool RescueStopButtom = true;

    public static bool FirstResFlag = true;

    private NavMeshAgent navAgent;

    public TalkAI TalkAI;

    public static bool isTalkingToNPC = false; // プレイヤーがNPCと話しているかどうかを追跡するフラグ
    public float interactionRange = 5f; // 対話可能な距離の設定（適宜調整）
    #endregion

    void Start()
    {
        #region 取得・読み込み
        TalkAI = GetComponent<TalkAI>();
        navAgent = GetComponent<NavMeshAgent>();  // NavMeshAgentのコンポーネントを取得
        mesh = GetComponent<MeshRenderer>();
        Rescue = GameObject.Find("Rescue");
        ResCounter = GameObject.Find("Rcounter");
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        CounterScript = ResCounter.GetComponent<RescueCount>();
        PlayCon = Player.GetComponent<PlayController>();

        var pInput = this.GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        TalkAction = actionMap["Talk"];
        ResTalkAction = actionMap["ResTalk"];

        //アニメーション読み込み
        FFanimator = Player.GetComponent<Animator>();
        NPCanimator = this.GetComponent<Animator>();

        NPCCol = this.GetComponent<BoxCollider>();

        gameManagerObj = GameObject.Find("Manager");
        gameManager = gameManagerObj.GetComponent<GameManager>(); // スクリプトを取得

        RescueStopButtom = true;
        FirstResFlag = true;


        #endregion

        #region 初期化

        //初期化
        r_num = 0;

        //////////////////////////////////
        Follow = false;   //NPCの追従 true = 追従 : false = 待機
        InGoal = false;   //救出地点に接触 true =　接触 : false = 非接触
        InZone = false;   //救出範囲に接触 true = 接触 : false = 非接触
        NPCrun = false;   //NPCの自動操作 true = 自動操作 : false = NPC_AIによる操作
        Rescued = false;   //キーボードを一回だけ入力するためのフラグ
        ActiveIcon = false;   //会話アイコンの制御
        FirstContact = false;   //会話回数の判定
        SecondContact = false;   //会話回数の判定
        Lock = false;
        #endregion
    }

    void Update()
    {
        bool Talk = TalkAction.triggered;
        bool ResTalk = ResTalkAction.triggered;

        Transform target = Player.transform;   //PlayerのTransform
        Vector3 TargetPosition = target.position;

        #region 救助範囲に入っている
        if (IsItInZone())
        {
            
            #region 重傷者
            if ( Severe && !IsItInGoal())   //重傷者に近づいたとき
            {
                if (HoldGauge != null && HoldGauge.gaugeStatus)//救助入力
                {
                    Debug.Log("Follow ; " + IsItFollow() + "\nDiplication ; " + DiplicationScript.getFlag());
                    if (!IsItFollow() && !DiplicationScript.getFlag())   //非追従時
                    {
                        DiplicationScript.OnFlag();
                        StopNPC();
                        SetFollow(true);
                        PlayerPrefs.SetInt("Lock", 1);
                        FFanimator.SetBool("Walk", false);
                        FFanimator.SetBool("Carry", true);
                        NPCanimator.SetBool("NPCCarry", true);
                        NPCCol.enabled = false;
                        Invoke(nameof(MoveLock), 2f);
                    }
                    
                    else   //追従時
                    {
                        
                    //    DiplicationScript.OffFlag();
                    //    SetFollow(false);
                    //    PlayerPrefs.SetInt("Lock", 1);
                    //    FFanimator.SetBool("Walk", false);
                    //    FFanimator.SetBool("Carry", false);
                    //    NPCanimator.SetBool("NPCCarry", false);
                    //    NPCCol.enabled = true;
                    //    PutVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);
                    //    Invoke(nameof(MoveLock), 2f);
                    }
                    
                }

            }
            #endregion

            #region 軽傷者
            //軽傷者消滅用
            if (Talk && Severe == false && RescueStopButtom)   //軽症者に近づいたとき
            {
                if (!IsItFirstContact())
                {
                    // プレイヤーを停止させる
                    TalkAI.FFStop = true;  // プレイヤーキャラを停止
                    Debug.Log("プレイヤーが停止しました。");
                    RescueStopButtom = false;
                    ComentON();// オブジェクト削除
                    SetActiveIcon(true);
                    StopNPC();
                    RotateToPlayer();  // 軽傷者をプレイヤーの方向に向ける
                    RadioText.SetActiveText(true);
                    AudioManager.GetComponent<Audio>().PlaySound(2);    //大地変更点

                    // AutoWalkスクリプトを無効化
                    AutoWalk autoWalkScript = GetComponent<AutoWalk>();
                    if (autoWalkScript != null)
                    {
                        autoWalkScript.enabled = false;  // AutoWalkスクリプトを無効化
                    }
                    // ナビメッシュエージェントを無効化
                    if (navAgent != null)
                    {
                        navAgent.enabled = false;  // NavMeshAgentを無効化
                    }


                    StartCoroutine(StopAutoWalk());

                    RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPCを救出したときのVector
                    SetRescued(true);
                    NPCanimator.SetBool("Walk", false);
                    CountDestroy();   //一定時間後にオブジェクト削除
                }
            }
            #endregion

            #region 重傷者を担いでいるとき
            if (IsItFollow() && !IsItInGoal() && Severe == true)   //追従時
            {
                FollowVectorNPC(TargetPosition.x, TargetPosition.y + NpcUp, TargetPosition.z);   //NPCを運搬する時のVector
                SetText("[E]Put");
            }
            #endregion

            #region 重傷者を救助地点にふれた時
            if (IsItInGoal() && !IsItRescued() && Severe == true)   //救出地点に接触かつ未救出かつ重傷者
            {
                DiplicationScript.OffFlag();
                SetText("");
                SetFollow(false);
                PlayerPrefs.SetInt("Lock", 0);//1ならロック
                FFanimator.SetBool("Carry", false);
                NPCanimator.SetBool("NPCCarry", false);
                NPCCol.enabled = true;
                Invoke(nameof(MoveLock), 2f);
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPCを救出したときのVector
                SetRescued(true);
                CountDestroy2();   //一定時間後にオブジェクト削除
                r_num = CounterScript.getNum();
                POP.PopR();
                ArrowON = false;
            }
            #endregion

            #region 軽傷者が救助地点にふれた時
            if (IsItInGoal() && !IsItRescued() && Severe == false)   //救出地点に接触かつ未救出かつ軽症者
            {
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPCを救出したときのVector
                SetRescued(true);
                NPCanimator.SetBool("Walk", false);
                CountDestroy();   //一定時間後にオブジェクト削除
                CounterScript.Count();
            }
            #endregion
        }
        #endregion
    }

    #region 関数
    //関数
    public bool ArrowONOFF()
    {
        return ArrowON;
    }
    public void ArrowFlag()
    {
        ArrowON = true;
    }
    public int getNum()
    {
        return r_num;
    }

    public void ComentON()
    {
        number.RNumber();
        Radio4.Dialogue();
        POP.LightR();
        RText.RescueFlag = true;
    }
    public void CountDestroy()//オブジェクトの破壊
    {
        if (Severe)//重傷者の時
        {
            POP.HeavyR();
        }
        //Invoke("Destroy", 5f);
    }
    public void CountDestroy2()//オブジェクトの破壊
    {
        if (Severe)//重傷者の時
        {
            POP.HeavyR();
        }
        Invoke("Destroy", 0f);
        CounterScript.Count();   //救助者カウント
        r_num = CounterScript.getNum();
    }


    private void Destroy()
    {
        Destroy(this.gameObject);
        //CounterScript.Count();   //救助者カウント
        //r_num = CounterScript.getNum();

    }
    private void Count()
    {
        CounterScript.Count();   //救助者カウント
        r_num = CounterScript.getNum();
    }

    void FollowVectorNPC(float x, float y, float z)//NPCの追従
    {
        transform.position = new Vector3(x, y-4.5f, z);
        transform.rotation = Quaternion.Euler(0,- 90, 0);
        transform.right = PlayController.CurrentForward;
    }

    void RescuedVectorNPC(float x, float y, float z)//NPC救出時の動作
    {
        Vector3 NowPosition = transform.position;  // 現在の位置
        Vector3 TargetPosition = new Vector3(x, y, z);  // 目標位置

        // 現在位置と目標位置が大きく離れている場合にのみ移動
        if (Vector3.Distance(NowPosition, TargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(NowPosition, TargetPosition, 0f);  // 速度を2fに変更して移動速度を緩やかに
        }
    }

    void PutVectorNPC(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
        int ran = UnityEngine.Random.Range(0, 2); // ランダム生成
        if (ran == 0)
        {
            this.transform.Rotate(-90f, 0f, 0f); // ランダムな角度で回転
        }
        else
        {
            this.transform.Rotate(90f, 0f, 0f); // ランダムな角度で回転
        }
    }

    public void StopNPC()   //NPC_AIの停止
    {
        NPCAI.MoveNPC();
        SetText("");
    }

    public void SetText(string text)   //textの設定
    {
        TMP.text = text;
    }

    public void WaitChange(float f)
    {
        Invoke("ChangeAlpha", f);
    }

    private void ChangeAlpha()
    {
        StartCoroutine("TransParent");
    }

    IEnumerator TransParent()
    {
        for (int i = 0; i < 51; i++)
        {
            mesh.material.color = mesh.material.color - new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(0.07f);
        }
        //Destroy();
        RescueStopButtom = true;
    }

    IEnumerator StopAutoWalk()
    {
        // フラグが立つまでループ
        while (!Radio_ver4.NPCStop)
        {
            yield return null; // フレームごとに待機（1フレーム毎に確認）
        }

        // フラグが立ったらこの処理が実行される
        
        if (navAgent != null)
        {
            navAgent.enabled = true;  // NavMeshAgentを有効
        }
        TalkAI TalkAIScript = GetComponent<TalkAI>();
        if (TalkAIScript != null)
        {
            TalkAIScript.enabled = true;  // talkAIスクリプトを有効
            TalkAI.TalkToNPC();
        }

        CounterScript.Count();   //救助者カウント
        r_num = CounterScript.getNum();
        Radio_ver4.NPCStop = false;

        while (!TalkAI.NPCDestroy)
        {
            Debug.Log("FirstResFlagその１:" + FirstResFlag);
            if (FirstResFlag == true)
            {
                // NPCが削除された後にプレイヤーを動けるようにする
                TalkAI.FFStop = false;  // プレイヤーキャラを再度動かす
                Debug.Log("NPCが削除されました。プレイヤーが再度動けます。");
                Debug.Log("初めての救助達成！");
                FirstResFlag = false;
                Debug.Log("FirstResFlag:" + FirstResFlag);
                Invoke("Destroy", 0.1f);
                break;
            }
            yield return null; // フレームごとに待機（1フレーム毎に確認）
        }
        RescueStopButtom = true;
        TalkAI.NPCDestroy = false;
    }

    void RotateToPlayer()
    {
        // プレイヤーの位置を取得
        Vector3 directionToPlayer = Player.transform.position - transform.position;

        // 高さ（y座標）を無視して水平方向にのみ向くようにする
        directionToPlayer.y = 0;

        // 方向ベクトルがゼロでない場合にのみ回転
        if (directionToPlayer != Vector3.zero)
        {
            // NPCがプレイヤーの方向を向くための回転を計算
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // 即座に回転させる
            transform.rotation = targetRotation;
        }
    }





    //bool判定
    public bool IsItFollow()
    {
        return Follow;
    }
    public bool IsItInGoal()
    {
        return InGoal;
    }
    public bool IsItNPCrun()
    {
        return NPCrun;
    }
    public bool IsItRescued()
    {
        return Rescued;
    }
    public bool IsItActiveIcon()
    {
        return ActiveIcon;
    }
    public bool IsItInZone()
    {
        return InZone;
    }
    public bool IsItFirstContact()
    {
        return FirstContact;
    }
    public bool IsItSecondContact()
    {
        return SecondContact;
    }
    public bool IsItLock()
    {
        return Lock;
    }

    //boolSet
    public void SetFollow(bool b)
    {
        Follow = b;
    }
    public void SetInGoal(bool b)
    {
        InGoal = b;
    }
    public void SetNPCrun(bool b)
    {
        NPCrun = b;
    }
    public void SetRescued(bool b)
    {
        Rescued = b;
    }
    public void SetActiveIcon(bool b)
    {
        ActiveIcon = b;
    }
    public void SetInZone(bool b)
    {
        InZone = b;
    }
    public void SetFirstContact(bool b)
    {
        FirstContact = b;
    }
    public void SetSecondContact(bool b)
    {
        SecondContact = b;
    }
    public void SetLock(bool b)
    {
        Lock = b;
    }

    public void MoveLock()
    {
        PlayerPrefs.SetInt("Lock", 0);
    }
    #endregion
}