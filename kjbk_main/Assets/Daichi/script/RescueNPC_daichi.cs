using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class RescueNPC_daichi : MonoBehaviour
{
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
    private GameObject Rescue;
    private GameObject ResCounter;
    private GameObject FF;
    private GameObject AudioManager;
    RescueCount CounterScript;   //救助者カウント
    RescueDiplication DiplicationScript;
    Audio_daichi talkScript;

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

    private Animator FFanimator;
    private Animator NPCanimator;

    BoxCollider NPCCol;

    public static int r_num = 0;
    [HideInInspector] public int MCnt = 0;  //軽傷者のカウント，MCntが3になったら0に戻してカウントをし直す
    public CollRadio RadioM;

    public Radio_ver3 Radio3;

    public R_Number number;

    public int NumberR;

    private AudioSource audiosource;


    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        Rescue = GameObject.Find("Rescue");
        ResCounter = GameObject.Find("Rcounter");
        DiplicationScript = Rescue.GetComponent<RescueDiplication>();
        CounterScript = ResCounter.GetComponent<RescueCount>();
        audiosource = GetComponent<AudioSource>();
        FF = GameObject.Find("AudioManager");
        talkScript = AudioManager.GetComponent<Audio_daichi>();

        var pInput = this.GetComponent<PlayerInput>();
        //現在のアクションマップを取得
        var actionMap = pInput.currentActionMap;

        //アクションマップからアクションを取得
        TalkAction = actionMap["Talk"];

        //アニメーション読み込み
        FFanimator = Player.GetComponent<Animator>();
        NPCanimator = this.GetComponent<Animator>();

        NPCCol = this.GetComponent<BoxCollider>();

        //初期化
        r_num = 0;
    }

    void FixedUpdate()
    {
        bool Talk = TalkAction.triggered;

        Transform target = Player.transform;   //PlayerのTransform
        Vector3 TargetPosition = target.position;
        if (IsItInZone())
        {
            if (Talk && Severe == true && !IsItInGoal())   //重傷者に近づいたとき
            {
                if (!IsItFollow() && !DiplicationScript.getFlag())   //非追従時
                {
                    DiplicationScript.OnFlag();
                    StopNPC();
                    SetFollow(true);
                    FFanimator.SetBool("Carry", true);
                    NPCanimator.SetBool("NPCCarry", true);
                    NPCCol.enabled = false;
                }
                else   //追従時
                {
                    DiplicationScript.OffFlag();
                    SetFollow(false);
                    FFanimator.SetBool("Carry", false);
                    NPCanimator.SetBool("NPCCarry", false);
                    NPCCol.enabled = true;
                    PutVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);
                }
            }
            if (Talk && Severe == false)   //軽症者に近づいたとき
            {
                if (!IsItFirstContact())
                {
                    ComentON();// オブジェクト削除
                    Debug.Log(SecondContact);
                    SetFirstContact(true);
                    SetActiveIcon(true);
                    StopNPC();
                    RadioText.SetActiveText(true);
                   // NPCanimator.SetBool("Walk", true);
                    //talkScript.PlaySound(2);
                    AudioManager.GetComponent<Audio_daichi>().PlaySound(2);
                }
                else if (IsItFirstContact())
                {
                    SetSecondContact(true);
                    Debug.Log("Second");
                    StopNPC();
                    RadioText.SetActiveText(true);
                    NPCanimator.SetBool("Walk", true);
                }
            }


            if (IsItFollow() && !IsItInGoal() && Severe == true)   //追従時
            {
                FollowVectorNPC(TargetPosition.x, TargetPosition.y + NpcUp, TargetPosition.z);   //NPCを運搬する時のVector
                SetText("[E]Put");
            }

            if (IsItInGoal() && !IsItRescued() && Severe == true)   //救出地点に接触かつ未救出かつ重傷者
            {
                DiplicationScript.OffFlag();
                SetText("");
                SetFollow(false);
                FFanimator.SetBool("Carry", false);
                NPCanimator.SetBool("NPCCarry", false);
                NPCCol.enabled = true;
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPCを救出したときのVector
                SetRescued(true);
                CountDestroy();   //一定時間後にオブジェクト削除
                //CounterScript.SevereCount();   //救助者カウント
                CounterScript.Count();
                r_num = CounterScript.getNum();
                POP.PopR();
            }

            if (IsItInGoal() && !IsItRescued() && Severe == false)   //救出地点に接触かつ未救出かつ軽症者
            {
                RescuedVectorNPC(TargetPosition.x, TargetPosition.y, TargetPosition.z);   //NPCを救出したときのVector
                SetRescued(true);
                NPCanimator.SetBool("Walk", false);
                CountDestroy();   //一定時間後にオブジェクト削除
                CounterScript.Count();
            }
        }
    }

    //関数

    public int getNum()
    {
        return r_num;
    }

    public void ComentON()
    {
       // NumberR = number.RNumber();
        Debug.Log("救助者のナンバー：" + NumberR);
        POP.LightR();
        Radio3.RPopFlag = true;
       // Radio3.RHintStop(NumberR);
    }
    public void CountDestroy()//オブジェクトの破壊
    {
        if (Severe)//重傷者の時
        {
            POP.HeavyR();
            Radio3.RHintFlag = true;
            Radio3.SymbolStop();
        }
        Invoke("Destroy", 2f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void Count()
    {
        CounterScript.Count();   //救助者カウント
        r_num = CounterScript.getNum();
    }

    void FollowVectorNPC(float x, float y, float z)//NPCの追従
    {
        transform.position = new Vector3(x, y - 4.5f, z);
        transform.rotation = Quaternion.Euler(0, -90, 0);
        transform.right = PlayController.CurrentForward;
    }

    void RescuedVectorNPC(float x, float y, float z)//NPC救出時の動作
    {
        Vector3 NowPosition = new Vector3(x, y, z);
        Vector3 HelpPosition = new Vector3(x, y, z - 30);
        transform.position = Vector3.MoveTowards(NowPosition, HelpPosition, 12f);
    }

    void PutVectorNPC(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
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
        Destroy();
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
}